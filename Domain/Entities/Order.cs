using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order : BaseEntity<string>
    {
        public string CustomerId { get; set; } = string.Empty;
        public string? CustomerName { get; set; }
        public string? PhoneNumber { get; set; }
        public string ShippingAdress { get; set; } = string.Empty;
        public Customer? Customer { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Discount { get; set; }
        public decimal ShipFee { get; set; }
        public decimal Amount { get; set; }
        public int? PromotionId { get; set; }
        public string? VoucherId { get; set; }
        public OrderStatus Status { get; set; }
        public string? HandlerId { get; set; }
        [NotMapped]
        public IUser? Handler { get; set; }
    }
}
