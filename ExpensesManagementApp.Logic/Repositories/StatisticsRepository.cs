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
                var nDays = DateTime.DaysInMonth(latestExpenseDate.Year, latestExpenseDate.Month);

                var amount = await database.Transactions.Where(e => e.OperationDate.Month == latestExpenseDate.Month).Select(e => e.Amount).OrderByDescending(a => a).ToArrayAsync();
                var nAll = amount.Length;
                var nI = amount.Where(a => a > 0).ToArray().Length;
                var nE = amount.Where(a => a < 0).ToArray().Length;


                var statistics = new Statistics
                {
                    Sum = amount.Sum(),
                    IncomeSum = amount.Where(a => a > 0).Sum(),
                    ExpensesSum = amount.Where(a => a < 0).Sum(),
                    Average = amount.Sum() / nDays,
                    IncomeAverage = amount.Where(a => a > 0).Sum() / nDays,
                    ExpensesAverage = amount.Where(a => a < 0).Sum() / nDays,
                    Median = nAll % 2 == 0 ? (amount[nAll / 2] + amount[(nAll / 2) + 1]) / 2 : amount[(int)(nAll / 2 + 0.5)],
                    IncomeMedian = nI % 2 == 0 ? (amount.Where(a => a > 0).ToArray()[nI / 2] + amount.Where(a => a > 0).ToArray()[(nI / 2) + 1]) / 2 : amount.Where(a => a > 0).ToArray()[(int)(nI / 2 + 0.5)],
                    ExpensesMedian = nE % 2 == 0 ? (amount.Where(a => a < 0).ToArray()[nE / 2] + amount.Where(a => a < 0).ToArray()[(nE / 2) + 1]) / 2 : amount.Where(a => a < 0).ToArray()[(int)(nE / 2 + 0.5)]
                };

                statistics.Round();

                return statistics;
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
