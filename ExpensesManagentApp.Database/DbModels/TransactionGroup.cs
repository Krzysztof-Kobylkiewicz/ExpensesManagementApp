﻿namespace ExpensesManagementApp.Database.DbModels
{
    public class TransactionGroup : DbModel
    {
        public TransactionGroup() { }

        public string? TransactionGroupName { get; set; }
        public double TransactionGroupSum { get; set; }
        public double TransactionGroupExpensesSum { get; set; }
        public double TransactionGroupIncomeSum { get; set; }
        public string? JsonGroupRepresentant { get; set; }

        public ICollection<Transaction>? Transactions { get; set; }

        public static TransactionGroup ConvertToDbTransactionGroup(Models.Transaction.TransactionGroup transactionGroup)
        {
            return new TransactionGroup
            {
                Id = transactionGroup.TransactionGroupId,
                TransactionGroupName = transactionGroup.TransactionGroupName,
                TransactionGroupSum = transactionGroup.TransactionGroupSum,
                TransactionGroupExpensesSum = transactionGroup.TransactionGroupExpensesSum.HasValue ? transactionGroup.TransactionGroupExpensesSum.Value : 0,
                TransactionGroupIncomeSum = transactionGroup.TransactionGroupIncomeSum.HasValue ? transactionGroup.TransactionGroupIncomeSum.Value : 0,
                JsonGroupRepresentant = Newtonsoft.Json.JsonConvert.SerializeObject(transactionGroup.Representant)
            };
        }

        public static Models.Transaction.TransactionGroup ConvertToTransactionGroupDTO(TransactionGroup transactionGroup)
        {
            return new Models.Transaction.TransactionGroup
            {
                TransactionGroupId = transactionGroup.Id,
                TransactionGroupName = transactionGroup.TransactionGroupName,
                TransactionGroupSum = transactionGroup.TransactionGroupSum,
                TransactionGroupExpensesSum = transactionGroup.TransactionGroupExpensesSum,
                TransactionGroupIncomeSum = transactionGroup.TransactionGroupIncomeSum,
                NumberOfTransactionsInGroup = transactionGroup?.Transactions?.Count() ?? 0,
                Transactions = transactionGroup?.Transactions?.Select(t => Transaction.ConvertToTransactionDTO(t)).ToList() ?? [],
                Representant = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Transaction.TransactionRepresentant?>(transactionGroup?.JsonGroupRepresentant ?? string.Empty),
            };
        }
    }
}
