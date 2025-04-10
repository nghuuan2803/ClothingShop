namespace Domain.Entities
{
    public class Tag : DefaultEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<ProductTag>? ProductTags { get; set; }
    }
}
