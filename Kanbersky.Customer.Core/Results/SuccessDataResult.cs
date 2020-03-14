namespace Kanbersky.Customer.Core.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(string message, T data,int statusCode) : base(true, message, data,statusCode)
        {
        }

        public SuccessDataResult(T data,int statusCode) : base(true, data,statusCode)
        {
        }
    }
}
