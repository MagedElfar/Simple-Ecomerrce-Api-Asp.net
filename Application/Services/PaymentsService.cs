using Application.Factories;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Interfaces.Strategies;
using Core.Specifications.SpecificationBuilder;

namespace Application.Services
{
    public class PaymentsService : IPaymentsService
    {
        private readonly ICartService cartService;
        private readonly PaymentFactory paymentFactory;
        private readonly IUnitOfWork unitOfWork;

        public PaymentsService(
            ICartService cartService,
            PaymentFactory paymentFactory,
            IUnitOfWork unitOfWork)
        {
            this.cartService = cartService;
            this.paymentFactory = paymentFactory;
            this.unitOfWork = unitOfWork;
        }

        public async Task<PaymentResult> CreateOrUpdatePaymentIntent(int orderId)
        {

            var spec = new OrderSpecificationBuilder()
                .WithId(orderId)
                .Include("Items")
                .Build();


            var order = await unitOfWork.Repository<Order>().GetOneAsync(spec);

            if (order == null)
                throw new NotFoundException("Order not found");

            IPaymentMethodStrategy paymentMethodStrategy = paymentFactory.CreatePaymentMethod(order.PaymentMethodId.Value);

            PaymentResult paymentIntent = await paymentMethodStrategy.Pay(order);

            return paymentIntent;
        }

    }
}
