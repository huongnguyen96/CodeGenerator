
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MCustomerGrouping
{
    public interface ICustomerGroupingValidator : IServiceScoped
    {
        Task<bool> Create(CustomerGrouping CustomerGrouping);
        Task<bool> Update(CustomerGrouping CustomerGrouping);
        Task<bool> Delete(CustomerGrouping CustomerGrouping);
    }

    public class CustomerGroupingValidator : ICustomerGroupingValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public CustomerGroupingValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(CustomerGrouping CustomerGrouping)
        {
            CustomerGroupingFilter CustomerGroupingFilter = new CustomerGroupingFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = CustomerGrouping.Id },
                Selects = CustomerGroupingSelect.Id
            };

            int count = await UOW.CustomerGroupingRepository.Count(CustomerGroupingFilter);

            if (count == 0)
                CustomerGrouping.AddError(nameof(CustomerGroupingValidator), nameof(CustomerGrouping.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(CustomerGrouping CustomerGrouping)
        {
            return CustomerGrouping.IsValidated;
        }

        public async Task<bool> Update(CustomerGrouping CustomerGrouping)
        {
            if (await ValidateId(CustomerGrouping))
            {
            }
            return CustomerGrouping.IsValidated;
        }

        public async Task<bool> Delete(CustomerGrouping CustomerGrouping)
        {
            if (await ValidateId(CustomerGrouping))
            {
            }
            return CustomerGrouping.IsValidated;
        }
    }
}
