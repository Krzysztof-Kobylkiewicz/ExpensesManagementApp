using ExpensesManagementApp.Client.Services.TransactionService;
using ExpensesManagementApp.Logic.Repositories.TransactionsRepository;
using ExpensesManagementApp.Models.CustomExceptions;
using Core.Models;
using ExpensesManagementApp.Models.Transaction;

namespace ExpensesManagementApp.Services
{
    public class TransactionService(ITransactionsRepository _transactionRepository, ILogger<TransactionService> _logger) : ITransactionService
    {
        public async Task<HttpResult<Transaction>> GetTransactionAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResult<IEnumerable<Transaction>>> GetAllTransactionsAsync()
        {
            try
            {
                var transactions = await _transactionRepository.GetAllTransactionsAsync();

                return new HttpResult<IEnumerable<Transaction>>(transactions);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<IEnumerable<Transaction>>.HandleExceptionAndLogError(ex, _logger);
            }
        }

        public async Task<HttpResult<IEnumerable<Transaction>>> GetSpecificTransactionsAsync(TransactionFilter filter)
        {
            try
            {
                var transactions = await _transactionRepository.GetSpecificTransactionsAsync(new Database.Filters.DbTransactionFilter(filter));

                return new HttpResult<IEnumerable<Transaction>>(transactions);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<IEnumerable<Transaction>>.HandleExceptionAndLogError(ex, _logger);
            }
        }

        public async Task<HttpResult<bool>> DeleteTransactionAsync(Guid id)
        {
            try
            {
                var succes = await _transactionRepository.DeleteTransactionsAsync(id);

                return new HttpResult<bool>(succes);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<bool>.HandleExceptionAndLogError(ex, _logger);
            }
        }

        public async Task<HttpResult<Models.Transaction.TransactionGroup>> AssignTransactionsToGroup(Models.Transaction.TransactionGroup transactionGroup)
        {
            try
            {
                var _transactionGroup = await _transactionRepository.AssignTransactionsToGroup(transactionGroup);

                return new HttpResult<Models.Transaction.TransactionGroup>(_transactionGroup);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<TransactionGroup>.HandleExceptionAndLogError(ex, _logger);
            }
        }
    }
}
