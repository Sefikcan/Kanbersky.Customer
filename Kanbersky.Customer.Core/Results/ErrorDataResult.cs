namespace Kanbersky.Customer.Core.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(string message, T data, int statusCode) : base(false, message, data, statusCode)
        {
        }

        public ErrorDataResult(T data, int statusCode) : base(false, data, statusCode)
        {
        }
    }
}
