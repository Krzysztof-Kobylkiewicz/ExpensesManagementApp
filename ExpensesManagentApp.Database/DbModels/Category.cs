using Core.Entities;

namespace ExpensesManagementApp.Database.DbModels
{
    public class Category : Entity<Category, Models.Category.Category, Guid>, IConvertable<Category, Models.Category.Category, Guid>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }

        public ICollection<Transaction>? Transactions { get; set; }


        public Models.Category.Category ConvertEntityToDTO() => new()
        {
            Id = this.Id,
            Name = this.Name,
            Description = this.Description,
            Code = this.Code
        };

        public static Category ConvertDTOToEntity(Models.Category.Category category) => new()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Code = category.Code
        };
    }
}
