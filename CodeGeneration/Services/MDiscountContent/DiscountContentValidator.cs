
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MDiscountContent
{
    public interface IDiscountContentValidator : IServiceScoped
    {
        Task<bool> Create(DiscountContent DiscountContent);
        Task<bool> Update(DiscountContent DiscountContent);
        Task<bool> Delete(DiscountContent DiscountContent);
    }

    public class DiscountContentValidator : IDiscountContentValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public DiscountContentValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(DiscountContent DiscountContent)
        {
            DiscountContentFilter DiscountContentFilter = new DiscountContentFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = DiscountContent.Id },
                Selects = DiscountContentSelect.Id
            };

            int count = await UOW.DiscountContentRepository.Count(DiscountContentFilter);

            if (count == 0)
                DiscountContent.AddError(nameof(DiscountContentValidator), nameof(DiscountContent.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(DiscountContent DiscountContent)
        {
            return DiscountContent.IsValidated;
        }

        public async Task<bool> Update(DiscountContent DiscountContent)
        {
            if (await ValidateId(DiscountContent))
            {
            }
            return DiscountContent.IsValidated;
        }

        public async Task<bool> Delete(DiscountContent DiscountContent)
        {
            if (await ValidateId(DiscountContent))
            {
            }
            return DiscountContent.IsValidated;
        }
    }
}
