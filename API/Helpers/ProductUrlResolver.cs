using AutoMapper;
using Core.Dtos.Cart;
using Core.Dtos.Products;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class ProductUrlResolver :
        IValueResolver<Product, ProductDto, string>,
        IMemberValueResolver<CartItem, CartItemDto, Product, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Resolve for Product -> ProductDto
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            return GetImageUrl(source.imageUrl);
        }

        // Resolve for CartItem -> CartItemDto
        public string Resolve(CartItem source, CartItemDto destination, Product sourceMember, string destMember, ResolutionContext context)
        {
            return GetImageUrl(sourceMember?.imageUrl);
        }

        private string GetImageUrl(string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                return _configuration["ApiUrl"] + imageUrl;
            }

            return null; // Use a default image if null or empty
        }
    }
}
