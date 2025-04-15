namespace Domain.Entities
{
    public class ProductCollection : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
