namespace Shop.Domain.Exceptions
{
    public class ApiException
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string? Details { get; set; }
        public ApiException(int statusCode, string message, string detail)
        {
            StatusCode = statusCode;
            Message = message;
            Details = detail;
        }
    }
}
