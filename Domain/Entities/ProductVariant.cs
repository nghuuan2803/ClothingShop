using Domain.ValueObjects;
namespace Domain.Entities
{
    public class ProductVariant : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public string Size { get; set; } = ProductSize.S;
        public int ColorId { get; set; }
        public Color? Color { get; set; }
        public string? ImageUrls { get; set; }
        public IEnumerable<string>? Images { get; set; } = [];
        public virtual ICollection<Inventory>? Inventories { get; set; } // Tồn kho theo chi nhánh
    }
}
