﻿using ExpensesManagementApp.Models.Transaction;

namespace ExpensesManagementApp.Models.Statistics
{
    public class CustomChartSettings
    {
        public CustomChartSettings() { }

        public const int BarsNumberYearly = 12;
        public const int BarsNumberMonthly = 31;
        public const int BarsNumberQuarterly = 4;

        public static readonly string[] XAxisLabelsQuarter = { "1st quarter", "2nd quarter", "3rd quarter", "4th quarter" };
        public static readonly string[] XAxisLabelsMonths = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        public int Index { get; set; } = -1;
        public int Height { get; set; } = 300;
        public int Width { get; set; } = 500;

        public string[] ChooseXAxisLabels(Models.Statistics.AggregationInterval range, TransactionFilter filter)
        {
            if (filter.DateTimeFrom.HasValue && filter.DateTimeTo.HasValue)
            {
                switch (range)
                {
                    case AggregationInterval.Monthly: return XAxisLabelsMonths.Skip(filter.DateTimeFrom.Value.Month).Take(filter.DateTimeTo.Value.Month - filter.DateTimeFrom.Value.Month).ToArray();
                    default: throw new NotImplementedException();
                }
            }
            return [];
        }
    }
}
