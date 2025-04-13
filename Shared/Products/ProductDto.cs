using Shared.Contants;
using Shared.Enums;

namespace Shared.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int BoxSize { get; set; } = 1;
        public int BoxWeight { get; set; } = 500;
        public Gender Gender { get; set; } = Gender.Male;
        public bool IsFeatured { get; set; } // Cho carousel sản phẩm nổi bật
        public IEnumerable<string>? Images { get; set; } = [];
        public IEnumerable<TagDto>? Tags { get; set; } = [];
        public IEnumerable<VariantDto>? Variants { get; set; } = [];

    }
    public class VariantDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Size { get; set; } = ProductSize.S;
        public int ColorId { get; set; }
        public string? ColorName { get; set; }
        public string? ColorHex { get; set; }
        public string? ImageUrls { get; set; }
        public IEnumerable<string>? Images { get; set; } = [];
        public IEnumerable<InventoryDto>? Inventories { get; set; } = [];
    }
    public class InventoryDto
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int Quantity { get; set; }
    }

    public class TagDto
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
    }
}
