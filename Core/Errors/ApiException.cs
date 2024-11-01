
namespace Core.Errors
{
    public class ApiException:ApiErrorResponse
    {
        public ApiException(int statuseCode, string? message = null, string? details = null)
            : base(statuseCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }

        public static implicit operator Version(ApiException v)
        {
            throw new NotImplementedException();
        }
    }
}
