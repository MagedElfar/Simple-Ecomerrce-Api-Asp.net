using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Interfaces.Strategies;
using PaymentIntent = Stripe.PaymentIntent;

namespace Application.Strategies
{
    public class StripePaymentStrategy : IPaymentMethodStrategy
    {
        private readonly IStripeService stripeService;
        private readonly IUnitOfWork unitOfWork;

        public StripePaymentStrategy(IStripeService stripeService, IUnitOfWork unitOfWork)
        {
            this.stripeService = stripeService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<PaymentResult> Pay(Order order)
        {
            PaymentIntent paymentIntent;

            if (order.PaymentIntentId == null)
                paymentIntent = await stripeService.CreatePaymentIntent(order.SubTotal);
            else
                paymentIntent = await stripeService.UpdatePaymentIntent(order.PaymentIntentId, order.SubTotal);

            order.PaymentIntentId = paymentIntent.Id;

            unitOfWork.Repository<Order>().Update(order);

            await unitOfWork.Compleate();

            return new PaymentResult
            {
                PaymentId = paymentIntent.Id,
                ClientSecret = paymentIntent.ClientSecret,
                Status = paymentIntent.Status
            };

        }
    }
}
