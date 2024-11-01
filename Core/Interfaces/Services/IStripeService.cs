using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Interfaces.Services
{
    public interface IStripeService
    {
        Task<PaymentIntent> CreatePaymentIntent(decimal amount);
        Task<PaymentIntent> UpdatePaymentIntent(string paymentIntentId, decimal amount);
        Task<PaymentIntent> GetPaymentIntent(string id);
        Task HandleSuccessPaymentIntentEvent(PaymentIntent paymentIntent);
        Task HandleFailedPaymentIntentEvent(PaymentIntent paymentIntent);
        Task HandleCanceldPymentIntentEvent(PaymentIntent paymentIntent);
    }
}
