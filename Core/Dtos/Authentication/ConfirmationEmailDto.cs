using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Authentication
{
    public class ConfirmationEmailDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
