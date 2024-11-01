using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PaymentMethodsController : BaseApiController
    {
        private readonly IBaseService<PaymentMethod> paymentMethodService;

        public PaymentMethodsController(IBaseService<PaymentMethod> paymentMethodService)
        {
            this.paymentMethodService = paymentMethodService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> Get() {
            var methods  = await paymentMethodService.FindAllAsync();

            return Ok(methods.Select(x => new { x.Id, x.Name }));
        }
    }
}
