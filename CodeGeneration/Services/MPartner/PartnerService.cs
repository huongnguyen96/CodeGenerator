
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MPartner
{
    public interface IPartnerService : IServiceScoped
    {
        Task<int> Count(PartnerFilter PartnerFilter);
        Task<List<Partner>> List(PartnerFilter PartnerFilter);
        Task<Partner> Get(long Id);
        Task<Partner> Create(Partner Partner);
        Task<Partner> Update(Partner Partner);
        Task<Partner> Delete(Partner Partner);
    }

    public class PartnerService : IPartnerService
    {
        public IUOW UOW;
        public IPartnerValidator PartnerValidator;

        public PartnerService(
            IUOW UOW, 
            IPartnerValidator PartnerValidator
        )
        {
            this.UOW = UOW;
            this.PartnerValidator = PartnerValidator;
        }
        public async Task<int> Count(PartnerFilter PartnerFilter)
        {
            int result = await UOW.PartnerRepository.Count(PartnerFilter);
            return result;
        }

        public async Task<List<Partner>> List(PartnerFilter PartnerFilter)
        {
            List<Partner> Partners = await UOW.PartnerRepository.List(PartnerFilter);
            return Partners;
        }

        public async Task<Partner> Get(long Id)
        {
            Partner Partner = await UOW.PartnerRepository.Get(Id);
            if (Partner == null)
                return null;
            return Partner;
        }

        public async Task<Partner> Create(Partner Partner)
        {
            if (!await PartnerValidator.Create(Partner))
                return Partner;

            try
            {
               
                await UOW.Begin();
                await UOW.PartnerRepository.Create(Partner);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(Partner, "", nameof(PartnerService));
                return await UOW.PartnerRepository.Get(Partner.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(PartnerService));
                throw new MessageException(ex);
            }
        }

        public async Task<Partner> Update(Partner Partner)
        {
            if (!await PartnerValidator.Update(Partner))
                return Partner;
            try
            {
                var oldData = await UOW.PartnerRepository.Get(Partner.Id);

                await UOW.Begin();
                await UOW.PartnerRepository.Update(Partner);
                await UOW.Commit();

                var newData = await UOW.PartnerRepository.Get(Partner.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(PartnerService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(PartnerService));
                throw new MessageException(ex);
            }
        }

        public async Task<Partner> Delete(Partner Partner)
        {
            if (!await PartnerValidator.Delete(Partner))
                return Partner;

            try
            {
                await UOW.Begin();
                await UOW.PartnerRepository.Delete(Partner);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", Partner, nameof(PartnerService));
                return Partner;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(PartnerService));
                throw new MessageException(ex);
            }
        }
    }
}
