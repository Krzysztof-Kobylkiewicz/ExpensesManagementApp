using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Models.File
{
    public partial class File : ModelCore
    {
        public File() { }

        [Display(Name = "File name"), Required(ErrorMessage = RequiredMessage)]
        public string? FileName { get; set; }

        [Display(Name = "File size"), Required(ErrorMessage = RequiredMessage)]
        public long? FileSize { get; set; }

        [Display(Name = "Transactions"), TransactionsValidation]
        public IEnumerable<Transaction.Transaction> Transactions { get; set; } = [];

        [Display(Name = "Bank type"), Required(ErrorMessage = RequiredMessage)]
        public BankTypeEnum? BankType { get; set; }

        public static string BankTypeText(BankTypeEnum bankType)
        {
            switch (bankType)
            {
                case BankTypeEnum.MBANK:
                    return "mBank";

                case BankTypeEnum.SANTANDER:
                    return "Santander";

                case BankTypeEnum _:
                    throw new NotImplementedException();
            }
        }

        public static IEnumerable<Transaction.Transaction> AssignSenderOrRecipent(IEnumerable<Transaction.Transaction> expenses)
        {
            return expenses.Select(e => new Transaction.Transaction
            {
                Id = e.Id,
                AccountingDate = e.AccountingDate,
                OperationDate = e.OperationDate,
                Amount = e.Amount,
                OperationTitle = e.OperationTitle,
                SenderAccountNumber = e.SenderAccountNumber,
                OperationNumber = e.OperationNumber,
                Recipient = e.Amount > 0 ? string.Empty : e.Recipient,
                Sender = e.Amount > 0 ? e.Recipient : string.Empty
            });
        }
    }
}
