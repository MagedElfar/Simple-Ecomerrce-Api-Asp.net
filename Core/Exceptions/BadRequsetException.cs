using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class BadRequsetException : BaseApiException
    {
        public BadRequsetException(string message = "Bad Requset" , IEnumerable<string> err =null) : base(400, message)
        {
            Errors = err;
        }

        public IEnumerable<string>? Errors { get; }
    }
}
