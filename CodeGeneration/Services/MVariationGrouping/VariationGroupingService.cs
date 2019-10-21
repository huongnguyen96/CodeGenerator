
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MVariationGrouping
{
    public interface IVariationGroupingService : IServiceScoped
    {
        Task<int> Count(VariationGroupingFilter VariationGroupingFilter);
        Task<List<VariationGrouping>> List(VariationGroupingFilter VariationGroupingFilter);
        Task<VariationGrouping> Get(long Id);
        Task<VariationGrouping> Create(VariationGrouping VariationGrouping);
        Task<VariationGrouping> Update(VariationGrouping VariationGrouping);
        Task<VariationGrouping> Delete(VariationGrouping VariationGrouping);
    }

    public class VariationGroupingService : IVariationGroupingService
    {
        public IUOW UOW;
        public IVariationGroupingValidator VariationGroupingValidator;

        public VariationGroupingService(
            IUOW UOW, 
            IVariationGroupingValidator VariationGroupingValidator
        )
        {
            this.UOW = UOW;
            this.VariationGroupingValidator = VariationGroupingValidator;
        }
        public async Task<int> Count(VariationGroupingFilter VariationGroupingFilter)
        {
            int result = await UOW.VariationGroupingRepository.Count(VariationGroupingFilter);
            return result;
        }

        public async Task<List<VariationGrouping>> List(VariationGroupingFilter VariationGroupingFilter)
        {
            List<VariationGrouping> VariationGroupings = await UOW.VariationGroupingRepository.List(VariationGroupingFilter);
            return VariationGroupings;
        }

        public async Task<VariationGrouping> Get(long Id)
        {
            VariationGrouping VariationGrouping = await UOW.VariationGroupingRepository.Get(Id);
            if (VariationGrouping == null)
                return null;
            return VariationGrouping;
        }

        public async Task<VariationGrouping> Create(VariationGrouping VariationGrouping)
        {
            if (!await VariationGroupingValidator.Create(VariationGrouping))
                return VariationGrouping;

            try
            {
               
                await UOW.Begin();
                await UOW.VariationGroupingRepository.Create(VariationGrouping);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(VariationGrouping, "", nameof(VariationGroupingService));
                return await UOW.VariationGroupingRepository.Get(VariationGrouping.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(VariationGroupingService));
                throw new MessageException(ex);
            }
        }

        public async Task<VariationGrouping> Update(VariationGrouping VariationGrouping)
        {
            if (!await VariationGroupingValidator.Update(VariationGrouping))
                return VariationGrouping;
            try
            {
                var oldData = await UOW.VariationGroupingRepository.Get(VariationGrouping.Id);

                await UOW.Begin();
                await UOW.VariationGroupingRepository.Update(VariationGrouping);
                await UOW.Commit();

                var newData = await UOW.VariationGroupingRepository.Get(VariationGrouping.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(VariationGroupingService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(VariationGroupingService));
                throw new MessageException(ex);
            }
        }

        public async Task<VariationGrouping> Delete(VariationGrouping VariationGrouping)
        {
            if (!await VariationGroupingValidator.Delete(VariationGrouping))
                return VariationGrouping;

            try
            {
                await UOW.Begin();
                await UOW.VariationGroupingRepository.Delete(VariationGrouping);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", VariationGrouping, nameof(VariationGroupingService));
                return VariationGrouping;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(VariationGroupingService));
                throw new MessageException(ex);
            }
        }
    }
}
