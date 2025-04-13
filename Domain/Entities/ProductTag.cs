namespace Domain.Entities
{
    public class ProductTag : IEntity
    {
        public int ProductId { get; set; }
        public int TagId { get; set; }
        public Product? Product { get; set; }
        public Tag? Tag { get; set; }
    }
}
