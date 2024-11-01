using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Cart
    {

        public Cart()
        {
            
        }

        public Cart(string id)
        {
            Id = id;
        }

        public string Id { get; set; } 
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public string? ClientSecret { get; set; }

        public string? PaymentIntentId { get; set; }
    }
}
