using Core.Dtos.Products;
using Core.Entities;
using Core.Specifications;
using Microsoft.AspNetCore.Http;


namespace Core.Interfaces.Services
{
    public interface IProductsService:IBaseService<Product>
    {
        Task<Product> AddProductAsync(AddProductDto addProductDto);
        Task<Product> UpdateProductAsync(int id ,UpdateProductDto updateProductDto);
        Task DeleteProductAsync(int id);
        Task<Product> UpdateProductImage(int id , IFormFile file);
        Task DeleteProductImage(int id);


    }
}
