
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MUnit
{
    public interface IUnitValidator : IServiceScoped
    {
        Task<bool> Create(Unit Unit);
        Task<bool> Update(Unit Unit);
        Task<bool> Delete(Unit Unit);
    }

    public class UnitValidator : IUnitValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public UnitValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Unit Unit)
        {
            UnitFilter UnitFilter = new UnitFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = Unit.Id },
                Selects = UnitSelect.Id
            };

            int count = await UOW.UnitRepository.Count(UnitFilter);

            if (count == 0)
                Unit.AddError(nameof(UnitValidator), nameof(Unit.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(Unit Unit)
        {
            return Unit.IsValidated;
        }

        public async Task<bool> Update(Unit Unit)
        {
            if (await ValidateId(Unit))
            {
            }
            return Unit.IsValidated;
        }

        public async Task<bool> Delete(Unit Unit)
        {
            if (await ValidateId(Unit))
            {
            }
            return Unit.IsValidated;
        }
    }
}
