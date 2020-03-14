namespace Kanbersky.Customer.Core.Results
{
    public class DataResult<T> : Result,IDataResult<T>
    {
        public T Data { get; }

        public DataResult(bool success,T data,int statusCode) : base(success,statusCode)
        {
            Data = data;
        }

        public DataResult(bool success,string message,T data,int statusCode) : base(success,message,statusCode)
        {
            Data = data;
        }
    }
}
