using ExpensesManagementApp.Models.Transaction;

namespace ExpensesManagementApp.Models.File
{
    public class FilePackage
    {
        public File? File { get; set; }

        public IEnumerable<TransactionGroup?>? TransactionGroups { get; set; }

        public IEnumerable<Models.Transaction.ITransaction?> ConvertIntoIEnumerableOfGroupRepresentantsAndOtherTransactions()
        {
            return TransactionGroups?.Select(tg => tg?.Representant).Concat(File?.Transactions.Where(t => !t.TransactionGroupId.HasValue) ?? []) ?? [];
        }
    }
}
