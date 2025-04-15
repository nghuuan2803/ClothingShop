using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Color : IEntity
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? HexCode { get; set; } = string.Empty;
    }
}
