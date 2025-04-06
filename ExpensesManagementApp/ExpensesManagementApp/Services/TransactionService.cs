using ExpensesManagementApp.Client.Services.TransactionService;
using ExpensesManagementApp.Logic.Repositories.TransactionsRepository;
using ExpensesManagementApp.Models.CustomExceptions;
using ExpensesManagementApp.Models.HttpResult;

namespace ExpensesManagementApp.Services
{
    public class TransactionService(ITransactionsRepository _transactionRepository, ILogger<TransactionService> _logger) : ITransactionService
    {
        public async Task<HttpResult<bool>> DeleteTransactionAsync(Guid id)
        {
            try
            {
                var succes = await _transactionRepository.DeleteTransactionsAsync(id);

                return new HttpResult<bool>(succes);
            }
            catch (ExpensesManagementAppDbException ex)
            {
                _logger.LogError(ex, "[{0D}] TransactionService threw an ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<bool>(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] TransactionService threw an Exception: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<bool>();
            }
        }

        public async Task<HttpResult<Models.Transaction.TransactionGroup>> AssignTransactionsToGroup(Models.Transaction.TransactionGroup transactionGroup)
        {
            try
            {
                var _transactionGroup = await _transactionRepository.AssignTransactionsToGroup(transactionGroup);

                return new HttpResult<Models.Transaction.TransactionGroup>(_transactionGroup);
            }
            catch (ExpensesManagementAppDbException ex)
            {
                _logger.LogError(ex, "[{0D}] TransactionService threw an ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.Transaction.TransactionGroup>(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] TransactionService threw an Exception: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.Transaction.TransactionGroup>();
            }
        }
    }
}
