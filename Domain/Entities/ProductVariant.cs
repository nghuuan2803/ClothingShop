
namespace Domain.Entities
{
    public class ProductVariant : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int SizeId { get; set; }
        public Size? Size { get; set; }
        public int ColorId { get; set; }
        public Color? Color { get; set; }
        public string? ImageUrls { get; set; }
        public IEnumerable<string>? Images { get; set; } = [];
        public virtual ICollection<Inventory>? Inventories { get; set; } // Tồn kho theo chi nhánh
    }
}
