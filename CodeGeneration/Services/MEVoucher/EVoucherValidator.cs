
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MEVoucher
{
    public interface IEVoucherValidator : IServiceScoped
    {
        Task<bool> Create(EVoucher EVoucher);
        Task<bool> Update(EVoucher EVoucher);
        Task<bool> Delete(EVoucher EVoucher);
    }

    public class EVoucherValidator : IEVoucherValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public EVoucherValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(EVoucher EVoucher)
        {
            EVoucherFilter EVoucherFilter = new EVoucherFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = EVoucher.Id },
                Selects = EVoucherSelect.Id
            };

            int count = await UOW.EVoucherRepository.Count(EVoucherFilter);

            if (count == 0)
                EVoucher.AddError(nameof(EVoucherValidator), nameof(EVoucher.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(EVoucher EVoucher)
        {
            return EVoucher.IsValidated;
        }

        public async Task<bool> Update(EVoucher EVoucher)
        {
            if (await ValidateId(EVoucher))
            {
            }
            return EVoucher.IsValidated;
        }

        public async Task<bool> Delete(EVoucher EVoucher)
        {
            if (await ValidateId(EVoucher))
            {
            }
            return EVoucher.IsValidated;
        }
    }
}
