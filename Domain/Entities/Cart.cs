namespace Domain.Entities
{
    public class Cart
    {
        public string CustomerId { get; set; } = string.Empty;
        public ICollection<CartItem>? Items { get; set; }
    }
}
