namespace Core.Entities
{
    public interface IEntity<Key> where Key : IEquatable<Key>
    {
        Key Id { get; set; }
        DateTime? UpoloadDate { get; set; }
        DateTime? UpdateDate { get; set; }
        string? IpAddress { get; set; }
    }
}
