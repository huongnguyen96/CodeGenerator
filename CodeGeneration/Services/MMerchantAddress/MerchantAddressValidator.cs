
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MMerchantAddress
{
    public interface IMerchantAddressValidator : IServiceScoped
    {
        Task<bool> Create(MerchantAddress MerchantAddress);
        Task<bool> Update(MerchantAddress MerchantAddress);
        Task<bool> Delete(MerchantAddress MerchantAddress);
    }

    public class MerchantAddressValidator : IMerchantAddressValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public MerchantAddressValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(MerchantAddress MerchantAddress)
        {
            MerchantAddressFilter MerchantAddressFilter = new MerchantAddressFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = MerchantAddress.Id },
                Selects = MerchantAddressSelect.Id
            };

            int count = await UOW.MerchantAddressRepository.Count(MerchantAddressFilter);

            if (count == 0)
                MerchantAddress.AddError(nameof(MerchantAddressValidator), nameof(MerchantAddress.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(MerchantAddress MerchantAddress)
        {
            return MerchantAddress.IsValidated;
        }

        public async Task<bool> Update(MerchantAddress MerchantAddress)
        {
            if (await ValidateId(MerchantAddress))
            {
            }
            return MerchantAddress.IsValidated;
        }

        public async Task<bool> Delete(MerchantAddress MerchantAddress)
        {
            if (await ValidateId(MerchantAddress))
            {
            }
            return MerchantAddress.IsValidated;
        }
    }
}
