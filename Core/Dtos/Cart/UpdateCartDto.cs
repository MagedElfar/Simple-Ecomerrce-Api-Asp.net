using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Cart
{
    public class UpdateCartDto
    {
        public List<UpdateCartItemDto> CartItems { get; set; } = new List<UpdateCartItemDto>();

    }
}
