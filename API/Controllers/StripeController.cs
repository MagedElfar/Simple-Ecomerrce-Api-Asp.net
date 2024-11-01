using Core.Interfaces.Services;
using Core.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using Stripe.Events;
namespace API.Controllers
{
    
    public class StripeController : BaseApiController
    {
        private readonly IOptions<StripeOption> options;
        private readonly IStripeService stripeService;

        public StripeController(IOptions<StripeOption> options , IStripeService stripeService)
        {
            this.options = options;
            this.stripeService = stripeService;
        }
        [HttpPost("webhook")]
        public async Task<ActionResult> HandelWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();


            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"] , options.Value.WebHookSecret, throwOnApiVersionMismatch: false);

            PaymentIntent? paymentIntent;

            switch (stripeEvent.Type) {
                case EventTypes.PaymentIntentSucceeded:
                     paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    if(paymentIntent is not null)
                        await stripeService.HandleSuccessPaymentIntentEvent(paymentIntent);
                    break;

                case EventTypes.PaymentIntentPaymentFailed:
                    paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    if (paymentIntent is not null)
                        await stripeService.HandleFailedPaymentIntentEvent(paymentIntent);
                    break;

                case EventTypes.PaymentIntentCanceled:
                    paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    if (paymentIntent is not null)
                        await stripeService.HandleCanceldPymentIntentEvent(paymentIntent);
                    break;
            }

            return Ok();
        }
    }
}
