namespace Core.Models
{
    public abstract class ModelCore<DTO, Key> : ModelValidation<DTO, Key>, IModelCore<DTO, Key> where Key : IEquatable<Key>
    {
        public Key Id { get; set; } = default!;

        public virtual bool Equals(DTO other) => throw new NotImplementedException("Model does not have its own implementation of Equals() method."); 
    }
}