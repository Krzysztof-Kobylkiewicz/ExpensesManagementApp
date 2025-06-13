using Core.Entities;
using ExpensesManagementApp.Models.File;

namespace ExpensesManagementApp.Database.DbModels
{
    public class File : Entity<File, Models.File.File, Guid>, IConvertable<File, Models.File.File, Guid>
    {
        public string? FileName { get; set; }
        public long? FileSize { get; set; }
        public BankTypeEnum? BankType { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }

        public Models.File.File ConvertEntityToDTO() => new()
        {
            Id = this.Id,
            FileName = this.FileName,
            FileSize = this.FileSize,
            BankType = this.BankType,
            Transactions = this.Transactions?.Select(t => t.ConvertEntityToDTO()) ?? []
        };

        public static File ConvertDTOToEntity(Models.File.File dtoFile) => new()
        {
            Id = dtoFile.Id,
            FileName = dtoFile.FileName,
            FileSize = dtoFile.FileSize,
            BankType = dtoFile.BankType,
            Transactions = dtoFile.Transactions.Select(t => Transaction.ConvertDTOToEntity(t)).ToList()
        };

        public void UpdateFile(Models.File.File file)
        {
            FileName = file.FileName;
            Transactions = file.Transactions.Select(t => Transaction.ConvertDTOToEntity(t)).ToList() ?? [];
        }
    }
}
