using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Models.Transaction
{
    public class TransactionGroup : ModelCore<TransactionGroup, Guid>
    {
        public TransactionGroup() 
        {
            TransactionGroupId = Guid.NewGuid();
        }

        public Guid TransactionGroupId { get; set; }

        [Display(Name = "Name of transaction group")]
        public string? TransactionGroupName { get; set; }

        [Display(Name = "Transactions")]
        public IList<Transaction?> Transactions { get; set; } = [];

        [Display(Name = "Representant")]
        public TransactionRepresentant? Representant { get; set; }

        [Display(Name = "Sum of group transactions")]
        public double TransactionGroupSum { get; set; }

        [Display(Name = "Sum of group expenses")]
        public double? TransactionGroupExpensesSum { get; set; }

        [Display(Name = "Sum of group income")]
        public double? TransactionGroupIncomeSum { get; set; }

        [Display(Name = "Number of group transactions")]
        public int NumberOfTransactionsInGroup { get; set; }

        public void Round(int n = 2)
        {
            TransactionGroupSum = Math.Round(Transactions.Select(t => t.Amount).Sum(), n);
            TransactionGroupExpensesSum = Math.Round(Transactions.Where(t => t.Amount < 0).Select(t => t.Amount).Sum(), n);
            TransactionGroupIncomeSum = Math.Round(Transactions.Where(t => t.Amount > 0).Select(t => t.Amount).Sum(), n);
        }

        public int Count()
        {
            NumberOfTransactionsInGroup = Transactions.Count;
            return NumberOfTransactionsInGroup;
        }
    }
}
