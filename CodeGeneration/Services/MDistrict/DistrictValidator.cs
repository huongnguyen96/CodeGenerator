
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MDistrict
{
    public interface IDistrictValidator : IServiceScoped
    {
        Task<bool> Create(District District);
        Task<bool> Update(District District);
        Task<bool> Delete(District District);
    }

    public class DistrictValidator : IDistrictValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public DistrictValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(District District)
        {
            DistrictFilter DistrictFilter = new DistrictFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = District.Id },
                Selects = DistrictSelect.Id
            };

            int count = await UOW.DistrictRepository.Count(DistrictFilter);

            if (count == 0)
                District.AddError(nameof(DistrictValidator), nameof(District.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(District District)
        {
            return District.IsValidated;
        }

        public async Task<bool> Update(District District)
        {
            if (await ValidateId(District))
            {
            }
            return District.IsValidated;
        }

        public async Task<bool> Delete(District District)
        {
            if (await ValidateId(District))
            {
            }
            return District.IsValidated;
        }
    }
}
