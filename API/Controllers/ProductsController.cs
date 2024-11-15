using AutoMapper;
using Core.DTOS.Product;
using Core.DTOS.Shared;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListWithCountDto<ProductDto>>>> GetAllProduct([FromQuery] ProductQueryDto productQueryDto)
        {

           return Ok(await productsService.GetAndCountAll(productQueryDto));
        }

        [HttpGet("{id:int}" , Name = "GetProductById")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            return Ok(await productsService.GetProductAsync(id));
        }
    }
}
