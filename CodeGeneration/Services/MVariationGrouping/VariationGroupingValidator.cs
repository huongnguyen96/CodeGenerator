
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MVariationGrouping
{
    public interface IVariationGroupingValidator : IServiceScoped
    {
        Task<bool> Create(VariationGrouping VariationGrouping);
        Task<bool> Update(VariationGrouping VariationGrouping);
        Task<bool> Delete(VariationGrouping VariationGrouping);
    }

    public class VariationGroupingValidator : IVariationGroupingValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public VariationGroupingValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(VariationGrouping VariationGrouping)
        {
            VariationGroupingFilter VariationGroupingFilter = new VariationGroupingFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = VariationGrouping.Id },
                Selects = VariationGroupingSelect.Id
            };

            int count = await UOW.VariationGroupingRepository.Count(VariationGroupingFilter);

            if (count == 0)
                VariationGrouping.AddError(nameof(VariationGroupingValidator), nameof(VariationGrouping.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(VariationGrouping VariationGrouping)
        {
            return VariationGrouping.IsValidated;
        }

        public async Task<bool> Update(VariationGrouping VariationGrouping)
        {
            if (await ValidateId(VariationGrouping))
            {
            }
            return VariationGrouping.IsValidated;
        }

        public async Task<bool> Delete(VariationGrouping VariationGrouping)
        {
            if (await ValidateId(VariationGrouping))
            {
            }
            return VariationGrouping.IsValidated;
        }
    }
}
