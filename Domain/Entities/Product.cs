namespace Domain.Entities
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int BoxSize { get; set; } = 1;
        public int BoxWeight { get; set; } = 500;
        public Gender Gender { get; set; } = Gender.Male;
        public Category? Category { get; set; }
        public bool IsFeatured { get; set; } // Cho carousel sản phẩm nổi bật
        public IEnumerable<string>? Images { get; set; } = [];
        public virtual ICollection<ProductTag>? ProductTags { get; set; }
        public virtual ICollection<ProductVariant>? Variants { get; set; } // Danh sách biến thể
    }
}
