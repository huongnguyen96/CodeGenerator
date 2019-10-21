
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MWard
{
    public interface IWardValidator : IServiceScoped
    {
        Task<bool> Create(Ward Ward);
        Task<bool> Update(Ward Ward);
        Task<bool> Delete(Ward Ward);
    }

    public class WardValidator : IWardValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public WardValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Ward Ward)
        {
            WardFilter WardFilter = new WardFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = Ward.Id },
                Selects = WardSelect.Id
            };

            int count = await UOW.WardRepository.Count(WardFilter);

            if (count == 0)
                Ward.AddError(nameof(WardValidator), nameof(Ward.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(Ward Ward)
        {
            return Ward.IsValidated;
        }

        public async Task<bool> Update(Ward Ward)
        {
            if (await ValidateId(Ward))
            {
            }
            return Ward.IsValidated;
        }

        public async Task<bool> Delete(Ward Ward)
        {
            if (await ValidateId(Ward))
            {
            }
            return Ward.IsValidated;
        }
    }
}
