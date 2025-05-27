using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Models.Transaction
{
    public partial class TransactionRepresentant : ModelCore<Guid>, ITransaction
    {
        public TransactionRepresentant() { }

        [Display(Name = "Operation date"), Required(ErrorMessage = RequiredMessage)]
        public DateOnly OperationDate { get; set; }

        [Display(Name = "Accounting date"), Required(ErrorMessage = RequiredMessage), AccountingDateValidation]
        public DateOnly AccountingDate { get; set; }

        [Display(Name = "Amount"), Required(ErrorMessage = RequiredMessage), DataType(DataType.Currency)]
        public double Amount { get; set; }

        [Display(Name = "Operation title")]
        public string? OperationTitle { get; set; }

        public Guid? TransactionGroupId { get; set; }
    }
}
