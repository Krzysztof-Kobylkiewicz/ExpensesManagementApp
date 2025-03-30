using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Database.DbModels
{
    public class Expense
    {
        [Key]
        public Guid Id { get; set; }
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

        public Guid FileId { get; set; }
        public File? File { get; set; }

        public static Expense ConvertToDbExpense(Models.Expense.Expense expense)
        {
            return new Expense
            {
                OperationDate = expense.OperationDate,
                AccountingDate = expense.AccountingDate,
                Amount = expense.Amount,
                Recipient = expense.Recipient,
                Sender = expense.Sender,
                OperationTitle = expense.OperationTitle,
                SenderAccountNumber = expense.SenderAccountNumber,
                OperationNumber = expense.OperationNumber
            };
        }

        public static Models.Expense.Expense ConvertToExpenseDTO(Expense expense)
        {
            return new Models.Expense.Expense
            {
                OperationDate = expense.OperationDate,
                AccountingDate = expense.AccountingDate,
                Amount = expense.Amount,
                Recipient = expense.Recipient,
                Sender = expense.Sender,
                OperationTitle = expense.OperationTitle,
                SenderAccountNumber = expense.SenderAccountNumber,
                OperationNumber = expense.OperationNumber
            };
        }
    }
}
