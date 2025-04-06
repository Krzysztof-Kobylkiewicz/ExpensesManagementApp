using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Models.Transaction
{
    public class Transaction
    {
        public Transaction() { }

        public Guid TransactionId { get; set; }
        [Required, Display(Name = "Operation date")]
        public DateOnly OperationDate { get; set; }
        [Required, Display(Name = "Accounting date")]
        public DateOnly AccountingDate { get; set; }
        [Required, DataType(DataType.Currency), Display(Name = "Amount")]
        public double Amount { get; set; }
        [Display(Name = "Recipent")]
        public string? Recipient { get; set; }
        [Display(Name = "Sender")]
        public string? Sender { get; set; }
        [Display(Name = "Operation title")]
        public string? OperationTitle { get; set; }
        [Display(Name = "Sender account number")]
        public string? SenderAccountNumber { get; set; }
        [Display(Name = "Operation number")]
        public int OperationNumber { get; set; }
        public Guid? TransactionGroupId { get; set; }

        public bool CompareTransactions(Transaction transactionToCompare)
        {
            if (Amount != transactionToCompare.Amount)
                return false;

            if (Sender != transactionToCompare.Sender)
                return false;

            if (Recipient != transactionToCompare.Recipient)
                return false;

            if (OperationTitle != transactionToCompare.OperationTitle)
                return false;

            if (SenderAccountNumber != transactionToCompare.SenderAccountNumber)
                return false;

            return true;
        }

        public bool CompareTransactionsWithTheSameDate(Transaction transactionToCompare)
        {
            if (OperationDate != transactionToCompare.OperationDate)
                return false;

            return CompareTransactions(transactionToCompare);
        }
    }
}
