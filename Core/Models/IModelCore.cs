using Microsoft.AspNetCore.Components;

namespace Core.Models
{
    public interface IModelCore
    {
        public Guid Id { get; set; }
        string GetValidationResultErrorMessages();
        MarkupString GetValidationResultErrorMarkupMessage();
    }
}
