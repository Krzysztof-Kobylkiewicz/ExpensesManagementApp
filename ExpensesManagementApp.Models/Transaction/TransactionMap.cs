using CsvHelper.Configuration;

namespace ExpensesManagementApp.Models.Transaction
{
    public class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Map(m => m.OperationDate).Index(1);
            Map(m => m.AccountingDate).Index(0);
            Map(m => m.Amount).Index(5);
            Map(m => m.Recipient).Index(3);
            //Map(m => m.Sender).Index();
            Map(m => m.OperationTitle).Index(2);
            Map(m => m.SenderAccountNumber).Index(4);
            Map(m => m.OperationNumber).Index(7);
        }
    }
}
