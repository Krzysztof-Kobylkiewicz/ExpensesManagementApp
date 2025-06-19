using ExpensesManagementApp.Database;
using ExpensesManagementApp.Database.Filters;
using ExpensesManagementApp.Logic.Repositories.TransactionsRepository;
using ExpensesManagementApp.Models.Statistics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExpensesManagementApp.Logic.Repositories
{
    public class StatisticsRepository(ApplicationDbContext database, ITransactionsRepository transactionsRepository, ILogger<StatisticsRepository> logger)
    {
        public async Task<Statistics?> InitializeStatisticsAsync()
        {
            try
            {
                var latestExpenseDate = await database.Transactions.MaxAsync(e => e.OperationDate);
                
                var nDays = DateTime.DaysInMonth(latestExpenseDate.Year, latestExpenseDate.Month);

                var amount = await database.Transactions.Where(e => e.OperationDate.Month == latestExpenseDate.Month).Select(e => e.Amount).OrderByDescending(a => a).ToArrayAsync();

                return ConstructStatistics(amount, nDays);
            }
            catch (InvalidOperationException ioe)
            {
                logger.LogError("[{0D}] Error while accessing database: {1M}", DateTime.Now, ioe.Message);

                if (ioe.Message.Equals("Sequence contains no elements."))
                    return new Statistics();
                else
                    throw;
            }
            catch (Exception ex)
            {
                logger.LogError("[{0D}] Error while accessing database: {1M}", DateTime.Now, ex.Message);
                throw;
            }
        }

        public async Task<Models.Statistics.TransactionsChartSeries> GetChartSeriesAsync(DbTransactionFilter filter)
        {
            var transactions = await transactionsRepository.GetSpecificTransactionsAsync(filter);

            var groupedTransactions = transactions.GroupBy(t => t.OperationDate.Year).Select(yeargroup => new
            {
                Year = yeargroup.Key,
                Months = yeargroup.GroupBy(t => t.OperationDate.Month).Select(monthgroup => new
                {
                    Month = monthgroup.Key,
                    Transactions = monthgroup.ToArray(),
                    Income = monthgroup.Where(t => t.Amount > 0).Select(t => t.Amount).ToArray(),
                    Expenses = monthgroup.Where(t => t.Amount < 0).Select(t => t.Amount).ToArray()
                }).OrderBy(t => t.Month).ToArray()
            }).OrderBy(t => t.Year).ToArray();

            TransactionsChartSeries series = new();

            foreach (var yeargroup in groupedTransactions)
            {
                foreach (var monthgroup in yeargroup.Months)
                {
                    series.Income = series.Income.Concat([Math.Round(monthgroup.Income.Sum(), 2)]).ToArray();
                    series.Expenses = series.Expenses.Concat([Math.Round(monthgroup.Expenses.Sum(), 2)]).ToArray();
                }
            }

            return series;
        }

        private double CalculateMedian(double[] data)
        {
            if (data is null || data.Length == 0)
                return 0;

            int n = data.Length;

            return n % 2 == 0 ? (data[n / 2 - 1] + data[(n / 2)]) / 2 : data[(int)(n / 2 + 0.5)];
        }

        private double CalculateDominant(IEnumerable<double> data)
        {
            var dominantDict = new Dictionary<double, int>();

            foreach (var value in data)
            {
                if (dominantDict.ContainsKey(value))
                    dominantDict[value]++;
                else
                    dominantDict[value] = 1;
            }

            int maxOccurences = dominantDict.Values.Max();
            double dominant = dominantDict.First(d => d.Value == maxOccurences).Key;

            return dominant;
        }

        private Statistics ConstructStatistics(double[]? transactionsAmount, int daysInPeriod)
        {
            if (transactionsAmount == null || transactionsAmount.Length == 0)
                return new Statistics();

            var amountIncone = transactionsAmount.Where(a => a > 0).ToArray();
            var amountExpenses = transactionsAmount.Where(a => a < 0).ToArray();

            var statistics = new Statistics
            {
                Sum = transactionsAmount.Sum(),
                IncomeSum = amountIncone.Sum(),
                ExpensesSum = amountExpenses.Sum(),
                Average = transactionsAmount.Sum() / daysInPeriod,
                IncomeAverage = amountIncone.Sum() / daysInPeriod,
                ExpensesAverage = amountExpenses.Sum() / daysInPeriod,
                Median = CalculateMedian(transactionsAmount),
                IncomeMedian = CalculateMedian(amountIncone),
                ExpensesMedian = CalculateMedian(amountExpenses),
                Dominant = CalculateDominant(transactionsAmount),
                IncomeDominant = CalculateDominant(amountIncone),
                ExpensesDominant = CalculateDominant(amountExpenses)
            };

            statistics.Round();

            return statistics;
        }
    }
}
