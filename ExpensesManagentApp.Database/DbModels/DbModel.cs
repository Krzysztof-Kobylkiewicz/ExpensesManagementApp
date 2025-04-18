using System.ComponentModel.DataAnnotations;

namespace ExpensesManagementApp.Database.DbModels
{
    public abstract class DbModel : IDbModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? UpoloadDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
