using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ApplicationUser:IdentityUser<int>
    {
        public BillingAddress? BillingAddress { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
