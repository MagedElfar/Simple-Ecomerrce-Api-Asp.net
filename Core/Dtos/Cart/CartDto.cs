using Core.Dtos.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Cart
{
    public class CartDto
    {

        public CartDto()
        {
            
        }
        public CartDto(string cartId)
        {
            CartId = cartId;
        }

        public string CartId { get; set; }

        public List<CartItemDto> cartItems { get; set; } = new List<CartItemDto>();
    }
}
