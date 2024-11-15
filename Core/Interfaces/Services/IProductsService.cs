using Core.DTOS.Product;
using Core.DTOS.Shared;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IProductsService
    {
        Task<ListWithCountDto<ProductDto>> GetAndCountAll(ProductQueryDto productQueryDto);
        Task<ProductDto> GetProductAsync(int id);
        Task<ProductDto> CreateProductAsync(AddProductDto addProductDto);
        Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto);
        Task DeleteProductAsync(int id);
        Task<string> UpdateProductImage(int id, IFormFile file);
        Task DeleteProductImage(int id);
    }
}
