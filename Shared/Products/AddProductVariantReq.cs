namespace Shared.Products
{
    public class AddProductVariantReq
    {
        public int ColorId { get; set; }
        public string? ImageUrls { get; set; }
        public Dictionary<int, int> Stock { get; set; } = [];
    }
}
