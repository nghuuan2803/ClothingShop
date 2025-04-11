namespace Domain.Entities
{
    public class ShippingDetail : DefaultEntity<int>
    {
        public int ShippingId { get; set; }
        public ShippingInfo? Shipping { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
    }
}
