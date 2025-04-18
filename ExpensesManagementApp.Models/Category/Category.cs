using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Models.Category
{
    public class Category
    {
        public Guid CategoryId { get; set; }

        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Code")]
        public string? Code { get; set; }
    }
}
