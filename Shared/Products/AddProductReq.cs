using Shared.Contants;
using Shared.Enums;
namespace Shared.Products
{
    public class AddProductReq
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Material { get; set; } = ProductMaterial.Cotton;
        public string Style { get; set; } = ProductStyle.Casual;
        public int CategoryId { get; set; }
        public int? CollectionId { get; set; }
        public int? BrandId { get; set; }
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        public Gender Gender { get; set; } = Gender.Male;
        public bool IsFeatured { get; set; }
        public IEnumerable<AddProductVariantReq>? Variants { get; set; }
    }
}
