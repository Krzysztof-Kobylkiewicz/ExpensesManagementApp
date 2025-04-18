using Microsoft.EntityFrameworkCore.Storage;

namespace ExpensesManagementApp.Logic.Repositories.TransactionsRepository
{
    public interface ITransactionsRepository
    {
        Task<Models.Transaction.Transaction> GetTransactionAsync(Guid id);
        Task<IEnumerable<Models.Transaction.Transaction>> GetAllTransactionsAsync();
        Task AddTransactionsAsync(IEnumerable<Models.Transaction.Transaction> transactions);
        Task AddTransactionsAsync(IEnumerable<Models.Transaction.Transaction> transactions, IDbContextTransaction dbContextTransaction);
        Task<bool> DeleteTransactionsAsync(Guid id);
        Task<Models.Transaction.TransactionGroup> AssignTransactionsToGroup(Models.Transaction.TransactionGroup transactionGroup);
        Task<IEnumerable<Models.Transaction.TransactionGroup>> GetTransactionGroupsAsync(IEnumerable<Guid?> ids);
        Task<IEnumerable<Models.Transaction.TransactionGroup>> GetTransactionGroupsAsync(IEnumerable<Guid?> ids, IDbContextTransaction dbContextTransaction);
    }
}
