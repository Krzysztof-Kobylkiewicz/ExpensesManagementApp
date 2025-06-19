using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Models.Category
{
    public class Category : ModelCore<Category, Guid>
    {
        [Display(Name = "Name"), Required(ErrorMessage = RequiredMessage)]
        public string? Name { get; set; }

        [Display(Name = "Description"), Required(ErrorMessage = RequiredMessage)]
        public string? Description { get; set; }

        [Display(Name = "Code"), Required(ErrorMessage = RequiredMessage), MaxLength(3, ErrorMessage = MaxLengthMessage)]
        public string? Code { get; set; }
    }
}
