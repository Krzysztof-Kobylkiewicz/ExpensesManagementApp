using ExpensesManagementApp.Models.Transaction;

namespace ExpensesManagementApp.Models.File
{
    public class FilePackage
    {
        public File? File { get; set; }

        public IEnumerable<TransactionGroup?>? TransactionGroups { get; set; }
    }
}
