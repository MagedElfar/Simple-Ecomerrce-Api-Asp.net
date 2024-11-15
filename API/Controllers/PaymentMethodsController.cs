using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PaymentMethodsController : BaseApiController
    {
        private readonly IPaymentMethodService paymentMethodService;

        public PaymentMethodsController(IPaymentMethodService paymentMethodService)
        {
            this.paymentMethodService = paymentMethodService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> Get() {

            return Ok(await paymentMethodService.GetPaymentMethodsAsync());
        }
    }
}
