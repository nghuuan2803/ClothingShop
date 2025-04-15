namespace Domain.Entities
{
    public class Inventory : IEntity
    {
        public int VariantId { get; set; }
        public ProductVariant? Variant { get; set; }
        public int SizeId { get; set; }
        public Size? Size { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
    }
}
