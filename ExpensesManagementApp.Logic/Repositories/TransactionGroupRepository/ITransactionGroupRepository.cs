using Microsoft.EntityFrameworkCore.Storage;

namespace ExpensesManagementApp.Logic.Repositories.TransactionGroupRepository
{
    public interface ITransactionGroupRepository
    {
        Task<IEnumerable<Models.Transaction.TransactionGroup?>> GetTransactionGroupsAsync(IEnumerable<Guid?> ids, IDbContextTransaction? parentDbContextTransaction = null);
        Task<Models.Transaction.TransactionGroup> AddAsync(Models.Transaction.TransactionGroup transactionGroup);
        Task<bool> DeleteTransactionGroupAsync(Guid id);
    }
}
