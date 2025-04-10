namespace Domain.Entities
{
    public class Order : BaseEntity<string>
    {
        public decimal Amount { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public Customer? Customer { get; set; }
        public string? VoucherId { get; set; }
        public int? PromotionId { get; set; }
        public int PaymentMethod { get; set; }
        public int ShippingId { get; set; }
        public int Status { get; set; }
    }
}
