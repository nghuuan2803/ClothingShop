namespace Domain.Entities
{
    public class CartItem
    {
        public string CustomerId { get; set; } = string.Empty;
        public int ProductVariantId { get; set; }
        public ProductVariant? ProductVariant { get; set; }
        public int Quantity { get; set; }
    }
}
