using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ShopBranch : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Location { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? ManagerId { get; set; }
        [NotMapped]
        public IUser? Manager { get; set; }
        public ICollection<Inventory>? Inventories { get; set; }
        public bool IsDeleted { get; set; }
    }
}
