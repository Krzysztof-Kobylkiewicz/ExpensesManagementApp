using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Models.Transaction
{
    public class TransactionRepresentant : ITransaction
    {
        public TransactionRepresentant() { }

        public Guid TransactionId { get; set; }

        [Required, Display(Name = "Operation date")]
        public DateOnly OperationDate { get; set; }

        [Required, Display(Name = "Accounting date")]
        public DateOnly AccountingDate { get; set; }

        [Required, DataType(DataType.Currency), Display(Name = "Amount")]
        public double Amount { get; set; }

        [Display(Name = "Operation title")]
        public string? OperationTitle { get; set; }

        public Guid? TransactionGroupId { get; set; }
    }
}
