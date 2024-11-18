using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Authentication
{
    public class CreateComfirmationLinkDto
    {
        [Required]
        public string Email {  get; set; }
    }
}
