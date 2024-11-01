﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class OrderItem
    {

        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId {  get; set; }
        public Product Product { get; set; }
        public string ProductName { get; set; }
        public string? ProductImage {  get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }


    }
}