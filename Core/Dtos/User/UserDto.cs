﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.User
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string Email { get; set; }

        public IEnumerable<string>? Roles { get; set; }
    }
}
