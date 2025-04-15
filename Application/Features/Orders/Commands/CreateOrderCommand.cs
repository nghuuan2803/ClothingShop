using Application.Common.Mapping;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Shared.Enums;
using Shared.Orders;

namespace Application.Features.Orders.Commands
{
    public class CreateOrderCommand : IRequest<OrderDto>
    {
        public string CustomerId { get; set; } = string.Empty;
        public string? CustomerName { get; set; }
        public string? PhoneNumber { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public IEnumerable<OrderItemReq>? Items { get; set; }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<Inventory> _inventoryRepo;
        private readonly IRepository<CartItem> _cartRepo;
        private readonly IRepository<Customer> _customerRepo;
        private readonly IAmountCalculator _amountCalculator;
        private readonly IShipFeeCalculator _shipFeecalculator;
        private readonly UpdateCustomerProcess _customerProcess;

        public CreateOrderCommandHandler(IRepository<Order> orderRepo, IRepository<Inventory> inventoryRepo, IRepository<CartItem> cartRepo, IRepository<Customer> customerRepo, IAmountCalculator amountCalculator, IShipFeeCalculator shipFeecalculator, UpdateCustomerProcess customerProcess)
        {
            _orderRepo = orderRepo;
            _inventoryRepo = inventoryRepo;
            _cartRepo = cartRepo;
            _customerRepo = customerRepo;
            _amountCalculator = amountCalculator;
            _shipFeecalculator = shipFeecalculator;
            _customerProcess = customerProcess;
        }

        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            //tìm customer/guest
            var customer = await _customerRepo.GetByIdAsync(request.CustomerId);
            if (customer == null)
            {
                //nếu chưa có customer/guest => tạo mới guest
                customer = new Customer() { Id = request.CustomerId };
                await _customerRepo.AddAsync(customer);
            }

            //tạo id đơn hàng
            string orderId = Guid.NewGuid().ToString();

            //khởi tạo đơn hàng
            var order = new Order
            {
                Id = orderId,
                CustomerId = customer.Id,
                CreatedAt = DateTimeOffset.UtcNow,
                CustomerName = request.CustomerName,
                PhoneNumber = request.PhoneNumber,
                ShipFee = 0,
                Items = request.Items?.Select(p => p.ToOrderItem(orderId)).ToList()
            };

            //set giá, kiểm tra tồn kho, xóa cart, tính tổng tiền
            foreach (var item in order.Items)
            {
                var inventory = await _inventoryRepo.GetSingleAsync(p => p.VariantId == item.VariantId && p.SizeId == item.SizeId);
                if (inventory == null)
                    throw new ApplicationException($"Variant {item.VariantId} with SizeId {item.SizeId} is out of stock!");
                if (inventory.Quantity < item.Quantity)
                    throw new ApplicationException($"Variant {item.VariantId} not enough quantity to order!");

                item.Price = inventory.Price ?? inventory.Variant?.Product?.Price ?? 0;

                inventory.Quantity -= item.Quantity;
                _inventoryRepo.Update(inventory);

                var cartItem = await _cartRepo.GetSingleAsync(p=>p.VariantId == item.VariantId && p.SizeId==p.SizeId && p.CustomerId==request.CustomerId);
                if(cartItem != null)
                {
                    _cartRepo.Delete(cartItem);
                }
            }

            await _customerProcess.Excute(customer,order);
            await _amountCalculator.Calculate(order);
            await _shipFeecalculator.CalculateFee(order);
            await _orderRepo.AddAsync(order);
            await _orderRepo.SaveChangesAsync();
            return order.ToOrderDto();
        }
    }
}
