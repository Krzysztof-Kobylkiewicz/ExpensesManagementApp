namespace Core.Models
{
    public abstract class ModelCore : ModelValidation, IModelCore
    {
        public Guid Id { get; set; } = Guid.Empty;
    }
}
