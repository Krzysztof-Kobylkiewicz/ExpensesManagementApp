namespace ExpensesManagementApp.Models.HttpResult
{
    public class HttpResult<T>
    {
        public HttpResult() 
        {
            Message = "Internal server error";
            StatusCode = 500;
        }

        public HttpResult(T? data)
        {
            Data = data;
            Message = "Success";
            StatusCode = 200;
        }

        public HttpResult(string? message, int? statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public HttpResult(T? data, string message, int statusCode)
        {
            Data = data;
            Message = message;
            StatusCode = statusCode;
        }

        public T? Data { get; set; }

        public string? Message { get; set; }

        public int? StatusCode { get; set; }
    }
}
