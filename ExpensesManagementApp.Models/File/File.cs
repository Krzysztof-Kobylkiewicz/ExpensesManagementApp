﻿using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Models.File
{
    public class File
    {
        public File() { }

        [Display(Name = "File ID")]
        public Guid FileId { get; set; }

        [Display(Name = "File name")]
        public string? FileName { get; set; }

        [Display(Name = "File size")]
        public long? FileSize { get; set; }

        [Display(Name = "Transactions")]
        public IEnumerable<Transaction.Transaction> Transactions { get; set; } = [];

        [Display(Name = "Bank type"), Required]
        public BankTypeEnum? BankType { get; set; }

        public bool IsUploadingAllowed()
        {
            if (string.IsNullOrEmpty(FileName))
                return false;

            if (FileSize == 0)
                return false;

            if (BankType == null)
                return false;

            return true;
        }

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
                TransactionId = e.TransactionId,
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
