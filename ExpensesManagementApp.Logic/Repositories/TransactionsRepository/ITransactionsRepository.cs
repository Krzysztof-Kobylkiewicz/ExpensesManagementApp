using ExpensesManagementApp.Database.Filters;
using Microsoft.EntityFrameworkCore.Storage;

namespace ExpensesManagementApp.Logic.Repositories.TransactionsRepository
{
    public interface ITransactionsRepository
    {
        Task<Models.Transaction.Transaction?> GetTransactionAsync(Guid id);
        Task<IEnumerable<Models.Transaction.Transaction?>> GetAllTransactionsAsync();
        Task<IEnumerable<Models.Transaction.Transaction?>> GetSpecificTransactionsAsync(DbTransactionFilter filter);
        Task AddTransactionsAsync(IEnumerable<Models.Transaction.Transaction> transactions, IDbContextTransaction? parentDbContextTransaction = null);
        Task<IEnumerable<Models.Transaction.Transaction>> ValidateTransactionsAsync(IEnumerable<Models.Transaction.Transaction> transactions);
        Task<bool> DeleteTransactionsAsync(Guid id);
        Task<Models.Transaction.TransactionGroup> AssignTransactionsToGroup(Models.Transaction.TransactionGroup transactionGroup);
    }
}
