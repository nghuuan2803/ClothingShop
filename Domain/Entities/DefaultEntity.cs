using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public abstract class DefaultEntity<TKey> : IEntity
    {
        [Key]
        public TKey Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
