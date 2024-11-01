namespace Core.Exceptions
{
    public abstract class BaseApiException:Exception
    {
        public BaseApiException(int statusCode ,string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; }
    }
}
