using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Specifications;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CartService:ICartService
    {
        private readonly ICartRepository cartRepository;
        private readonly IProductsService productsService;

        public CartService(ICartRepository cartRepository , IProductsService productsService)
        {
            this.cartRepository = cartRepository;
            this.productsService = productsService;
        }

        public async Task<bool> DeleteCart(string id)
        {
            return await cartRepository.DeleteAsync(id);
        }

        public async Task<Cart> GetCartByIdAsync(string id)
        {
            var cart = await cartRepository.GetCartAsync(id);

            if(cart?.Items != null)
                await PopulateCartItemsWithProducts(cart);

            return cart;
        }

        public async Task<Cart> UpdateCart(Cart cart)
        {

            var ids = cart.Items.Select(x => x.ProductId).ToList();

            var products = await productsService.FindAllAsync(x => ids.Contains(x.Id));

            var existentProducts = products.Select(p => p.Id).ToHashSet();

            var nonExistentProducts = ids.Where(x => !existentProducts.Contains(x)).ToList();


            if (nonExistentProducts.Any()) {
                var error = new StringBuilder();

                error.Append("Cart Can't Update , Products with ids {");
                int count = nonExistentProducts.Count();
                for (int i = 0; i < count; i++)
                {
                    error.Append(nonExistentProducts.ElementAt(i));
                    if (i < count - 1)
                    {
                        error.Append(", ");
                    }
                }

                error.Append("} not existing");

                throw new BadRequsetException(error.ToString());
            }


            var isCreated = await cartRepository.Update(cart);


            if (!isCreated) throw new Exception("An unexpected error occurred while creating the cart please try again.");


            return await GetCartByIdAsync (cart.Id);

        }

        private async Task PopulateCartItemsWithProducts(Cart cart)
        {
            var ids = cart.Items.Select(x => x.ProductId).ToList();

            if (ids.Any())
            {
                var products = await productsService.FindAllAsync(x => ids.Contains(x.Id));
                var productDictionary = products.ToDictionary(p => p.Id);

                foreach (var item in cart.Items)
                {
                    if (productDictionary.TryGetValue(item.ProductId, out var product))
                    {
                        item.Product = product;
                    }
                    else
                    {
                        item.Product = null;
                    }
                }

            }
        }

    }
}
