
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MPaymentMethod
{
    public interface IPaymentMethodValidator : IServiceScoped
    {
        Task<bool> Create(PaymentMethod PaymentMethod);
        Task<bool> Update(PaymentMethod PaymentMethod);
        Task<bool> Delete(PaymentMethod PaymentMethod);
    }

    public class PaymentMethodValidator : IPaymentMethodValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public PaymentMethodValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(PaymentMethod PaymentMethod)
        {
            PaymentMethodFilter PaymentMethodFilter = new PaymentMethodFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = PaymentMethod.Id },
                Selects = PaymentMethodSelect.Id
            };

            int count = await UOW.PaymentMethodRepository.Count(PaymentMethodFilter);

            if (count == 0)
                PaymentMethod.AddError(nameof(PaymentMethodValidator), nameof(PaymentMethod.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(PaymentMethod PaymentMethod)
        {
            return PaymentMethod.IsValidated;
        }

        public async Task<bool> Update(PaymentMethod PaymentMethod)
        {
            if (await ValidateId(PaymentMethod))
            {
            }
            return PaymentMethod.IsValidated;
        }

        public async Task<bool> Delete(PaymentMethod PaymentMethod)
        {
            if (await ValidateId(PaymentMethod))
            {
            }
            return PaymentMethod.IsValidated;
        }
    }
}
