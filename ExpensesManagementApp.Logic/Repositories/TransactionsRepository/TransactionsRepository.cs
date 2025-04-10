using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Data;
using ExpensesManagementApp.Database;
using ExpensesManagementApp.Models.CustomExceptions;
using ExpensesManagementApp.Database.DbModels;

namespace ExpensesManagementApp.Logic.Repositories.TransactionsRepository
{
    public class TransactionsRepository(ApplicationDbContext database, ILogger<TransactionsRepository> logger) : ITransactionsRepository
    {
        public async Task AddTransactionsAsync(IEnumerable<Models.Transaction.Transaction> transactions)
        {
            using var _dbTransaction = database.Database.BeginTransaction();
            try
            {
                await AddTransactionsAsync(transactions, _dbTransaction);
            }
            catch (Exception ex)
            {
                logger.LogError("[{0D}] An error occured while adding transactions to database: {1M}", DateTime.Now, ex.Message);
                await _dbTransaction.RollbackAsync();
            }
        }

        public async Task AddTransactionsAsync(IEnumerable<Models.Transaction.Transaction> transactions, IDbContextTransaction dbContextTransaction)
        {
            try
            {
                transactions = await ValidateTransactionsAsync(transactions);
                await database.AddRangeAsync(transactions.Select(e => Transaction.ConvertToDbTransaction(e)));
            }
            catch (Exception ex)
            {
                logger.LogError("[{0D}] An error occured while adding transactions to database: {1M}", DateTime.Now, ex.Message);
                await dbContextTransaction.GetDbTransaction().RollbackAsync();
            }
        }

        private async Task<IEnumerable<Models.Transaction.Transaction>> ValidateTransactionsAsync(IEnumerable<Models.Transaction.Transaction> transactions)
        {
            DateOnly? earliestDate = transactions.MinBy(e => e.OperationDate)?.OperationDate;
            DateOnly? latestDate = transactions.MaxBy(e => e.OperationDate)?.OperationDate;

            var areTransactionsFromGivenPeriodPresent = database.Transactions.Any(expense => expense.OperationDate >= earliestDate && expense.OperationDate <= latestDate);

            if (!areTransactionsFromGivenPeriodPresent)
            {
                return transactions;
            }
            else
            {
                var transactionsFromGivenPeriod = await database.Transactions.Where(expense => expense.OperationDate >= earliestDate && expense.OperationDate <= latestDate)
                                                                     .Select(e => Transaction.ConvertToTransactionDTO(e))
                                                                     .ToListAsync();

                return transactions.Where(e => transactionsFromGivenPeriod.All(ep => !ep.CompareTransactionsWithTheSameDate(e)));
            }
        }

        public async Task<bool> DeleteTransactionsAsync(Guid id)
        {
            using var _dbTransaction = database.Database.BeginTransaction();
            try
            {
                var transactionToDelete = await database.Transactions.FirstOrDefaultAsync(e => e.TransactionId == id) ?? throw new ExpensesManagementAppDbException("No such transaction was found.", 404);

                database.Transactions.Remove(transactionToDelete);
                int n = await database.SaveChangesAsync();

                if (n > 1)
                    throw new Exception("An attempt to delete more than one transaction by id was registered.");

                await _dbTransaction.CommitAsync();

                return n == 1;
            }
            catch (Exception ex)
            {
                logger.LogError("[{0D}] Error while attempt to remove transaction: {1M}", DateTime.Now, ex.Message);
                await _dbTransaction.RollbackAsync();
                return false;
            }
        }

        public async Task<Models.Transaction.TransactionGroup> AssignTransactionsToGroup(Models.Transaction.TransactionGroup transactionGroup)
        {
            using var _dbTransaction = database.Database.BeginTransaction();
            try
            {
                var transactionsToUpdate = await database.Transactions.Where(db_t => transactionGroup.Transactions.Select(t => t.TransactionId).Contains(db_t.TransactionId)).ToListAsync();

                transactionGroup.TransactionGroupSum = Math.Round(transactionGroup.Transactions.Select(t => t.Amount).Sum(), 2);
                transactionGroup.TransactionGroupExpensesSum = Math.Round(transactionGroup.Transactions.Where(t => t.Amount < 0).Select(t => t.Amount).Sum(), 2);
                transactionGroup.TransactionGroupIncomeSum = Math.Round(transactionGroup.Transactions.Where(t => t.Amount > 0).Select(t => t.Amount).Sum(), 2);
                transactionGroup.NumberOfTransactionsInGroup = transactionGroup.Transactions.Count();

                var latestTransactionInGroup = transactionGroup.Transactions.MaxBy(t => t.OperationDate);


                transactionGroup.Representant = new Models.Transaction.TransactionRepresentant
                {
                    TransactionId = Guid.NewGuid(),
                    AccountingDate = latestTransactionInGroup?.AccountingDate ??
                        throw new ArgumentNullException(nameof(transactionGroup), "AccountingDate cannot be null."),
                    OperationDate = latestTransactionInGroup?.OperationDate ?? 
                        throw new ArgumentNullException(nameof(transactionGroup), "OperationDate cannot be null."),
                    Amount = transactionGroup.TransactionGroupSum,
                    OperationTitle = transactionGroup.TransactionGroupName,
                    TransactionGroupId = transactionGroup.TransactionGroupId,
                };

                var dbTransactionGroup = TransactionGroup.ConvertToDbTransactionGroup(transactionGroup);

                dbTransactionGroup.Transactions = transactionsToUpdate;

                await database.TransactionGroups.AddAsync(dbTransactionGroup);
                await database.SaveChangesAsync();
                await _dbTransaction.CommitAsync();

                return TransactionGroup.ConvertToTransactionGroupDTO(dbTransactionGroup);
            }
            catch (Exception ex)
            {
                logger.LogError("[{0D}] Error while attempt to assign transactions to group: {1M}", DateTime.Now, ex.Message);
                await _dbTransaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Models.Transaction.TransactionGroup>> GetTransactionGroupsAsync(IEnumerable<Guid?> ids)
        {
            using var _dbTransaction = database.Database.BeginTransaction();
            try
            {
                return await GetTransactionGroupsAsync(ids, _dbTransaction);
            }
            catch (Exception ex)
            {
                logger.LogError("[{0D}] Error while attempt to get transaction groups: {1M}", DateTime.Now, ex.Message);
                await _dbTransaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Models.Transaction.TransactionGroup>> GetTransactionGroupsAsync(IEnumerable<Guid?> ids, IDbContextTransaction dbContextTransaction)
        {
            return await database.TransactionGroups.Where(tg => ids.Contains(tg.TransactionGroupId)).Select(tg => TransactionGroup.ConvertToTransactionGroupDTO(tg)).ToArrayAsync();
        }
    }
}
