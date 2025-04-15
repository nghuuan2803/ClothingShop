using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Size : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
