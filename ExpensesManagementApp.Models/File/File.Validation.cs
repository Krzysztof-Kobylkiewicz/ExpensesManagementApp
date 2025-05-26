using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Models.File
{
    public partial class File
    {
        public class TransactionsValidationAttribute : ValidationAttribute
        {
            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                if (validationContext.ObjectInstance is File)
                {
                    var file = validationContext.ObjectInstance as File;

                    if (file?.Transactions.Count() == 0)
                        return new System.ComponentModel.DataAnnotations.ValidationResult("File must have at least 1 transaction.", [validationContext.MemberName ?? "transactions"]);
                }

                return System.ComponentModel.DataAnnotations.ValidationResult.Success;
            }
        }
    }
}
