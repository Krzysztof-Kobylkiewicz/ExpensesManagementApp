namespace Core.Models
{
    public abstract class ModelCore<Key> : ModelValidation<Key>, IModelCore<Key> where Key : IEquatable<Key>
    {
        public Key Id { get; set; } = default!;
    }
}