using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Cart
{
    public class CartDto
    {
        public int CartId { get; set; }

        public List<CartItemDto> cartItems { get; set; } = new List<CartItemDto>();
    }
}
