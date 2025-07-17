using Core.Helpers;
using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Models.Statistics
{
    public class Statistics : ModelCore<Statistics, Guid>
    {
        #region ctors

        public Statistics()
        {
            Sum = 0;
            IncomeSum = 0;
            ExpensesSum = 0;

            Average = 0;
            IncomeAverage = 0;
            ExpensesAverage = 0;

            Median = 0;
            IncomeMedian = 0;
            ExpensesMedian = 0;

            Dominant = 0;
            IncomeDominant = 0;
            ExpensesDominant = 0;
        }

        public Statistics(double[] amount, int daysInPeriod, bool round = true)
        {
            var amountIncone = amount.Where(a => a > 0).ToArray();
            var amountExpenses = amount.Where(a => a < 0).ToArray();

            Sum = amount?.Sum();
            IncomeSum = amountIncone?.Sum();
            ExpensesSum = amountExpenses?.Sum();

            Average = amount?.Sum() / daysInPeriod;
            IncomeAverage = amountIncone?.Sum() / daysInPeriod;
            ExpensesAverage = amountExpenses?.Sum() / daysInPeriod;

            Median = MathHelper.CalculateMedian(amount ?? []);
            IncomeMedian = MathHelper.CalculateMedian(amountIncone ?? []);
            ExpensesMedian = MathHelper.CalculateMedian(amountExpenses ?? []);

            Dominant = MathHelper.CalculateDominant(amount ?? []);
            IncomeDominant = MathHelper.CalculateDominant(amountIncone ?? []);
            ExpensesDominant = MathHelper.CalculateDominant(amountExpenses ?? []);

            if (round)
                Round();

        }

        #endregion

        [Display(Name = "Sum")]
        public double? Sum { get; set; }

        [Display(Name = "Income sum")]
        public double? IncomeSum { get; set; }

        [Display(Name = "Expenses sum")]
        public double? ExpensesSum { get; set; }

        [Display(Name = "Average")]
        public double? Average { get; set; }

        [Display(Name = "Income average")]
        public double? IncomeAverage { get; set; }

        [Display(Name = "Expenses average")]
        public double? ExpensesAverage { get; set; }

        [Display(Name = "Median")]
        public double? Median { get; set; }

        [Display(Name = "Income median")]
        public double? IncomeMedian { get; set; }

        [Display(Name = "Expense median")]
        public double? ExpensesMedian { get; set; }

        [Display(Name = "Dominant")]
        public double? Dominant { get; set; }

        [Display(Name = "Income dominant")]
        public double? IncomeDominant { get; set; }

        [Display(Name = "Expenses dominant")]
        public double? ExpensesDominant { get; set; }

        [Display(Name = "Period")]
        public PeriodEnum Period { get; set; } = PeriodEnum.Quarter;

        [Display(Name = "Date from")]
        public DateTime? DateFrom { get; set; }

        [Display(Name = "Date to")]
        public DateTime? DateTo { get; set; }

        public void Round(int _decimalPlace = 2)
        {
            Sum = Sum.HasValue ? Math.Round(Sum.Value, _decimalPlace) : 0;
            IncomeSum = IncomeSum.HasValue ? Math.Round(IncomeSum.Value, _decimalPlace) : 0;
            ExpensesSum = ExpensesSum.HasValue ? Math.Round(ExpensesSum.Value, _decimalPlace) : 0;

            Average = Average.HasValue ? Math.Round(Average.Value, _decimalPlace) : 0;
            IncomeAverage = IncomeAverage.HasValue ? Math.Round(IncomeAverage.Value, _decimalPlace) : 0;
            ExpensesAverage = ExpensesAverage.HasValue ? Math.Round(ExpensesAverage.Value, _decimalPlace) : 0;

            Median = Median.HasValue ? Math.Round(Median.Value, _decimalPlace) : 0;
            IncomeMedian = IncomeMedian.HasValue ? Math.Round(IncomeMedian.Value, _decimalPlace) : 0;
            ExpensesMedian = ExpensesMedian.HasValue ? Math.Round(ExpensesMedian.Value, _decimalPlace) : 0;

            Dominant = Dominant.HasValue ? Math.Round(Dominant.Value, _decimalPlace) : 0;
            IncomeDominant = IncomeDominant.HasValue ? Math.Round(IncomeDominant.Value, _decimalPlace) : 0;
            Dominant = Dominant.HasValue ? Math.Round(Dominant.Value, _decimalPlace) : 0;
        }
        
        public static string PeriodEnumToString(PeriodEnum? period) => (period) switch
        {
            PeriodEnum.Day => "Day",
            PeriodEnum.Week => "Week",
            PeriodEnum.Month => "Month",
            PeriodEnum.Quarter => "Quarter",
            PeriodEnum.Year => "Year",
            PeriodEnum.Other => "Other",
            _ => throw new NotImplementedException()
        };
    };

    public enum PeriodEnum
    {
        Day,
        Week,
        Month,
        Quarter,
        Year,
        Other
    }
}
