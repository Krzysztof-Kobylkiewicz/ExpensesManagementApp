using ExpensesManagementApp.Models.HttpResult;
using ExpensesManagementApp.Models.Transaction;

namespace ExpensesManagementApp.Client.Services.TransactionService
{
    public interface ITransactionService
    {
        Task<HttpResult<Transaction>> GetTransactionAsync(Guid id);
        Task<HttpResult<IEnumerable<Transaction>>> GetAllTransactionsAsync();
        Task<HttpResult<IEnumerable<Transaction>>> GetSpecificTransactionsAsync(ITransactionFiltr transactionFiltr);
        Task<HttpResult<bool>> DeleteTransactionAsync(Guid id);
        Task<HttpResult<TransactionGroup>> AssignTransactionsToGroup(TransactionGroup transactionGroup);
    }
}
