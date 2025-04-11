namespace Domain.Entities
{
    public class Color : DefaultEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string? HexCode { get; set; } = string.Empty;
    }
}
