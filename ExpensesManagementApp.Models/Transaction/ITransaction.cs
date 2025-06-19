using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Models.Transaction
{
    public interface ITransaction
    {
        Guid Id { get; set; }

        [Required, Display(Name = "Operation date")]
        DateOnly OperationDate { get; set; }

        [Required, Display(Name = "Accounting date")]
        DateOnly AccountingDate { get; set; }

        [Required, DataType(DataType.Currency), Display(Name = "Amount")]
        double Amount { get; set; }

        [Display(Name = "Operation title")]
        string? OperationTitle { get; set; }

        Guid? TransactionGroupId { get; set; }
    }
}
