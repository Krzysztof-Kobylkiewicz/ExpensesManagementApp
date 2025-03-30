using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Models.Expense
{
    public class Expense
    {
        public Expense() { }
        public Expense(DateOnly operationDate,
                       DateOnly accountingDate,
                       double amount,
                       string? recipent,
                       string? sender,
                       string? operationTitle,
                       string? senderAccountNumber,
                       int operationNumber)
        {
            OperationDate = operationDate;
            AccountingDate = accountingDate;
            Amount = amount;
            Recipient = recipent;
            Sender = sender;
            OperationTitle = operationTitle;
            SenderAccountNumber = senderAccountNumber;
            OperationNumber = operationNumber;
        }

        public Guid Id { get; set; }
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

        public bool CompareExpenses(Expense expenseToCompare)
        {
            if (Amount != expenseToCompare.Amount)
                return false;

            if (Sender != expenseToCompare.Sender)
                return false;

            if (Recipient != expenseToCompare.Recipient)
                return false;

            if (OperationTitle != expenseToCompare.OperationTitle)
                return false;

            if (SenderAccountNumber != expenseToCompare.SenderAccountNumber)
                return false;

            return true;
        }

        public bool CompareExpensesWithTheSameDate(Expense expenseToCompare)
        {
            if (OperationDate != expenseToCompare.OperationDate)
                return false;

            return CompareExpenses(expenseToCompare);
        }
    }
}
