namespace Core.Entities
{
    public abstract class Entity<Key> : IEntity<Key> where Key : IEquatable<Key>
    {
        public Key Id { get; set; } = default!;
        public DateTime? UpoloadDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? IpAddress { get; set; }
    }
}
