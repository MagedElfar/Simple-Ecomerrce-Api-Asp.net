using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Order
{
    public class OrderItemDto
    {
        public string ProductName { get; set; }
        public string? ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
