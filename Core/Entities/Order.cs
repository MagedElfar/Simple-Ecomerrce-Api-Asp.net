using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Order:BaseEntity
    { 
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public ShippingAddress ShippingAddress { get; set; }
        public int? UserId {  get; set; }
        public ApplicationUser?User { get; set; }
        public string Email {  get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal SubTotal { get; set; }
        public OrderStatusEnum OrderStatus { get; set; } = OrderStatusEnum.Pending;
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public string? PaymentIntentId {  get; set; }

        public int? PaymentMethodId {  get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        //public int ShippingMethodId { get; set; }
        //public ShippingMethod ShippingMethod { get; set; }

    }
}
