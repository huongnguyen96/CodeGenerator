
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MMerchant
{
    public interface IMerchantValidator : IServiceScoped
    {
        Task<bool> Create(Merchant Merchant);
        Task<bool> Update(Merchant Merchant);
        Task<bool> Delete(Merchant Merchant);
    }

    public class MerchantValidator : IMerchantValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public MerchantValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Merchant Merchant)
        {
            MerchantFilter MerchantFilter = new MerchantFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = Merchant.Id },
                Selects = MerchantSelect.Id
            };

            int count = await UOW.MerchantRepository.Count(MerchantFilter);

            if (count == 0)
                Merchant.AddError(nameof(MerchantValidator), nameof(Merchant.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(Merchant Merchant)
        {
            return Merchant.IsValidated;
        }

        public async Task<bool> Update(Merchant Merchant)
        {
            if (await ValidateId(Merchant))
            {
            }
            return Merchant.IsValidated;
        }

        public async Task<bool> Delete(Merchant Merchant)
        {
            if (await ValidateId(Merchant))
            {
            }
            return Merchant.IsValidated;
        }
    }
}
