
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MAdministrator
{
    public interface IAdministratorValidator : IServiceScoped
    {
        Task<bool> Create(Administrator Administrator);
        Task<bool> Update(Administrator Administrator);
        Task<bool> Delete(Administrator Administrator);
    }

    public class AdministratorValidator : IAdministratorValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public AdministratorValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Administrator Administrator)
        {
            AdministratorFilter AdministratorFilter = new AdministratorFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = Administrator.Id },
                Selects = AdministratorSelect.Id
            };

            int count = await UOW.AdministratorRepository.Count(AdministratorFilter);

            if (count == 0)
                Administrator.AddError(nameof(AdministratorValidator), nameof(Administrator.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(Administrator Administrator)
        {
            return Administrator.IsValidated;
        }

        public async Task<bool> Update(Administrator Administrator)
        {
            if (await ValidateId(Administrator))
            {
            }
            return Administrator.IsValidated;
        }

        public async Task<bool> Delete(Administrator Administrator)
        {
            if (await ValidateId(Administrator))
            {
            }
            return Administrator.IsValidated;
        }
    }
}
