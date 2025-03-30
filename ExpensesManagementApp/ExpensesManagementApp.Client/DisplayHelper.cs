using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ExpensesManagementApp.Client
{
    public static class DisplayHelper
    {
        public static string DisplayName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression.Body is MemberExpression member)
            {
                var displayAttribute = member.Member
                    .GetCustomAttributes(typeof(DisplayAttribute), false)
                    .FirstOrDefault() as DisplayAttribute;

                return displayAttribute?.Name ?? member.Member.Name;
            }
            return string.Empty;
        }
    }
}
