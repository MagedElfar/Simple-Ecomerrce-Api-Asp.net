﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum OrderStatus
    {
        Pending = 1,
        Shipping,
        Canceld,
        PaymentFaild, 
        PaymentSucceeded,
        Confirmed,
        Completed
    }
}
