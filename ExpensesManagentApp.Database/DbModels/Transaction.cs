using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Database.DbModels
{
    public class Transaction : Entity<Guid>
    {
        public DateOnly OperationDate { get; set; }
        public DateOnly AccountingDate { get; set; }
        [DataType(DataType.Currency)]
        public double Amount { get; set; }
        public string? Recipient { get; set; }
        public string? Sender { get; set; }
        public string? OperationTitle { get; set; }
        public string? SenderAccountNumber { get; set; }
        public int OperationNumber { get; set; }

        //Many to one relationship with file
        public Guid? FileId { get; set; }
        public File? File { get; set; }

        //Many to one relationship with transaction group
        public Guid? TransactionGroupId { get; set; }
        public TransactionGroup? TransactionGroup { get; set; }

        //Many to one relationship with category
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }

        public static Transaction ConvertToDbTransaction(Models.Transaction.Transaction transaction)
        {
            return new Transaction
            {
                Id = transaction.Id,
                OperationDate = transaction.OperationDate,
                AccountingDate = transaction.AccountingDate,
                Amount = transaction.Amount,
                Recipient = transaction.Recipient,
                Sender = transaction.Sender,
                OperationTitle = transaction.OperationTitle,
                SenderAccountNumber = transaction.SenderAccountNumber,
                OperationNumber = transaction.OperationNumber
            };
        }

        public static Models.Transaction.Transaction ConvertToTransactionDTO(Transaction transaction)
        {
            return new Models.Transaction.Transaction
            {
                Id = transaction.Id,
                OperationDate = transaction.OperationDate,
                AccountingDate = transaction.AccountingDate,
                Amount = transaction.Amount,
                Recipient = transaction.Recipient,
                Sender = transaction.Sender,
                OperationTitle = transaction.OperationTitle,
                SenderAccountNumber = transaction.SenderAccountNumber,
                OperationNumber = transaction.OperationNumber,
                TransactionGroupId = transaction.TransactionGroupId
            };
        }
    }
}
