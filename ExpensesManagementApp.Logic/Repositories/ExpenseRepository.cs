using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Data;
using ExpensesManagementApp.Database;

namespace ExpensesManagementApp.Logic.Repositories
{
    public class ExpenseRepository
    {
        private readonly ApplicationDbContext database;
        private readonly ILogger<ExpenseRepository> logger;

        public ExpenseRepository(ApplicationDbContext database, ILogger<ExpenseRepository> logger)
        {
            this.database = database;
            this.logger = logger;
        }

        public async Task AddExpensesAsync(IEnumerable<Models.Expense.Expense> expenses)
        {
            using var transaction = database.Database.BeginTransaction();
            try
            {
                await AddExpensesAsync(expenses, transaction);
            }
            catch (Exception ex)
            {
                logger.LogError("[{0D}] An error occured while adding expenses to database: {1M}", DateTime.Now, ex.Message);
                await transaction.RollbackAsync();
            }
        }

        public async Task AddExpensesAsync(IEnumerable<Models.Expense.Expense> expenses, IDbContextTransaction dbContextTransaction)
        {
            try
            {
                expenses = await ValidateExpensesAsync(expenses);
                await database.AddRangeAsync(expenses.Select(e => Database.DbModels.Expense.ConvertToDbExpense(e)));
            }
            catch (Exception ex)
            {
                logger.LogError("[{0D}] An error occured while adding expenses to database: {1M}", DateTime.Now, ex.Message);
                await dbContextTransaction.GetDbTransaction().RollbackAsync();
            }
        }

        private async Task<IEnumerable<Models.Expense.Expense>> ValidateExpensesAsync(IEnumerable<Models.Expense.Expense> expenses)
        {
            DateOnly? earliestDate = expenses.MinBy(e => e.OperationDate)?.OperationDate;
            DateOnly? latestDate = expenses.MaxBy(e => e.OperationDate)?.OperationDate;

            var areExpensesFromGivenPeriodPresent = database.Expenses.Any(expense => expense.OperationDate >= earliestDate && expense.OperationDate <= latestDate);

            if (!areExpensesFromGivenPeriodPresent)
            {
                return expenses;
            }
            else
            {
                var expensesFromGivenPeriod = await database.Expenses.Where(expense => expense.OperationDate >= earliestDate && expense.OperationDate <= latestDate)
                                                                     .Select(e => new Models.Expense.Expense(e.OperationDate, e.AccountingDate, e.Amount, e.Recipient, e.Sender, e.OperationTitle, e.SenderAccountNumber, e.OperationNumber))
                                                                     .ToListAsync();

                return expenses.Where(e => expensesFromGivenPeriod.All(ep => !ep.CompareExpensesWithTheSameDate(e)));
            }
        }
    }
}
