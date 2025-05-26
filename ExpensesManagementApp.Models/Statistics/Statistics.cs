using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Models.Statistics
{
    public class Statistics : ModelCore
    {
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

        [Display(Name = "Range")]
        public AggregationInterval Range { get; set; } = AggregationInterval.Monthly;

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
    };

    public enum AggregationInterval
    {
        Daily,
        Weekly,
        Monthly,
        Quarterly,
        Yearly,
        Other
    }
}
