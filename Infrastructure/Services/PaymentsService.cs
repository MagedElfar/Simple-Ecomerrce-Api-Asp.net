using Core.Dtos.Order;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Interfaces.Strategies;
using Infrastructure.Factories;

namespace Infrastructure.Services
{
    public class PaymentsService : IPaymentsService
    {
        private readonly ICartService cartService;
        private readonly IProductsService productsService;
        private readonly IOrdersService ordersService;
        private readonly PaymentFactory paymentFactory;

        public PaymentsService(
            ICartService cartService,
            IProductsService productsService,
            IOrdersService ordersService,
            PaymentFactory paymentFactory
        )
        {
            this.cartService = cartService;
            this.productsService = productsService;
            this.ordersService = ordersService;
            this.paymentFactory = paymentFactory;
        }

        public async Task<PaymentResult> CreateOrUpdatePaymentIntent(int orderId)
        {
            var order = await ordersService.FindByIdAsync(orderId);

            if (order == null)
                throw new NotFoundException("Order not found");

            IPaymentMethodStrategy paymentMethodStrategy = paymentFactory.CreatePaymentMethod(order.PaymentMethodId.Value);

            PaymentResult paymentIntent = await paymentMethodStrategy.Pay(order);

            return paymentIntent;
        }

    }
}
