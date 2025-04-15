using Shared.Enums;

namespace Shared.Orders
{
    public class CreateOrderReq
    {
        public string CustomerId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public IEnumerable<OrderItemReq>? Items { get; set; } = [];
    }

    public class OrderItemReq
    {
        public int VariantId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
