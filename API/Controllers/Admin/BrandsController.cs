using Core.DTOS.Category;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin
{
    public class BrandsController : BaseAdminController
    {
        private readonly IBrandsService brandsService;

        public BrandsController(
            IBrandsService brandsService
        )
        {
            this.brandsService = brandsService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BrandDto>> GetById(int id)
        {
            return Ok(await brandsService.GetByIdAync(id));
        }

        [HttpPost("")]
        public async Task<ActionResult<BrandDto>> Create(AddBrandDto addBrandDto)
        {
            var brand = await brandsService.CreateBrandAsyc(addBrandDto);

            return CreatedAtAction(nameof(GetById), new { id = brand.BrandId }, brand);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<BrandDto>> GetById(int id, UpdateBrandDto updateBrandDto)
        {
            return Ok(await brandsService.UpdateBrandAsyc(id, updateBrandDto));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await brandsService.DeleteBrandAsyc(id);

            return NoContent();
        }
    }
}
