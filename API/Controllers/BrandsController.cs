using Core.Dtos.Brands;
using Core.Enums;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BrandsController : BaseApiController
    {
        private readonly IBrandsService brandsService;

        public BrandsController(IBrandsService brandsService)
        {
            this.brandsService = brandsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandWithProductCountDto>>> GetAllBrands()
        {
            var brands = await brandsService.GetAllWithProductsCount();
           return Ok(brands);
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Admin)}")]
        [HttpPost]
        public async Task<ActionResult<BrandDto>> AddBrand(AddBrandDto addBrandDto)
        {
            var brand = await brandsService.AddBrand(addBrandDto);
            return Ok(new BrandDto { BrandId = brand.Id , BrandName = brand.Name});
        }
    }
}
