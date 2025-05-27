namespace Core.Models
{
    public interface IModelCore<Key> where Key : IEquatable<Key>
    {
        public Key Id { get; set; }
    }
}
