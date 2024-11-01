namespace Core.Exceptions
{
    public class ConflictException : BaseApiException
    {
        public ConflictException(string message = "Conflict") : base(409, message) { }


    }
}
