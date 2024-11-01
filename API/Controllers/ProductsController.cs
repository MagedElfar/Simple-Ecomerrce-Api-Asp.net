using Core.Dtos.Products;
using Core.Errors;
using AutoMapper;
using Core.Entities;
using Core.Specifications.Products;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using Core.Specifications;
using Core.Dtos.Pagning;
using Microsoft.AspNetCore.Authorization;
using Core.Enums;
using Core.Dtos;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductsService productsService;
        private readonly IMapper mapper;

        public ProductsController(IProductsService productsService , IMapper mapper)
        {
            this.productsService = productsService;
            this.mapper = mapper;
        }

        [HttpGet(Name = "GetAll")]
        public async Task<ActionResult<IEnumerable<PagningListDto<ProductDto>>>> GetAllProduct([FromQuery] ProductQueryDto productQueryDto)
        {

            var spec = new ProductFilterSpecifications(
                productQueryDto.Sort, 
                productQueryDto.Name, 
                productQueryDto.BrandId, 
                productQueryDto.CategoryId,
                productQueryDto.Limit,
                productQueryDto.Page,
                new[] {"Brand"},
                ascending: productQueryDto.Asc ?? true
            );

            var result = await productsService.FindAndCountAll(spec);


            return Ok(new PagningListDto<ProductDto>
            {
                Count = result.Count,
                Items = mapper.Map<List<ProductDto>>(result.Rows )
            });
        }

        [HttpGet("{id:int}", Name = "GetSingleProduct")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {


            var product = await productsService.FindOneAync(new BaseSpecifications<Product>(x=> x.Id == id , includes: new[] {"Brand" , "ProductType"}));

            if (product is null) return NotFound(new ApiErrorResponse(400 , "Product not Found"));

            return Ok(mapper.Map<Product , ProductDto>(product));
        }

        [HttpPost()]
        [Authorize(Roles = nameof(RoleEnum.Admin))]
        public async Task<ActionResult<ProductDto>> AddProduct([FromForm] AddProductDto addProductDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);  

            var product = await productsService.AddProductAsync(addProductDto);

            return CreatedAtAction(nameof(GetProduct), new {id = product.Id} , mapper.Map<Product, ProductDto>(product));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = nameof(RoleEnum.Admin))]
        public async Task<ActionResult<ProductDto>> UodateProduct(int id , UpdateProductDto updateProductDto)
        {
            var product = await productsService.UpdateProductAsync(id , updateProductDto);

            return Ok(mapper.Map<Product, ProductDto>(product));
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = nameof(RoleEnum.Admin))]
        public async Task<ActionResult<ProductDto>> DeleteProduct(int id)
        {
            await productsService.DeleteProductAsync(id);

            return NoContent();
        }



        [HttpPatch("{id:int}")]
        [Authorize(Roles = nameof(RoleEnum.Admin))]
        public async Task<ActionResult<ProductDto>> UpdateProductImage(int id , [FromForm] UploadMediaDto uploadMediaDto)
        {

            var product = await productsService.UpdateProductImage(id, uploadMediaDto.File);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, mapper.Map<Product, ProductDto>(product));
        }

    }
}
