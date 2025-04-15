using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Brand : IEntity
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
    }
}
