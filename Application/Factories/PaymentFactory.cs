using Application.Strategies;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Interfaces.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Factories
{
    public class PaymentFactory
    {

        private readonly CashOnDeliveryPaymentStrategy cashOnDelivery;
        private readonly StripePaymentStrategy stripe;

        public PaymentFactory(CashOnDeliveryPaymentStrategy cashOnDelivery, StripePaymentStrategy stripe)
        {
            this.cashOnDelivery = cashOnDelivery;
            this.stripe = stripe;
        }

        public IPaymentMethodStrategy CreatePaymentMethod(int paymentMethodId) {
            return paymentMethodId switch
            {
                1 => cashOnDelivery,
                2 => stripe,
                _ => throw new NotSupportedException("Payment type not supported")
            };
        }
    }
}
