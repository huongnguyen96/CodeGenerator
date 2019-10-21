
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MAdministrator
{
    public interface IAdministratorService : IServiceScoped
    {
        Task<int> Count(AdministratorFilter AdministratorFilter);
        Task<List<Administrator>> List(AdministratorFilter AdministratorFilter);
        Task<Administrator> Get(long Id);
        Task<Administrator> Create(Administrator Administrator);
        Task<Administrator> Update(Administrator Administrator);
        Task<Administrator> Delete(Administrator Administrator);
    }

    public class AdministratorService : IAdministratorService
    {
        public IUOW UOW;
        public IAdministratorValidator AdministratorValidator;

        public AdministratorService(
            IUOW UOW, 
            IAdministratorValidator AdministratorValidator
        )
        {
            this.UOW = UOW;
            this.AdministratorValidator = AdministratorValidator;
        }
        public async Task<int> Count(AdministratorFilter AdministratorFilter)
        {
            int result = await UOW.AdministratorRepository.Count(AdministratorFilter);
            return result;
        }

        public async Task<List<Administrator>> List(AdministratorFilter AdministratorFilter)
        {
            List<Administrator> Administrators = await UOW.AdministratorRepository.List(AdministratorFilter);
            return Administrators;
        }

        public async Task<Administrator> Get(long Id)
        {
            Administrator Administrator = await UOW.AdministratorRepository.Get(Id);
            if (Administrator == null)
                return null;
            return Administrator;
        }

        public async Task<Administrator> Create(Administrator Administrator)
        {
            if (!await AdministratorValidator.Create(Administrator))
                return Administrator;

            try
            {
               
                await UOW.Begin();
                await UOW.AdministratorRepository.Create(Administrator);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(Administrator, "", nameof(AdministratorService));
                return await UOW.AdministratorRepository.Get(Administrator.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(AdministratorService));
                throw new MessageException(ex);
            }
        }

        public async Task<Administrator> Update(Administrator Administrator)
        {
            if (!await AdministratorValidator.Update(Administrator))
                return Administrator;
            try
            {
                var oldData = await UOW.AdministratorRepository.Get(Administrator.Id);

                await UOW.Begin();
                await UOW.AdministratorRepository.Update(Administrator);
                await UOW.Commit();

                var newData = await UOW.AdministratorRepository.Get(Administrator.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(AdministratorService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(AdministratorService));
                throw new MessageException(ex);
            }
        }

        public async Task<Administrator> Delete(Administrator Administrator)
        {
            if (!await AdministratorValidator.Delete(Administrator))
                return Administrator;

            try
            {
                await UOW.Begin();
                await UOW.AdministratorRepository.Delete(Administrator);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", Administrator, nameof(AdministratorService));
                return Administrator;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(AdministratorService));
                throw new MessageException(ex);
            }
        }
    }
}
