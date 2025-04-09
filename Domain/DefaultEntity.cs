namespace Domain
{
    public abstract class DefaultEntity<TKey> : IEntity
    {
        public TKey Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
