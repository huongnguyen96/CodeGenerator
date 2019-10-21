
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MDiscount
{
    public interface IDiscountValidator : IServiceScoped
    {
        Task<bool> Create(Discount Discount);
        Task<bool> Update(Discount Discount);
        Task<bool> Delete(Discount Discount);
    }

    public class DiscountValidator : IDiscountValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public DiscountValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Discount Discount)
        {
            DiscountFilter DiscountFilter = new DiscountFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = Discount.Id },
                Selects = DiscountSelect.Id
            };

            int count = await UOW.DiscountRepository.Count(DiscountFilter);

            if (count == 0)
                Discount.AddError(nameof(DiscountValidator), nameof(Discount.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(Discount Discount)
        {
            return Discount.IsValidated;
        }

        public async Task<bool> Update(Discount Discount)
        {
            if (await ValidateId(Discount))
            {
            }
            return Discount.IsValidated;
        }

        public async Task<bool> Delete(Discount Discount)
        {
            if (await ValidateId(Discount))
            {
            }
            return Discount.IsValidated;
        }
    }
}
