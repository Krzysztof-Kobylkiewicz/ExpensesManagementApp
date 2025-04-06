using ExpensesManagementApp.Database;
using ExpensesManagementApp.Models.Statistics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace ExpensesManagementApp.Logic.Repositories
{
    public class StatisticsRepository(ApplicationDbContext database, ILogger<StatisticsRepository> logger)
    {
        public async Task<Statistics?> InitializeStatisticsAsync()
        {
            try
            {
                var latestExpenseDate = await database.Transactions.MaxAsync(e => e.OperationDate);

                var amount = await database.Transactions.Where(e => e.OperationDate.Month == latestExpenseDate.Month).Select(e => e.Amount).ToArrayAsync();

                var sum = amount.Sum();
                var avg = sum / 30;
                var median = amount.OrderByDescending(a => a).ToArray()[amount.Length / 2];

                return new Statistics
                {
                    Sum = Math.Round(sum, 2),
                    Average = Math.Round(avg, 2),
                    Median = Math.Round(median, 2)
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
