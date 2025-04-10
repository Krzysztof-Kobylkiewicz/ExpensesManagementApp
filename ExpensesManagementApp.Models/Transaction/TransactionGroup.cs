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

        [Display(Name = "Transactions")]
        public IList<Transaction> Transactions { get; set; } = [];

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
    }
}
