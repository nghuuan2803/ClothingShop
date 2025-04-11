namespace Domain.Entities
{
    public class ShippingDetail : DefaultEntity<string>
    {
        public string ShippingId { get; set; } = string.Empty;
        public ShippingInfo? Shipping { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
    }
}
