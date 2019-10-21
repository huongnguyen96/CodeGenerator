
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MProvince
{
    public interface IProvinceValidator : IServiceScoped
    {
        Task<bool> Create(Province Province);
        Task<bool> Update(Province Province);
        Task<bool> Delete(Province Province);
    }

    public class ProvinceValidator : IProvinceValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public ProvinceValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Province Province)
        {
            ProvinceFilter ProvinceFilter = new ProvinceFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = Province.Id },
                Selects = ProvinceSelect.Id
            };

            int count = await UOW.ProvinceRepository.Count(ProvinceFilter);

            if (count == 0)
                Province.AddError(nameof(ProvinceValidator), nameof(Province.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(Province Province)
        {
            return Province.IsValidated;
        }

        public async Task<bool> Update(Province Province)
        {
            if (await ValidateId(Province))
            {
            }
            return Province.IsValidated;
        }

        public async Task<bool> Delete(Province Province)
        {
            if (await ValidateId(Province))
            {
            }
            return Province.IsValidated;
        }
    }
}
