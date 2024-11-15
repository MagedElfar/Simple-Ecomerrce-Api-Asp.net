using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Application.Services
{
    public class StockService : IStockService
    {
        private readonly IUnitOfWork unitOfWork;

        public StockService(
            IUnitOfWork unitOfWork
        ) {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> ISExist(int productId , int quantity)
        {
            var product = await GetProduct(productId);

            return product.Quantity >= quantity ;
        }
        public async Task<int> AddStockAsync(int productId, int quantity)
        {
            var product = await GetProduct(productId);

            product.Quantity += quantity;

            unitOfWork.Repository<Product>().Update(product);

            await unitOfWork.Compleate();

            return product.Quantity;
        }

        public async Task<int> ReduceStockAsync(int productId, int quantity)
        {
            var product = await GetProduct(productId);

            if (product.Quantity < quantity)
                throw new InvalidOperationException("Insufficient stock to reduce");

            product.Quantity -= quantity;

            unitOfWork.Repository<Product>().Update(product);

            await unitOfWork.Compleate();

            return product.Quantity;

        }

        public async Task<int> UpdateStock(int productId, int quantity)
        {
            var product = await GetProduct(productId);

            product.Quantity = quantity;

            unitOfWork.Repository<Product>().Update(product);

            await unitOfWork.Compleate();

            return product.Quantity;
        }

        private async Task<Product> GetProduct(int productId)
        {
            var product = await unitOfWork.Repository<Product>().GetByIdAsync(productId);

            if (product == null) 
                throw new NotFoundException("Product not found");

            return product;
        }

        public async Task UpdateStockAfterOrder(IEnumerable<OrderItem> items)
        {
            foreach(var item in items)
            {
                var product = await GetProduct(item.ProductId);

                if (product.Quantity < item.Quantity)
                    throw new InvalidOperationException("Insufficient stock to reduce");

                product.Quantity -= item.Quantity;

                unitOfWork.Repository<Product>().Update(product);
            }

            await unitOfWork.Compleate();
        }
    }
}
