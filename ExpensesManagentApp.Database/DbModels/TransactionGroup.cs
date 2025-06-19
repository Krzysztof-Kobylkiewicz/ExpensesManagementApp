using Core.Entities;

namespace ExpensesManagementApp.Database.DbModels
{
    public class TransactionGroup : Entity<TransactionGroup, Models.Transaction.TransactionGroup, Guid>, IConvertable<TransactionGroup, Models.Transaction.TransactionGroup, Guid>
    {
        public TransactionGroup() { }

        public string? TransactionGroupName { get; set; }
        public double TransactionGroupSum { get; set; }
        public double TransactionGroupExpensesSum { get; set; }
        public double TransactionGroupIncomeSum { get; set; }
        public string? JsonGroupRepresentant { get; set; }

        public ICollection<Transaction>? Transactions { get; set; }

        public Models.Transaction.TransactionGroup ConvertEntityToDTO() => new()
        {
            TransactionGroupId = this.Id,
            TransactionGroupName = this.TransactionGroupName,
            TransactionGroupSum = this.TransactionGroupSum,
            TransactionGroupExpensesSum = this.TransactionGroupExpensesSum,
            TransactionGroupIncomeSum = this.TransactionGroupIncomeSum,
            NumberOfTransactionsInGroup = this?.Transactions?.Count() ?? 0,
            Transactions = this?.Transactions?.Select(t => t.ConvertEntityToDTO()).ToList() ?? [],
            Representant = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Transaction.TransactionRepresentant?>(this?.JsonGroupRepresentant ?? string.Empty),
        };

        public static TransactionGroup ConvertDTOToEntity(Models.Transaction.TransactionGroup transactionGroup) => new()
        {
            Id = transactionGroup.TransactionGroupId,
            TransactionGroupName = transactionGroup.TransactionGroupName,
            TransactionGroupSum = transactionGroup.TransactionGroupSum,
            TransactionGroupExpensesSum = transactionGroup.TransactionGroupExpensesSum.HasValue ? transactionGroup.TransactionGroupExpensesSum.Value : 0,
            TransactionGroupIncomeSum = transactionGroup.TransactionGroupIncomeSum.HasValue ? transactionGroup.TransactionGroupIncomeSum.Value : 0,
            JsonGroupRepresentant = Newtonsoft.Json.JsonConvert.SerializeObject(transactionGroup.Representant)
        };
    }
}
