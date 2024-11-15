using Core.Entities;
using Core.Enums;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Interfaces.Strategies;

namespace Application.Strategies
{
    public class CashOnDeliveryPaymentStrategy : IPaymentMethodStrategy
    {
        private readonly ICartService cartService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IStockService stockService;

        public CashOnDeliveryPaymentStrategy(ICartService cartService, IUnitOfWork unitOfWork, IStockService stockService)
        {
            this.cartService = cartService;
            this.unitOfWork = unitOfWork;
            this.stockService = stockService;
        }

        public async Task<PaymentResult> Pay(Order order)
        {

            order.OrderStatus = OrderStatus.Confirmed;


            await stockService.UpdateStockAfterOrder(order.Items);

            unitOfWork.Repository<Order>().Update(order);

            await unitOfWork.Compleate();

            await cartService.DeleteCart(order.UserId.Value);

            return new PaymentResult
            {
                Status = OrderStatus.Confirmed.ToString(),
            };

        }
    }
}
