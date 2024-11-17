using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.User
{
    public class MangeUserRoleDto
    {
        [Required]
        public int UserId { get; set; }
        public string RoleName { get; set; }
    }
}
