namespace Core.Models
{
    public interface IModelCore<DTO, Key> where Key : IEquatable<Key>
    {
        public Key Id { get; set; }

        public bool Equals(DTO other);
    }
}
