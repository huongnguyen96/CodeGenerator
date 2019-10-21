
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MCustomer
{
    public interface ICustomerValidator : IServiceScoped
    {
        Task<bool> Create(Customer Customer);
        Task<bool> Update(Customer Customer);
        Task<bool> Delete(Customer Customer);
    }

    public class CustomerValidator : ICustomerValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public CustomerValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Customer Customer)
        {
            CustomerFilter CustomerFilter = new CustomerFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = Customer.Id },
                Selects = CustomerSelect.Id
            };

            int count = await UOW.CustomerRepository.Count(CustomerFilter);

            if (count == 0)
                Customer.AddError(nameof(CustomerValidator), nameof(Customer.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(Customer Customer)
        {
            return Customer.IsValidated;
        }

        public async Task<bool> Update(Customer Customer)
        {
            if (await ValidateId(Customer))
            {
            }
            return Customer.IsValidated;
        }

        public async Task<bool> Delete(Customer Customer)
        {
            if (await ValidateId(Customer))
            {
            }
            return Customer.IsValidated;
        }
    }
}
