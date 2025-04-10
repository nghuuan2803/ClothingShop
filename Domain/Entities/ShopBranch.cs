namespace Domain.Entities
{
    public class ShopBranch : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Location { get; set; }
        public ICollection<Inventory>? Inventories { get; set; }
    }
}
