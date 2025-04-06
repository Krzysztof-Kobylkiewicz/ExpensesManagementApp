using ExpensesManagementApp.Client.Services.TransactionService;

namespace ExpensesManagementApp.MapGroup
{
    internal static class TransactionEndpointRoutBuilderExtensions
    {
        internal static IEndpointConventionBuilder MapExpenses(this IEndpointRouteBuilder endpoints)
        {
            var expensesGroup = endpoints.MapGroup("api/v1/transactions");

            // endpoints

            expensesGroup.MapDelete("/delete/{id:guid}", (ITransactionService transactionService, Guid id) => transactionService.DeleteTransactionAsync(id));

            expensesGroup.MapPost("/assignToGroup", (ITransactionService transactionService, Models.Transaction.TransactionGroup transactionGroup) => transactionService.AssignTransactionsToGroup(transactionGroup));

            return expensesGroup;
        }
    }
}
