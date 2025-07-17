using Core.Helpers;
using ExpensesManagementApp.Models.Transaction;
using MudBlazor;

namespace ExpensesManagementApp.Models.Statistics
{
    public class TransactionsChartSeries
    {
        public TransactionsChartSeries() { }

        public double[] Income { get; set; } = [];
        public double[] Expenses { get; set;} = [];

        public int Index { get; set; } = -1;
        public int Height { get; set; } = 300;
        public int Width { get; set; } = 500;

        public double[] GetIncomeDividedByParameter(double dividedBy = 1000) => Income.Select(i => i / dividedBy).ToArray() ?? [];
        public double[] GetExpensesDividedByParameter(double dividedBy = 1000) => Expenses.Select(e => e / -dividedBy).ToArray() ?? [];

        public List<ChartSeries> ChartSeries
        {
            get
            {
                return
                [
                    new ChartSeries() { Name = "Income", Data = GetIncomeDividedByParameter() ?? []},
                    new ChartSeries() { Name = "Expenses", Data = GetExpensesDividedByParameter() ?? [] }
                ];
            }
        }

        public string FirstBarLabel() => ChartSeries[0].Name;

        public string[] BarColors() => (FirstBarLabel()) switch
        {
            "Income" => [Colors.Green.Default, Colors.Red.Default],
            "Expenses" => [Colors.Red.Default, Colors.Green.Default],
            _ => throw new InvalidOperationException("Transaction chart series handles only income and expenses labels")
        };

        public static readonly string[] DefaultColors = [Colors.Gray.Lighten1, Colors.Gray.Darken1];

        public static string[] ChooseXAxisLabels(TransactionFilter filter, DateOnly latestTransactionDate) => (filter.Period) switch
        {
            PeriodEnum.Day => [DateHelper.DateToString(latestTransactionDate)],
            PeriodEnum.Week => DateHelper.WeekDependingOfDate(latestTransactionDate),
            PeriodEnum.Month => throw new NotImplementedException(),
            PeriodEnum.Quarter => [.. DateHelper.MonthsFromGivenQuarter(DateHelper.QuarterDependingOnMonth(latestTransactionDate.Month)).Select(d => DateHelper.MonthIntToString(d))],
            PeriodEnum.Year => throw new NotImplementedException(),
            PeriodEnum.Other => throw new NotImplementedException(),
            _ => throw new NotImplementedException()
        };
    }
}

