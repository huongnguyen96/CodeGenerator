
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MDiscountCustomerGrouping
{
    public interface IDiscountCustomerGroupingValidator : IServiceScoped
    {
        Task<bool> Create(DiscountCustomerGrouping DiscountCustomerGrouping);
        Task<bool> Update(DiscountCustomerGrouping DiscountCustomerGrouping);
        Task<bool> Delete(DiscountCustomerGrouping DiscountCustomerGrouping);
    }

    public class DiscountCustomerGroupingValidator : IDiscountCustomerGroupingValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public DiscountCustomerGroupingValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(DiscountCustomerGrouping DiscountCustomerGrouping)
        {
            DiscountCustomerGroupingFilter DiscountCustomerGroupingFilter = new DiscountCustomerGroupingFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = DiscountCustomerGrouping.Id },
                Selects = DiscountCustomerGroupingSelect.Id
            };

            int count = await UOW.DiscountCustomerGroupingRepository.Count(DiscountCustomerGroupingFilter);

            if (count == 0)
                DiscountCustomerGrouping.AddError(nameof(DiscountCustomerGroupingValidator), nameof(DiscountCustomerGrouping.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(DiscountCustomerGrouping DiscountCustomerGrouping)
        {
            return DiscountCustomerGrouping.IsValidated;
        }

        public async Task<bool> Update(DiscountCustomerGrouping DiscountCustomerGrouping)
        {
            if (await ValidateId(DiscountCustomerGrouping))
            {
            }
            return DiscountCustomerGrouping.IsValidated;
        }

        public async Task<bool> Delete(DiscountCustomerGrouping DiscountCustomerGrouping)
        {
            if (await ValidateId(DiscountCustomerGrouping))
            {
            }
            return DiscountCustomerGrouping.IsValidated;
        }
    }
}
