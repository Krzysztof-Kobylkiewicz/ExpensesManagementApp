using ExpensesManagementApp.Database;
using ExpensesManagementApp.Models.Statistics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace ExpensesManagementApp.Logic.Repositories
{
    public class StatisticsRepository(ApplicationDbContext database, ILogger<StatisticsRepository> logger)
    {
        public async Task<Statistics?> InitializeAsync()
        {
            try
            {
                var latestExpenseDate = await database.Expenses.MaxAsync(e => e.OperationDate);

                var sum = database.Expenses.Where(e => e.OperationDate.Month == latestExpenseDate.Month).Select(e => e.Amount).Sum();

                return new Statistics
                {
                    Sum = Math.Round(sum, 2)
                };
            }
            catch (InvalidOperationException ioe)
            {
                logger.LogError("[{0D}] Error while accessing database: {1M}", DateTime.Now, ioe.Message);

                if (ioe.Message.Equals("Sequence contains no elements."))
                    return new Statistics(@default: true);
                else
                    throw;
            }
            catch (Exception ex)
            {
                logger.LogError("[{0D}] Error while accessing database: {1M}", DateTime.Now, ex.Message);
                throw;
            }
        }
    }
}
