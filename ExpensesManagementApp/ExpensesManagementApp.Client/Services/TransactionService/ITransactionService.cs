using ExpensesManagementApp.Models.HttpResult;
using ExpensesManagementApp.Models.Transaction;

namespace ExpensesManagementApp.Client.Services.TransactionService
{
    public interface ITransactionService
    {
        Task<HttpResult<bool>> DeleteTransactionAsync(Guid id);
        Task<HttpResult<TransactionGroup>> AssignTransactionsToGroup(TransactionGroup transactionGroup);
    }
}
