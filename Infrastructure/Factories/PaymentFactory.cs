using Core.Interfaces.Services;
using Core.Interfaces.Strategies;
using Infrastructure.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Factories
{
    public class PaymentFactory
    {
        private readonly IStripeService stripeService;
        private readonly IOrdersService ordersService;
        private readonly ICartService cartService;

        public PaymentFactory(IStripeService stripeService , IOrdersService ordersService , ICartService cartService)
        {
            this.stripeService = stripeService;
            this.ordersService = ordersService;
            this.cartService = cartService;
        }


        public IPaymentMethodStrategy CreatePaymentMethod(int paymentMethodId) {
            return paymentMethodId switch
            {
                1 => new CashOnDeliveryPaymentStrategy(ordersService , cartService),
                2 => new StripePaymentStrategy(stripeService , ordersService),
                _ => throw new NotSupportedException("Payment type not supported")
            };
        }
    }
}
