using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Models.Transaction
{
    public class TransactionGroup
    {
        public TransactionGroup() 
        {
            TransactionGroupId = Guid.NewGuid();
        }

        public Guid TransactionGroupId { get; set; }

        [Display(Name = "Name of transaction group")]
        public string? TransactionGroupName { get; set; }

        public IList<Transaction> Transactions { get; set; } = [];

        public double TransactionGroupSum { get; set; }

        public double? TransactionGroupExpensesSum { get; set; }

        public double? TransactionGroupIncomeSum { get; set; }

        public int NumberOfTransactionsInGroup { get; set; }
    }
}
