namespace Domain.Entities
{
    public class Size : DefaultEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        //public List<ProductVariant> ProductVariants { get; set; }
    }
}
