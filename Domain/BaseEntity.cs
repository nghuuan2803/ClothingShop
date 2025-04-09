namespace Domain
{
    public abstract class BaseEntity<TKey> : DefaultEntity<TKey>
    {
        public string? CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
