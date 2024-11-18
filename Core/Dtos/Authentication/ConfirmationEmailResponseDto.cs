using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Authentication
{
    public class ConfirmationEmailResponseDto
    {
        public string Message { get; set; }
        public bool IsEmailConfirmationRequired { get; set; }
    }
}
