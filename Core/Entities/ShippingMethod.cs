using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ShippingMethod:BaseEntity
    {
        public string Name { get; set; }

        public string DeliveryTime { get; set; }

        public string Description {  get; set; }

        public decimal Price { get; set; }
    }
}
