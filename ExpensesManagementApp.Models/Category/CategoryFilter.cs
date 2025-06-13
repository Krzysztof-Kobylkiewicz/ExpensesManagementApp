using Core.Filters;

namespace ExpensesManagementApp.Models.Category
{
    public class CategoryFilter : IFilterBase
    {
        public DateTime? DateTimeFrom { get; set; }
        public DateTime? DateTimeTo { get; set; }
        public string? Login { get; set; }
        public string? IpAddress { get; set; }
        public string? SearchString { get; set; }
        public void ClearFilterBase()
        {
            DateTimeFrom = null;
            DateTimeTo = null;
            Login = null;
            SearchString = null;
        }
    }
}
