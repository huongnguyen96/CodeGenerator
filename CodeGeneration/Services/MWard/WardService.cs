
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MWard
{
    public interface IWardService : IServiceScoped
    {
        Task<int> Count(WardFilter WardFilter);
        Task<List<Ward>> List(WardFilter WardFilter);
        Task<Ward> Get(long Id);
        Task<Ward> Create(Ward Ward);
        Task<Ward> Update(Ward Ward);
        Task<Ward> Delete(Ward Ward);
    }

    public class WardService : IWardService
    {
        public IUOW UOW;
        public IWardValidator WardValidator;

        public WardService(
            IUOW UOW, 
            IWardValidator WardValidator
        )
        {
            this.UOW = UOW;
            this.WardValidator = WardValidator;
        }
        public async Task<int> Count(WardFilter WardFilter)
        {
            int result = await UOW.WardRepository.Count(WardFilter);
            return result;
        }

        public async Task<List<Ward>> List(WardFilter WardFilter)
        {
            List<Ward> Wards = await UOW.WardRepository.List(WardFilter);
            return Wards;
        }

        public async Task<Ward> Get(long Id)
        {
            Ward Ward = await UOW.WardRepository.Get(Id);
            if (Ward == null)
                return null;
            return Ward;
        }

        public async Task<Ward> Create(Ward Ward)
        {
            if (!await WardValidator.Create(Ward))
                return Ward;

            try
            {
               
                await UOW.Begin();
                await UOW.WardRepository.Create(Ward);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(Ward, "", nameof(WardService));
                return await UOW.WardRepository.Get(Ward.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(WardService));
                throw new MessageException(ex);
            }
        }

        public async Task<Ward> Update(Ward Ward)
        {
            if (!await WardValidator.Update(Ward))
                return Ward;
            try
            {
                var oldData = await UOW.WardRepository.Get(Ward.Id);

                await UOW.Begin();
                await UOW.WardRepository.Update(Ward);
                await UOW.Commit();

                var newData = await UOW.WardRepository.Get(Ward.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(WardService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(WardService));
                throw new MessageException(ex);
            }
        }

        public async Task<Ward> Delete(Ward Ward)
        {
            if (!await WardValidator.Delete(Ward))
                return Ward;

            try
            {
                await UOW.Begin();
                await UOW.WardRepository.Delete(Ward);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", Ward, nameof(WardService));
                return Ward;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(WardService));
                throw new MessageException(ex);
            }
        }
    }
}
