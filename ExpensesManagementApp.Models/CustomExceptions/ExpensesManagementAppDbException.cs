namespace ExpensesManagementApp.Models.CustomExceptions
{
    public class ExpensesManagementAppDbException : Exception
    {
        public ExpensesManagementAppDbException() : base() { }

        public ExpensesManagementAppDbException(string message) : base(message) { }

        public ExpensesManagementAppDbException(string message, int statusCode) : base(message) 
        { 
            StatusCode = statusCode;
        }

        public ExpensesManagementAppDbException(string message, Exception inner) : base(message, inner) { }

        public ExpensesManagementAppDbException(string message, Exception inner, int statusCode) : base(message, inner) 
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; } = 500;
    }
}
