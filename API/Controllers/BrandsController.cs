using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BrandsController : BaseApiController
    {
        private readonly IBrandsService brandsService;

        public BrandsController(
            IBrandsService brandsService
        )
        {
            this.brandsService = brandsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {

            return Ok( await brandsService.GetAll() );
        }
    }
}
