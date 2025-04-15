using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities
{
    public class ProductVariant : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int ColorId { get; set; }
        public Color? Color { get; set; }
        public string? ImageUrls { get; set; }
        public bool IsDeleted { get; set; }
        [NotMapped]
        public IEnumerable<string>? Images { get; set; } = [];
        public virtual ICollection<Inventory>? Inventories { get; set; } //tồn kho theo size
    }
}
