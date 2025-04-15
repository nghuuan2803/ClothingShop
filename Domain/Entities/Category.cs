using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Category : IEntity
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
        public virtual ICollection<Category>? SubCategories { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
