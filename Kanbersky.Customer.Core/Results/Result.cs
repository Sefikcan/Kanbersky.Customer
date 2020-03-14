namespace Kanbersky.Customer.Core.Results
{
    public class Result : IResult
    {
        public bool Success { get; }

        public string Message { get; }

        public int StatusCode { get; }

        public Result(bool success,int statusCode)
        {
            Success = success;
            StatusCode = statusCode;
        }

        public Result(bool success,string message, int statusCode)
        {
            Success = success;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
