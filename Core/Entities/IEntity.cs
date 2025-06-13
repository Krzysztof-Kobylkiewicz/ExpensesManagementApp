using Core.Models;

namespace Core.Entities
{
    public interface IEntity<E, DTO, Key> where E : Entity<E, DTO, Key> where DTO : IModelCore<Key> where Key : IEquatable<Key>
    {
        Key Id { get; set; }
        DateTime? UpoloadDate { get; set; }
        DateTime? UpdateDate { get; set; }
        string? IpAddress { get; set; }
        public string? Login { get; set; }
    }
}
