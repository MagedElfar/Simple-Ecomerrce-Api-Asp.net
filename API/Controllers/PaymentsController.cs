using API.Extensions;
using AutoMapper;
using Core.DTOS.Payment;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   public class PaymentsController : BaseApiController
   {
        private readonly IPaymentsService paymentsService;
        private readonly IMapper mapper;

        public PaymentsController(IPaymentsService paymentsService , IMapper mapper)
        {
            this.paymentsService = paymentsService;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpPost("init")]
        public async Task<ActionResult<PaymentResult>> CreatePaymentIntent(CreatePaymentIntentDto createPaymentIntentDto)
        {

            var paymentIntent = await paymentsService.CreateOrUpdatePaymentIntent(createPaymentIntentDto.orderId);

            return Ok(paymentIntent);
        }
    }
}
