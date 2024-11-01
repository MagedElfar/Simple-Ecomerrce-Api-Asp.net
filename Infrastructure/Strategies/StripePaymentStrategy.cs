using Core.Entities;
using Core.Interfaces.Services;
using Core.Interfaces.Strategies;
using Core.Options;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.FinancialConnections;
using Stripe.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentIntent = Stripe.PaymentIntent;

namespace Infrastructure.Strategies
{
    public class StripePaymentStrategy : IPaymentMethodStrategy
    {
        private readonly IStripeService stripeService;
        private readonly IOrdersService ordersService;

        public StripePaymentStrategy(IStripeService stripeService , IOrdersService ordersService)
        {
            this.stripeService = stripeService;
            this.ordersService = ordersService;
        }

        public async Task<PaymentResult> Pay(Order order)
        {
            PaymentIntent paymentIntent;

            if (order.PaymentIntentId == null)
                paymentIntent = await stripeService.CreatePaymentIntent(order.SubTotal);
            else
                paymentIntent = await stripeService.UpdatePaymentIntent(order.PaymentIntentId, order.SubTotal);

            order.PaymentIntentId = paymentIntent.Id;

            await ordersService.UpdateAsync(order);

            return new PaymentResult
            {
                PaymentIntentId = paymentIntent.Id,
                ClientSecret = paymentIntent.ClientSecret,
                Status = paymentIntent.Status
            };

        }
    }
}
