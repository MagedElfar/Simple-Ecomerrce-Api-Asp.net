using Core.Dtos.Address;
using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Order
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public string Status { get; set; }
        public string CustomerEmail { get; set; }

        public string PaymentMethod { get; set; }

        public string PaymentIntentId { get; set; }
        public ShippingAddressDto ShippingAddress { get; set; }
        public IEnumerable<OrderItemDto> Items { get; set; }  = Enumerable.Empty<OrderItemDto>();
    }
}
