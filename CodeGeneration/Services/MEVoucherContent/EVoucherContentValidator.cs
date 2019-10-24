
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MEVoucherContent
{
    public interface IEVoucherContentValidator : IServiceScoped
    {
        Task<bool> Create(EVoucherContent EVoucherContent);
        Task<bool> Update(EVoucherContent EVoucherContent);
        Task<bool> Delete(EVoucherContent EVoucherContent);
    }

    public class EVoucherContentValidator : IEVoucherContentValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public EVoucherContentValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(EVoucherContent EVoucherContent)
        {
            EVoucherContentFilter EVoucherContentFilter = new EVoucherContentFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = EVoucherContent.Id },
                Selects = EVoucherContentSelect.Id
            };

            int count = await UOW.EVoucherContentRepository.Count(EVoucherContentFilter);

            if (count == 0)
                EVoucherContent.AddError(nameof(EVoucherContentValidator), nameof(EVoucherContent.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(EVoucherContent EVoucherContent)
        {
            return EVoucherContent.IsValidated;
        }

        public async Task<bool> Update(EVoucherContent EVoucherContent)
        {
            if (await ValidateId(EVoucherContent))
            {
            }
            return EVoucherContent.IsValidated;
        }

        public async Task<bool> Delete(EVoucherContent EVoucherContent)
        {
            if (await ValidateId(EVoucherContent))
            {
            }
            return EVoucherContent.IsValidated;
        }
    }
}
