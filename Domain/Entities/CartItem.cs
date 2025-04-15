namespace Domain.Entities
{
    public class CartItem : IEntity
    {
        public string? CustomerId { get; set; } = string.Empty;
        public Customer? Customer { get; set; }
        public int VariantId { get; set; }
        public ProductVariant? Variant { get; set; }
        public int SizeId { get; set; }
        public Size? Size { get; set; }
        public int Quantity { get; set; }
    }
}
