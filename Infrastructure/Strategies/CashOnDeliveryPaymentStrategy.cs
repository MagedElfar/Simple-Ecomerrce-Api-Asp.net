using Core.Entities;
using Core.Enums;
using Core.Interfaces.Services;
using Core.Interfaces.Strategies;

namespace Infrastructure.Strategies
{
    public class CashOnDeliveryPaymentStrategy : IPaymentMethodStrategy
    {
        private readonly IOrdersService ordersService;
        private readonly ICartService cartService;

        public CashOnDeliveryPaymentStrategy(IOrdersService ordersService , ICartService cartService)
        {
            this.ordersService = ordersService;
            this.cartService = cartService;
        }

        public async Task<PaymentResult> Pay(Order order)
        {

            order.OrderStatus = OrderStatusEnum.Confirmed;

            await ordersService.UpdateAsync(order);

            await cartService.DeleteCart(order.UserId.ToString());

            return new PaymentResult
            {
                Status = OrderStatusEnum.Confirmed.ToString(),
            };

        }
    }
}
