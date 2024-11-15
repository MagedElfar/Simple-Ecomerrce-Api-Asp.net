using Core.DTOS.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Order
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
        public IEnumerable<OrderItemDto> Items { get; set; } = Enumerable.Empty<OrderItemDto>();
    }
}
