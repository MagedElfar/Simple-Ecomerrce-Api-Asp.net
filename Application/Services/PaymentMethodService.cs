using AutoMapper;
using Core.DTOS.PaymentMethod;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PaymentMethodService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<PaymentMethodDto>> GetPaymentMethodsAsync()
        {
            var paymentMethods = await unitOfWork.Repository<PaymentMethod>().GetAllAsync();

            return  mapper.Map<IEnumerable<PaymentMethodDto>>(paymentMethods).ToList();
        }
    }
}
