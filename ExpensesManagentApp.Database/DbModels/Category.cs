namespace ExpensesManagementApp.Database.DbModels
{
    public class Category : DbModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }

        public ICollection<Transaction>? Transactions { get; set; }

        public static Category ConvertToDbCategory(Models.Category.Category category)
        {
            return new Category
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Code = category.Code
            };
        }

        public static Models.Category.Category ConvertToCategoryDTO(Category category)
        {
            return new Models.Category.Category
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Code = category.Code
            };
        }
    }
}
