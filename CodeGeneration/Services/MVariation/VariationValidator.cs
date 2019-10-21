
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MVariation
{
    public interface IVariationValidator : IServiceScoped
    {
        Task<bool> Create(Variation Variation);
        Task<bool> Update(Variation Variation);
        Task<bool> Delete(Variation Variation);
    }

    public class VariationValidator : IVariationValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public VariationValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Variation Variation)
        {
            VariationFilter VariationFilter = new VariationFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = Variation.Id },
                Selects = VariationSelect.Id
            };

            int count = await UOW.VariationRepository.Count(VariationFilter);

            if (count == 0)
                Variation.AddError(nameof(VariationValidator), nameof(Variation.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(Variation Variation)
        {
            return Variation.IsValidated;
        }

        public async Task<bool> Update(Variation Variation)
        {
            if (await ValidateId(Variation))
            {
            }
            return Variation.IsValidated;
        }

        public async Task<bool> Delete(Variation Variation)
        {
            if (await ValidateId(Variation))
            {
            }
            return Variation.IsValidated;
        }
    }
}
