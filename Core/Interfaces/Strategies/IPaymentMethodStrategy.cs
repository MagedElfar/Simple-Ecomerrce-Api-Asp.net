using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Strategies
{
    public interface IPaymentMethodStrategy
    {

        Task<PaymentResult> Pay(Order order);
    }
}
