using Core.Dtos.Products;
using Core.Errors;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using Core.Specifications;
using Core.Dtos.Pagning;
using Microsoft.AspNetCore.Authorization;
using Core.Enums;
using Core.Dtos;
using Core.Specifications.SpecificationBuilder;

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

            var bulider = new ProductSpecificationBuilder()
                .WithName(productQueryDto.Name)
                .WithTypeId(productQueryDto.CategoryId)
                .WithBrandId(productQueryDto.BrandId)
                .WithLimit(productQueryDto.Limit)
                .WithPage(productQueryDto.Page)
                .WithOrderBy(productQueryDto.Sort, productQueryDto.Asc ?? true)
                .Include("Brand")
                .Include("ProductType");

            var result = await productsService.FindAndCountAll(bulider.Build());


            return Ok(new PagningListDto<ProductDto>
            {
                Count = result.Count,
                Items = mapper.Map<List<ProductDto>>(result.Rows )
            });
        }

        [HttpGet("{id:int}", Name = "GetSingleProduct")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductSpecificationBuilder()
                .WithTypeId(id)
                .Include("Brand")
                .Include("ProductType")
                .Build();


            var product = await productsService.FindOneAync(spec);

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
