using Shared.Enums;

namespace Shared.Orders
{
    public class OrderDto
    {
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public string? CustomerName { get; set; }
        public string? PhoneNumber { get; set; }
        public string ShippingAdress { get; set; } = string.Empty;
        public PaymentMethod PaymentMethod { get; set; }
        public string? PaymentMethodName { get; set; }
        public decimal Discount { get; set; }
        public decimal ShipFee { get; set; }
        public decimal Amount { get; set; }
        public int? PromotionId { get; set; }
        public string? VoucherId { get; set; }
        public OrderStatus Status { get; set; }
        public string? HandlerId { get; set; }
        public string? Handler { get; set; }

        public IEnumerable<OrderItemDto>? Items { get; set; }
    }

    public class OrderItemDto
    {
        public int VariantId { get; set; }
        public string? Variant { get; set; }
        public int SizeId { get; set; }
        public string? Size { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
