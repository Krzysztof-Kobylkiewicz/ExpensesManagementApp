using Core.Entities;
using ExpensesManagementApp.Models.File;

namespace ExpensesManagementApp.Database.DbModels
{
    public class File : Entity<Guid>
    {
        public string? FileName { get; set; }
        public long? FileSize { get; set; }
        public BankTypeEnum BankType { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }

        public static File ConvertToDbFile(Models.File.File file)
        {
            if (!file.BankType.HasValue)
                throw new InvalidOperationException("File must have a bank type assigned");

            return new File
            {
                Id = file.Id,
                FileName = file.FileName,
                FileSize = file.FileSize,
                BankType = file.BankType.Value,
                Transactions = file.Transactions.Select(t => Transaction.ConvertToDbTransaction(t)).ToList()
            };
        }

        public static Models.File.File ConvertToFileDTO(File file)
        {
            return new Models.File.File
            {
                Id = file.Id,
                FileName = file.FileName,
                FileSize = file.FileSize,
                BankType = file.BankType,
                Transactions = file?.Transactions?.Select(t => Transaction.ConvertToTransactionDTO(t)).ToList() ?? []
            };
        }

        public void UpdateFile(Models.File.File file)
        {
            FileName = file.FileName;
            Transactions = file.Transactions.Select(t => Transaction.ConvertToDbTransaction(t)).ToList() ?? [];
        }
    }
}
