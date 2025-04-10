namespace Domain.Entities
{
    public class Banner : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string? DesignBy { get; set; } = string.Empty;
        public string? Link { get; set; } = string.Empty;
        public bool Showing { get; set; }
    }
}
