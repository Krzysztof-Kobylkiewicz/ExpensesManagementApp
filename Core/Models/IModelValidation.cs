using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public interface IModelValidation
    {
        bool IsValid(IDictionary<object, object?>? validationDict = null);

        IList<ValidationResult> ValidationResult(IDictionary<object, object?>? validationDict = null);
    }
}
