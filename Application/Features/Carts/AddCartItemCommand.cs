using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Carts
{
    public class AddCartItemCommand : IRequest<string>
    {
        //CustomerId or guestId
        public string CustomerId { get; set; }
        public int VariantId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
    }

    public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand, string>
    {
        private readonly IRepository<CartItem> _cartRepo;
        private readonly IRepository<ProductVariant> _variantRepo;
        private readonly IRepository<Customer> _customerRepo;

        public AddCartItemCommandHandler(IRepository<CartItem> cartRepo, IRepository<ProductVariant> variantRepo, IRepository<Customer> customerRepo)
        {
            _cartRepo = cartRepo;
            _variantRepo = variantRepo;
            _customerRepo = customerRepo;
        }

        public async Task<string> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepo.GetByIdAsync(request.CustomerId);
            if (customer == null)
            {
                customer = new Customer
                {
                    Id = request.CustomerId,
                };
                await _customerRepo.AddAsync(customer);
            }
            var variant = await _variantRepo.GetSingleAsync(p => p.Id == request.VariantId);
            if (variant == null)
            {
                return "Variant not found";
            }
            if (variant.IsDeleted || variant.Product.IsDeleted)
            {
                return "Variant is deleted";
            }

            var inventory = variant.Inventories?.FirstOrDefault(p => p.SizeId == request.SizeId);
            if (inventory == null)
            {
                return "This size is out of stock";
            }

            var cartItem = await _cartRepo.GetSingleAsync(p => p.VariantId == request.VariantId &&
                                            p.CustomerId == request.CustomerId &&
                                            p.SizeId == request.SizeId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    VariantId = request.VariantId,
                    CustomerId = request.CustomerId,
                    SizeId = request.SizeId,
                    Quantity = request.Quantity,
                };
                await _cartRepo.AddAsync(cartItem);
            }
            else
            {
                cartItem.Quantity += request.Quantity;
                _cartRepo.Update(cartItem);
            }
            if (cartItem.Quantity < 1)
            {
                _cartRepo.Delete(cartItem);
            }
            if(inventory.Quantity < cartItem.Quantity)
            {
                return "Quantity not enough";
            }
            await _cartRepo.SaveChangesAsync();
            return "Success";
        }
    }
}
