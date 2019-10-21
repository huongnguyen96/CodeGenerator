
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MBrand
{
    public interface IBrandValidator : IServiceScoped
    {
        Task<bool> Create(Brand Brand);
        Task<bool> Update(Brand Brand);
        Task<bool> Delete(Brand Brand);
    }

    public class BrandValidator : IBrandValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public BrandValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Brand Brand)
        {
            BrandFilter BrandFilter = new BrandFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = Brand.Id },
                Selects = BrandSelect.Id
            };

            int count = await UOW.BrandRepository.Count(BrandFilter);

            if (count == 0)
                Brand.AddError(nameof(BrandValidator), nameof(Brand.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(Brand Brand)
        {
            return Brand.IsValidated;
        }

        public async Task<bool> Update(Brand Brand)
        {
            if (await ValidateId(Brand))
            {
            }
            return Brand.IsValidated;
        }

        public async Task<bool> Delete(Brand Brand)
        {
            if (await ValidateId(Brand))
            {
            }
            return Brand.IsValidated;
        }
    }
}
