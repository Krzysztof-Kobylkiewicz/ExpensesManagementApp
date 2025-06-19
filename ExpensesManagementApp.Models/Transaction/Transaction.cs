using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Models.Transaction
{
    public class Transaction : TransactionRepresentant, ITransaction, IModelCore<Transaction, Guid>
    {
        public Transaction() : base() { }

        [Display(Name = "Recipent")]
        public string? Recipient { get; set; }

        [Display(Name = "Sender")]
        public string? Sender { get; set; }

        [Display(Name = "Sender account number")]
        public string? SenderAccountNumber { get; set; }

        [Display(Name = "Operation number")]
        public int OperationNumber { get; set; }

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

        public bool Equals(Transaction other)
        {
            if (!OperationDate.Equals(other.OperationDate))
                return false;

            if (!AccountingDate.Equals(other.AccountingDate))
                return false;

            if (Amount != other.Amount)
                return false;

            if (OperationTitle?.Equals(other.OperationTitle) == false)
                return false;

            if (!TransactionGroupId.Equals(other.TransactionGroupId))
                return false;

            if (Recipient?.Equals(other.Recipient) == false)
                return false;

            if (Sender?.Equals(other.Sender) == false)
                return false;

            if (SenderAccountNumber?.Equals(other.SenderAccountNumber) == false)
                return false;

            return true;
        }
    }
}
