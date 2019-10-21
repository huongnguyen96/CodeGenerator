
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MDiscountItem
{
    public interface IDiscountItemValidator : IServiceScoped
    {
        Task<bool> Create(DiscountItem DiscountItem);
        Task<bool> Update(DiscountItem DiscountItem);
        Task<bool> Delete(DiscountItem DiscountItem);
    }

    public class DiscountItemValidator : IDiscountItemValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public DiscountItemValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(DiscountItem DiscountItem)
        {
            DiscountItemFilter DiscountItemFilter = new DiscountItemFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = DiscountItem.Id },
                Selects = DiscountItemSelect.Id
            };

            int count = await UOW.DiscountItemRepository.Count(DiscountItemFilter);

            if (count == 0)
                DiscountItem.AddError(nameof(DiscountItemValidator), nameof(DiscountItem.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(DiscountItem DiscountItem)
        {
            return DiscountItem.IsValidated;
        }

        public async Task<bool> Update(DiscountItem DiscountItem)
        {
            if (await ValidateId(DiscountItem))
            {
            }
            return DiscountItem.IsValidated;
        }

        public async Task<bool> Delete(DiscountItem DiscountItem)
        {
            if (await ValidateId(DiscountItem))
            {
            }
            return DiscountItem.IsValidated;
        }
    }
}
