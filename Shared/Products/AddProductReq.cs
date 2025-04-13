using Shared.Enums;

namespace Shared.Products
{
    public class AddProductReq
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int BoxSize { get; set; } = 1;
        public int BoxWeight { get; set; } = 500;
        public Gender Gender { get; set; } = Gender.Male;
        public bool IsFeatured { get; set; }
        public IEnumerable<AddProductVariantReq>? Variants { get; set; }
    }
}
