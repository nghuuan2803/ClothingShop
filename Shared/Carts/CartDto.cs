namespace Shared.Carts
{
    public class CartDto
    {
        public string CustomerId { get; set; }
        public IEnumerable<CartItemDto>? Items { get; set; } = [];
    }

    public class CartItemDto
    {
        public int VariantId { get; set; }
        public string? Product { get; set; }
        public string? Color { get; set; }
        public int SizeId { get; set; }
        public string? Size { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
