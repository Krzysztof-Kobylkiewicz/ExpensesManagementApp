using System.ComponentModel.DataAnnotations;
using ExpensesManagementApp.Models.File;

namespace ExpensesManagementApp.Database.DbModels
{
    public class File
    {
        [Key]
        public Guid FileId { get; set; }
        public string? FileName { get; set; }
        public long? FileSize { get; set; }
        public BankTypeEnum BankType { get; set; }
        public DateTime UpoloadDate { get; set; } = DateTime.Now;
        public ICollection<Expense>? Expenses { get; set; }

        public static File ConvertToDbFile(Models.File.File file)
        {
            if (!file.BankType.HasValue)
                throw new InvalidOperationException("File must have a bank type assigned");

            return new File
            {
                FileId = file.FileId,
                FileName = file.FileName,
                FileSize = file.FileSize,
                BankType = file.BankType.Value,
                Expenses = file.Expenses.Select(e => Expense.ConvertToDbExpense(e)).ToList()
            };
        }

        public static Models.File.File ConvertToFileDTO(File file)
        {
            return new Models.File.File
            {
                FileId = file.FileId,
                FileName = file.FileName,
                FileSize = file.FileSize,
                BankType = file.BankType,
                Expenses = file?.Expenses?.Select(e => Expense.ConvertToExpenseDTO(e)).ToList() ?? []
            };
        }
    }
}
