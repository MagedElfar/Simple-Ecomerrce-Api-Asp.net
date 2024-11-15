using AutoMapper;
using Core.DTOS.Product;
using Core.DTOS.Shared;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static StackExchange.Redis.Role;

namespace API.Controllers.Admin
{
    public class ProductsController : BaseAdminController
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }


        [HttpPost()]
        public async Task<ActionResult<ProductDto>> CreatProduct([FromForm] AddProductDto addProductDto)
        {
            var product = await productsService.CreateProductAsync(addProductDto);

            return CreatedAtAction("GetProductById", new { id = product.ProductId }, product);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            return Ok(await productsService.UpdateProductAsync(id, updateProductDto));
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await productsService.DeleteProductAsync(id);

            return NoContent();
        }



        [HttpPatch("{id:int}")]
        public async Task<ActionResult<string>> UpdateProductImage(int id, [FromForm] UploadMediaDto uploadMediaDto)
        {

            var url = await productsService.UpdateProductImage(id, uploadMediaDto.File);

            return Ok(new {ImageUrl = url});
        }
    }
}
