namespace ExpensesManagementApp.Database.DbModels
{
    public interface IDbModel
    {
        Guid Id { get; set; }
        DateTime? UpoloadDate { get; set; }
        DateTime? UpdateDate { get; set; }
    }
}
