
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MShippingAddress
{
    public interface IShippingAddressValidator : IServiceScoped
    {
        Task<bool> Create(ShippingAddress ShippingAddress);
        Task<bool> Update(ShippingAddress ShippingAddress);
        Task<bool> Delete(ShippingAddress ShippingAddress);
    }

    public class ShippingAddressValidator : IShippingAddressValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public ShippingAddressValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(ShippingAddress ShippingAddress)
        {
            ShippingAddressFilter ShippingAddressFilter = new ShippingAddressFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = ShippingAddress.Id },
                Selects = ShippingAddressSelect.Id
            };

            int count = await UOW.ShippingAddressRepository.Count(ShippingAddressFilter);

            if (count == 0)
                ShippingAddress.AddError(nameof(ShippingAddressValidator), nameof(ShippingAddress.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(ShippingAddress ShippingAddress)
        {
            return ShippingAddress.IsValidated;
        }

        public async Task<bool> Update(ShippingAddress ShippingAddress)
        {
            if (await ValidateId(ShippingAddress))
            {
            }
            return ShippingAddress.IsValidated;
        }

        public async Task<bool> Delete(ShippingAddress ShippingAddress)
        {
            if (await ValidateId(ShippingAddress))
            {
            }
            return ShippingAddress.IsValidated;
        }
    }
}
