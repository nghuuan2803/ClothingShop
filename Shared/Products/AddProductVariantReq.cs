using Shared.Contants;

namespace Shared.Products
{
    public class AddProductVariantReq
    {
        public int ProductId { get; set; }
        public string Size { get; set; } = ProductSize.S;
        public int ColorId { get; set; }
        public string? ImageUrls { get; set; }
    }
}
