namespace Kanbersky.Customer.Core.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message, int statusCode) : base(false, message, statusCode)
        {
        }

        public ErrorResult(int statusCode) : base(false, statusCode)
        {
        }
    }
}
