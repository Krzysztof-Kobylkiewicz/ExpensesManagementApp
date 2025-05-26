using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Models.Transaction
{
    public partial class TransactionRepresentant
    {
        public class AccountingDateValidationAttribute : ValidationAttribute
        {
            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                if (validationContext.ObjectInstance is TransactionRepresentant)
                {
                    var transactionRepresentatn = validationContext.ObjectInstance as TransactionRepresentant;

                    if (transactionRepresentatn?.OperationDate > transactionRepresentatn?.AccountingDate)
                        return new ValidationResult("Operation date must be earlier than accounting date.");
                }

                return System.ComponentModel.DataAnnotations.ValidationResult.Success;
            }
        }
    }
}
