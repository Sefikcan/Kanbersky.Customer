namespace Kanbersky.Customer.Core.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message,int statusCode) : base(true,message,statusCode)
        {
        }

        public SuccessResult(int statusCode)  :base(true,statusCode)
        {
        }
    }
}
