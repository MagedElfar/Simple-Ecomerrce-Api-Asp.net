using AutoMapper;
using Core.DTOS.Cart;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CartService : ICartService
    {
        private readonly IMapper mapper;
        private readonly IRedisRepository<Cart> cartRepository;
        private readonly IUnitOfWork unitOfWork;

        public CartService(
            IMapper mapper,
            IRedisRepository<Cart> cartRepository,
            IUnitOfWork unitOfWork,
            ILogger<CartService> logger)
        {
            this.mapper = mapper;
            this.cartRepository = cartRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteCart(int id)
        {
            return await cartRepository.DeleteAsync(id.ToString());
        }

        public async Task<Cart> GetCartByIdAsync(int id)
        {
            var cart = await cartRepository.GetAsync(id.ToString());

            if (cart?.Items != null)
                await PopulateCartItemsWithProducts(cart);

            return cart;
        }

        public async Task<CartDto> MappingCart(int id)
        {
            var cart = await GetCartByIdAsync(id);

            return cart is not null ? mapper.Map<CartDto>(cart) : new CartDto { CartId = id };
        }

        public async Task<CartDto> UpdateCart(int id, UpdateCartDto updateCartDto)
        {
            var cart = new Cart { Id = id };

            cart.Items = mapper.Map<IEnumerable<CartItem>>(updateCartDto.CartItems)
                .GroupBy(x => x.ProductId)
                .Select(g => new CartItem
                {
                    ProductId = g.Key,
                    Quantity = g.Sum(x => x.Quantity)
                });

            await CheckItemsBeforeUpdate(cart);

            var isUpdated = await cartRepository.UpdateAsync(cart);

            if (!isUpdated)
                throw new Exception("An unexpected error occurred while updating the cart, please try again.");

            return await MappingCart(cart.Id);
        }

        private async Task CheckItemsBeforeUpdate(Cart cart)
        {
            var ids = cart.Items.Select(x => x.ProductId).ToList();
            var products = await unitOfWork.Repository<Product>().GetAllAsync(x => ids.Contains(x.Id));
            var productDictionary = products.ToDictionary(p => p.Id);

            var nonExistentProducts = ids.Where(id => !productDictionary.ContainsKey(id)).ToList();
            if (nonExistentProducts.Any())
            {
                string errorMessage = $"Cart can't be updated. Products with IDs {string.Join(", ", nonExistentProducts)} do not exist.";
                throw new BadRequestException(errorMessage);
            }
        }

        private async Task PopulateCartItemsWithProducts(Cart cart)
        {
            var ids = cart.Items.Select(x => x.ProductId).ToList();
            if (!ids.Any()) return;

            var products = await unitOfWork.Repository<Product>().GetAllAsync(x => ids.Contains(x.Id));
            var productDictionary = products.ToDictionary(p => p.Id);

            foreach (var item in cart.Items)
            {
                if (!productDictionary.TryGetValue(item.ProductId, out var product))
                {
                    item.Product = null;
                }
                else
                {
                    item.Product = product;
                }
            }
        }
    }
}
