using Microsoft.AspNetCore.Components;

namespace Core.Models
{
    public abstract class ModelCore : ModelValidation, IModelCore
    {
        public Guid Id { get; set; } = Guid.Empty;

        public string GetValidationResultErrorMessages() => string.Join(" ", this.ValidationResult());
        public MarkupString GetValidationResultErrorMarkupMessage() => new MarkupString(string.Join("<br />", this.ValidationResult()));
        
    }
}
