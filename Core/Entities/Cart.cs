using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Cart:BaseEntity
    {
        public IEnumerable<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
