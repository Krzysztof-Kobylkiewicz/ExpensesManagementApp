namespace Core.Helpers
{
    public static class DateHelper
    {
        #region Quarters

        /// <summary>
        /// Returns value that corresponds to quarter of provided month.
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static int QuarterDependingOnMonth(int month)
        {
            if (month < 0)
                throw new InvalidOperationException("Month cannot be negative.");

            if (month <= 3)
                return 1;
            else if (month <= 6)
                return 2;
            else if (month <= 9)
                return 3;
            else if (month <= 12)
                return 4;
            else
                throw new InvalidOperationException("Month cannot be outside range 1-12.");
        }

        /// <summary>
        /// Returns array of months which belong to provided quarter.
        /// </summary>
        /// <param name="quarter"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static int[] MonthsFromGivenQuarter(int quarter) => (quarter) switch
        {
            1 => [1, 2, 3],
            2 => [4, 5, 6],
            3 => [7, 8, 9],
            4 => [10, 11, 12],
            _ => throw new InvalidOperationException("Quarter cannot be outside range 1-4.")
        };

        public static int DaysInQuarter(DateOnly latestExpenseDate)
        {
            int nDays = 0;

            for (int i = 0; i < 3; i++)
            {
                nDays = nDays + DateTime.DaysInMonth(latestExpenseDate.Year, latestExpenseDate.AddMonths(i).Month);
            }

            return nDays;
        }

        #endregion

        #region Months

        /// <summary>
        /// Returns names of months corresponding to provided values.
        /// </summary>
        /// <param name="months"></param>
        /// <returns></returns>
        public static IEnumerable<string> MonthsIntToString(IEnumerable<int> months)
        {
            List<string> monthsToString = [];

            foreach (var month in months)
            {
                monthsToString.Add(MonthIntToString(month));
            }

            return monthsToString;
        }

        /// <summary>
        /// Returns name of month corresponding to provided value.
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static string MonthIntToString(int month) => (month) switch
        {
            1 => "January",
            2 => "February",
            3 => "March",
            4 => "April",
            5 => "May",
            6 => "June",
            7 => "July",
            8 => "August",
            9 => "September",
            10 => "October",
            11 => "November",
            12 => "December",
            _ => throw new InvalidOperationException("Month cannot be outside range 1-12.")
        };

        /// <summary>
        /// Returns name of quarter corresponding to provided month.
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static string MonthIntToQuarterString(int month)
        {
            if (month > 0 && month <= 3)
                return "1st quarter";

            if (month > 3 && month <= 6)
                return "2nd quarter";

            if (month > 6 && month <= 9)
                return "3rd quarter";

            if (month > 9 && month <= 12)
                return "4th quarter";

            throw new InvalidOperationException("Month cannot be outside range 1-12.");
        }

        /// <summary>
        /// Returns 3 months which belong to quarter where provided month belongs. 
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int[] MonthsFromQuarterOfProvidedMonth(int month) => MonthsFromGivenQuarter(QuarterDependingOnMonth(month));

        /// <summary>
        /// Returns names of 3 months which belong to quarter where provided month belongs. 
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string[] MonthsFromQuarterOfProvidedMonthToString(int month) => [.. MonthsFromQuarterOfProvidedMonth(month).Select(m => MonthIntToString(m))];

        #endregion

        #region Days

        /// <summary>
        /// Returns name of provided day.
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static string DayIntToString(int day) => (day) switch
        {
            1 => "Monday",
            2 => "Tuesday",
            3 => "Wednesday",
            4 => "Thursday",
            5 => "Friday",
            6 => "Saturday",
            7 => "Sunday",
            _ => throw new InvalidOperationException("Day cannot be outside range 1-7.")
        };

        /// <summary>
        /// Returns string representation of date.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="displayDayOfWeek"></param>
        /// <returns></returns>
        public static string DateToString(DateOnly date, bool displayDayOfWeek = false) => $"{date:dd.MM.yyyy}{(displayDayOfWeek ? " - " + date.DayOfWeek : string.Empty)}";

        #endregion
    }
}
