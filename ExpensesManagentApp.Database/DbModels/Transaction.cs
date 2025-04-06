using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Database.DbModels
{
    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; }
        public DateOnly OperationDate { get; set; }
        public DateOnly AccountingDate { get; set; }
        [DataType(DataType.Currency)]
        public double Amount { get; set; }
        public string? Recipient { get; set; }
        public string? Sender { get; set; }
        public string? OperationTitle { get; set; }
        public string? SenderAccountNumber { get; set; }
        public int OperationNumber { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

        public Guid? FileId { get; set; }
        public File? File { get; set; }

        public Guid? TransactionGroupId { get; set; }
        public TransactionGroup? TransactionGroup { get; set; }

        public static Transaction ConvertToDbTransaction(Models.Transaction.Transaction transaction)
        {
            return new Transaction
            {
                TransactionId = transaction.TransactionId,
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
                TransactionId = transaction.TransactionId,
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
