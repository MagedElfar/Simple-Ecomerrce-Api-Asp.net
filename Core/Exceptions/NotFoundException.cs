namespace Core.Exceptions
{

    public class NotFoundException : BaseApiException
    {
        public NotFoundException(string message = "Not Found") : base(404, message) { }


    }
}
