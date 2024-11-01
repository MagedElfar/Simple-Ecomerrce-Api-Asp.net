using Core.Entities;
using Core.Interfaces.Services;
using Core.Options;
using Core.Specifications;
using Microsoft.Extensions.Options;
using Stripe;
using PaymentIntent = Stripe.PaymentIntent;
using Core.Enums;
namespace Infrastructure.Services
{
    public class StripeService : IStripeService
    {
        private readonly IOptions<StripeOption> options;
        private readonly IOrdersService ordersService;
        private readonly ICartService cartService;

        public StripeService(
            IOptions<StripeOption> options,
            IOrdersService ordersService,
            ICartService cartService
            )
        {
            this.options = options;
            this.ordersService = ordersService;
            this.cartService = cartService;
            StripeConfiguration.ApiKey = this.options.Value.Secretkey;
        }


        public async Task<PaymentIntent> CreatePaymentIntent(decimal amount)
        {
            // Initialize options for creating a PaymentIntent
            var options = new PaymentIntentCreateOptions
            {

                Amount = (long)(amount * 100), // Stripe expects amount in cents
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }, // Only card for now
            };

            // Use Stripe's PaymentIntentService to create the PaymentIntent
            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return paymentIntent;
        }

        public async Task<PaymentIntent> UpdatePaymentIntent(string paymentIntentId, decimal amount)
        {
            // Initialize options for updating the PaymentIntent
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long)(amount * 100) // Update the amount in cents
            };

            // Use Stripe's PaymentIntentService to update the PaymentIntent
            var service = new PaymentIntentService();
            var paymentIntent = await service.UpdateAsync(paymentIntentId, options);

            return paymentIntent;
        }

        public async Task<PaymentIntent> GetPaymentIntent(string id)
        {
            var service = new PaymentIntentService();
            return await service.GetAsync(id);
        }

        //public Task<>


        public async Task HandleCanceldPymentIntentEvent(PaymentIntent paymentIntent)
        {
            var order = await GetOrder(paymentIntent.Id);

            if (order == null)
                return;

            order.OrderStatus = OrderStatusEnum.Canceld;

            await ordersService.UpdateAsync(order);

            return;
        }

        public async Task HandleFailedPaymentIntentEvent(PaymentIntent paymentIntent)
        {

            Console.WriteLine(paymentIntent.Id);
            var order = await GetOrder(paymentIntent.Id);

            if (order == null)
                return;

            order.OrderStatus = OrderStatusEnum.PaymentFaild;

            await ordersService.UpdateAsync(order);

            return;
        }

        public async Task HandleSuccessPaymentIntentEvent(PaymentIntent paymentIntent)
        {
            var order = await GetOrder(paymentIntent.Id);

            if (order == null)
                return;

            order.OrderStatus = OrderStatusEnum.PaymentSucceeded;

            await ordersService.UpdateAsync(order);

            await cartService.DeleteCart(order.UserId.ToString());

            return;
        }

        private async Task<Order?> GetOrder(string paymentIntentId)
        {
            var order = await ordersService.FindOneAync(new BaseSpecifications<Order>(x => x.PaymentIntentId == paymentIntentId));

            return order;
        }
    }

}
