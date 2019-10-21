
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MPartner
{
    public interface IPartnerValidator : IServiceScoped
    {
        Task<bool> Create(Partner Partner);
        Task<bool> Update(Partner Partner);
        Task<bool> Delete(Partner Partner);
    }

    public class PartnerValidator : IPartnerValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public PartnerValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Partner Partner)
        {
            PartnerFilter PartnerFilter = new PartnerFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = Partner.Id },
                Selects = PartnerSelect.Id
            };

            int count = await UOW.PartnerRepository.Count(PartnerFilter);

            if (count == 0)
                Partner.AddError(nameof(PartnerValidator), nameof(Partner.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(Partner Partner)
        {
            return Partner.IsValidated;
        }

        public async Task<bool> Update(Partner Partner)
        {
            if (await ValidateId(Partner))
            {
            }
            return Partner.IsValidated;
        }

        public async Task<bool> Delete(Partner Partner)
        {
            if (await ValidateId(Partner))
            {
            }
            return Partner.IsValidated;
        }
    }
}
