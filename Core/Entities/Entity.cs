using Core.Models;

namespace Core.Entities
{
    public abstract class Entity<E, DTO, Key> : IEntity<E, DTO, Key> where E : Entity<E, DTO, Key> where DTO : IModelCore<Key> where Key : IEquatable<Key>
    {
        public Key Id { get; set; } = default!;
        public DateTime? UpoloadDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? IpAddress { get; set; }
        public string? Login { get; set; }
    }
}
