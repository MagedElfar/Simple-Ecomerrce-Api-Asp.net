using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Cart
{
    public class UpdateCartDto
    {
 
        public List<UpdateCartItemDto> CartItems { get; set; } = new List<UpdateCartItemDto>();

    }
}
