using Shared.Enums;

namespace Shared.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public string? Style { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        public int CategoryId { get; set; }
        public string? Category { get; set; }
        public int? CollectionId { get; set; }
        public string? Collection { get; set; }
        public int? BrandId { get; set; }
        public string? Brand { get; set; }
        public int BoxSize { get; set; } = 1;
        public int BoxWeight { get; set; } = 500;
        public Gender Gender { get; set; } = Gender.Male;
        public bool IsFeatured { get; set; } // Cho carousel sản phẩm nổi bật
        public IEnumerable<string>? Images { get; set; } = [];
        public IEnumerable<VariantDto>? Variants { get; set; } = [];

    }
    public class VariantDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public string? Color { get; set; }
        public string? ColorHex { get; set; }
        public string? ImageUrls { get; set; }
        public IEnumerable<string>? Images { get; set; } = [];
        public IEnumerable<InventoryDto>? Inventories { get; set; } = [];
    }
    public class InventoryDto
    {
        public int SizeId { get; set; }
        public string? Size { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
    }
}
