using Core.DTOS.PaymentMethod;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentMethodDto>> GetPaymentMethodsAsync();
    }
}
