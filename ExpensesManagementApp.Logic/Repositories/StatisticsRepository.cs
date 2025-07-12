using Core.Helpers;
using ExpensesManagementApp.Database.DbModels;
using ExpensesManagementApp.Database.Filters;
using ExpensesManagementApp.Models.Statistics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExpensesManagementApp.Logic.Repositories
{
    public class StatisticsRepository(TransactionsRepository.ITransactionsRepository transactionsRepository, ILogger<StatisticsRepository> logger)
    {
        public async Task<Statistics?> InitializeStatisticsAsync(DbTransactionFilter filter)
        {
            try
            {
                var latestExpenseDate = await transactionsRepository.LatestTransactionDateAsync();

                var nDays = DaysInPeriod(latestExpenseDate, filter.Period);

                var amount = await AmountBasedOnPeriod(latestExpenseDate, filter.Period);

                return ConstructStatistics(amount, nDays);
            }
            catch (InvalidOperationException ioe)
            {
                logger.LogError("[{0D}] Error while accessing database: {1M}", DateTime.Now, ioe.Message);

                if (ioe.Message.Equals("Sequence contains no elements"))
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
            //var transactions = await transactionsRepository.GetSpecificTransactionsAsync(filter);

            var latestExpenseDate = await transactionsRepository.LatestTransactionDateAsync();

            var transactions = await TransactionsBasedOnPeriod(latestExpenseDate, filter.Period);

            var groupedTransactions = transactions?.GroupBy(t => t.OperationDate.Year).Select(yeargroup => new
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

            foreach (var yeargroup in groupedTransactions ?? [])
            {
                foreach (var monthgroup in yeargroup.Months)
                {
                    series.Income = series.Income.Concat([Math.Round(monthgroup.Income.Sum(), 2)]).ToArray();
                    series.Expenses = series.Expenses.Concat([Math.Round(monthgroup.Expenses.Sum(), 2)]).ToArray();
                }
            }

            return series;
        }

        private Statistics ConstructStatistics(double[]? transactionsAmount, int daysInPeriod)
        {
            if (transactionsAmount == null || transactionsAmount.Length == 0)
                return new Statistics();

            var amountIncone = transactionsAmount.Where(a => a > 0).ToArray();
            var amountExpenses = transactionsAmount.Where(a => a < 0).ToArray();

            var statistics = new Statistics
            {
                Sum = transactionsAmount?.Sum(),
                IncomeSum = amountIncone?.Sum(),
                ExpensesSum = amountExpenses?.Sum(),
                Average = transactionsAmount?.Sum() / daysInPeriod,
                IncomeAverage = amountIncone?.Sum() / daysInPeriod,
                ExpensesAverage = amountExpenses?.Sum() / daysInPeriod,
                Median = MathHelper.CalculateMedian(transactionsAmount ?? []),
                IncomeMedian = MathHelper.CalculateMedian(amountIncone ?? []),
                ExpensesMedian = MathHelper.CalculateMedian(amountExpenses ?? []),
                Dominant = MathHelper.CalculateDominant(transactionsAmount ?? []),
                IncomeDominant = MathHelper.CalculateDominant(amountIncone ?? []),
                ExpensesDominant = MathHelper.CalculateDominant(amountExpenses ?? [])
            };

            statistics.Round();

            return statistics;
        }

        private async Task<Transaction[]?> TransactionsBasedOnPeriod(DateOnly latestExpenseDate, PeriodEnum? period, DbTransactionFilter? filter = null)
        {
            var transactionsQuery = transactionsRepository.GetTransactionsQuery();

            switch (period)
            {
                case PeriodEnum.Day:
                    transactionsQuery = transactionsQuery.Where(t => t.OperationDate == latestExpenseDate);
                    break;
                case PeriodEnum.Week:
                    transactionsQuery = transactionsQuery.Where(t => t.OperationDate <= latestExpenseDate && t.OperationDate >= latestExpenseDate.AddDays(-7));
                    break;
                case PeriodEnum.Month:
                    transactionsQuery = transactionsQuery.Where(t => t.OperationDate <= latestExpenseDate && t.OperationDate >= latestExpenseDate.AddMonths(-1));
                    break;
                case PeriodEnum.Quarter:
                    int[] months = DateHelper.MonthsFromQuarterOfProvidedMonth(latestExpenseDate.Month);
                    transactionsQuery = transactionsQuery.Where(t => t.OperationDate.Month >= months[0] && t.OperationDate.Month <= months[2]);
                    break;
                case PeriodEnum.Year:
                    transactionsQuery = transactionsQuery.Where(t => t.OperationDate <= latestExpenseDate && t.OperationDate >= latestExpenseDate.AddYears(-1));
                    break;
                case PeriodEnum.Other:
                    if (filter?.DateTimeFrom.HasValue == true)
                    {
                        DateOnly dateFrom = new DateOnly(filter.DateTimeFrom.Value.Year, filter.DateTimeFrom.Value.Month, filter.DateTimeFrom.Value.Day);
                        transactionsQuery = transactionsQuery.Where(t => t.OperationDate >= dateFrom);
                    }
                    if (filter?.DateTimeTo.HasValue == true)
                    {
                        DateOnly dateTo = new DateOnly(filter.DateTimeTo.Value.Year, filter.DateTimeTo.Value.Month, filter.DateTimeTo.Value.Day);
                        transactionsQuery = transactionsQuery.Where(t => t.OperationDate >= dateTo);
                    }
                    break;
            };

            try
            {
                return await transactionsQuery.ToArrayAsync();
            }
            catch (InvalidOperationException ioe) when (ioe.Message.Equals("Sequence contains no elements"))
            {
                return [];
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<double[]?> AmountBasedOnPeriod(DateOnly latestExpenseDate, PeriodEnum? aggregationInterval) => (await TransactionsBasedOnPeriod(latestExpenseDate, aggregationInterval))?.Select(t => t.Amount).ToArray();

        private int DaysInPeriod(DateOnly latestExpenseDate, PeriodEnum? period) => (period) switch
        {
            PeriodEnum.Day => 1,
            PeriodEnum.Week => 7,
            PeriodEnum.Month => DateTime.DaysInMonth(latestExpenseDate.Year, latestExpenseDate.Month),
            PeriodEnum.Quarter => DateHelper.DaysInQuarter(latestExpenseDate),
            PeriodEnum.Year => DateTime.IsLeapYear(latestExpenseDate.Year) ? 366 : 355,
            _ => throw new NotImplementedException()
        };

        public async Task<DateOnly> LatestTransactionDateAsync() => await transactionsRepository.LatestTransactionDateAsync();
        public async Task<DateOnly> EarliestTransactionDateAsync() => await transactionsRepository.EarliestTransactionDateAsync();
    }
}
