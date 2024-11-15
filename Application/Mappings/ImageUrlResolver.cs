using AutoMapper;
using Core.DTOS.Product;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class ImageUrlResolver : IValueResolver<Product, ProductDto, string>
    {

        private readonly IConfiguration configuration;
        public ImageUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // Resolve for Product -> ProductDto
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            return GetImageUrl(source.ImageUrl);
        }

        private string GetImageUrl(string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                return configuration["ApiUrl"] + imageUrl;
            }

            return null; // Use a default image if null or empty
        }
    }
}
