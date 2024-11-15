using Core.Entities;
using Core.Interfaces.Services;
using Core.Options;
using Core.Specifications;
using Microsoft.Extensions.Options;
using Stripe;
using PaymentIntent = Stripe.PaymentIntent;
using Core.Enums;
using Core.Interfaces.Repositories;
using Core.Specifications.SpecificationBuilder;
namespace Application.Services
{
    public class StripeService : IStripeService
    {
        private readonly IOptions<StripeOption> options;
        private readonly ICartService cartService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IStockService stockService;

        public StripeService(
            IOptions<StripeOption> options,
            ICartService cartService,
            IUnitOfWork unitOfWork,
            IStockService stockService)
        {
            this.options = options;
            this.cartService = cartService;
            StripeConfiguration.ApiKey = this.options.Value.Secretkey;
            this.unitOfWork = unitOfWork;
            this.stockService = stockService;
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

            order.OrderStatus = OrderStatus.Canceld;

            unitOfWork.Repository<Order>().Update(order);

            await unitOfWork.Compleate();

            return;
        }

        public async Task HandleFailedPaymentIntentEvent(PaymentIntent paymentIntent)
        {

            var order = await GetOrder(paymentIntent.Id);

            if (order == null)
                return;

            order.OrderStatus = OrderStatus.PaymentFaild;

            unitOfWork.Repository<Order>().Update(order);

            await unitOfWork.Compleate();

            return;
        }

        public async Task HandleSuccessPaymentIntentEvent(PaymentIntent paymentIntent)
        {
            var order = await unitOfWork.Repository<Order>().GetOneAsync(new OrderSpecificationBuilder().WithPaymentIntentId(paymentIntent.Id).Include("Items").Build());

            if (order == null)
                return;

            order.OrderStatus = OrderStatus.PaymentSucceeded;

            await stockService.UpdateStockAfterOrder(order.Items);

            unitOfWork.Repository<Order>().Update(order);

            await unitOfWork.Compleate();

            await cartService.DeleteCart(order.UserId.Value);

            return;
        }

        private async Task<Order?> GetOrder(string paymentIntentId)
        {
            var order = await unitOfWork.Repository<Order>().GetOneAsync(x => x.PaymentIntentId == paymentIntentId);

            return order;
        }
    }

}
