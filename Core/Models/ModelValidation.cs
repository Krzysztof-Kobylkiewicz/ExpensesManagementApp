using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public abstract class ModelValidation<DTO, Key> : IModelValidation<Key>
    {
        public const string RequiredMessage = "{0} is required.";
        public const string MaxLengthMessage = "{0} max allowed length is {1}.";
        public const string GreaterThanMessage = "{0} must be greater than {1}.";

        public virtual bool IsValid(IDictionary<object, object?>? validationDict = null)
        {
            return ValidationResult().Count == 0;
        }

        public virtual IList<ValidationResult> ValidationResult(IDictionary<object, object?>? validationDict = null)
        {
            IList<ValidationResult> validationResults = [];

            try
            {
                Validator.TryValidateObject(this, new ValidationContext(this, null, validationDict), validationResults, true);

                return validationResults;
            }
            catch
            {
                validationResults.Add(new ValidationResult("Unhandeled error during validation"));
                return validationResults;
            }
        }

        public string GetValidationResultErrorMessages() => string.Join(" ", this.ValidationResult());
        public MarkupString GetValidationResultErrorMarkupMessage() => new(string.Join("<br />", this.ValidationResult()));
    }
}
