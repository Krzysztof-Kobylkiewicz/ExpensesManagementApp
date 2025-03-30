using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Models.Statistics
{
    public class Statistics
    {
        public Statistics() { }
        public Statistics(bool @default)
        {
            if (@default)
            {
                Sum = 0;
                Average = 0;
                Median = 0;
                Dominant = 0;
            }
            else
            {
                new Statistics();
            }
        }

        public Guid Id { get; set; }
        [Display(Name = "Sum")]
        public double? Sum { get; set; }
        [Display(Name = "Average")]
        public double? Average { get; set; }
        [Display(Name = "Median")]
        public double? Median { get; set; }
        [Display(Name = "Dominant")]
        public double? Dominant { get; set; }
        [Display(Name = "Range")]
        public Range Range { get; set; } = Range.Monthly;
        [Display(Name = "Date from")]
        public DateTime? DateFrom { get; set; }
        [Display(Name = "Date to")]
        public DateTime? DateTo { get; set; }
    }

    public enum Range
    {
        Daily,
        Weekly,
        Monthly,
        Quarterly,
        Yearly,
        Other
    }
}
