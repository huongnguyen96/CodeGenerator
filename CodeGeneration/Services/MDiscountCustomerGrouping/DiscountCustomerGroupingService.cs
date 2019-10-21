
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MDiscountCustomerGrouping
{
    public interface IDiscountCustomerGroupingService : IServiceScoped
    {
        Task<int> Count(DiscountCustomerGroupingFilter DiscountCustomerGroupingFilter);
        Task<List<DiscountCustomerGrouping>> List(DiscountCustomerGroupingFilter DiscountCustomerGroupingFilter);
        Task<DiscountCustomerGrouping> Get(long Id);
        Task<DiscountCustomerGrouping> Create(DiscountCustomerGrouping DiscountCustomerGrouping);
        Task<DiscountCustomerGrouping> Update(DiscountCustomerGrouping DiscountCustomerGrouping);
        Task<DiscountCustomerGrouping> Delete(DiscountCustomerGrouping DiscountCustomerGrouping);
    }

    public class DiscountCustomerGroupingService : IDiscountCustomerGroupingService
    {
        public IUOW UOW;
        public IDiscountCustomerGroupingValidator DiscountCustomerGroupingValidator;

        public DiscountCustomerGroupingService(
            IUOW UOW, 
            IDiscountCustomerGroupingValidator DiscountCustomerGroupingValidator
        )
        {
            this.UOW = UOW;
            this.DiscountCustomerGroupingValidator = DiscountCustomerGroupingValidator;
        }
        public async Task<int> Count(DiscountCustomerGroupingFilter DiscountCustomerGroupingFilter)
        {
            int result = await UOW.DiscountCustomerGroupingRepository.Count(DiscountCustomerGroupingFilter);
            return result;
        }

        public async Task<List<DiscountCustomerGrouping>> List(DiscountCustomerGroupingFilter DiscountCustomerGroupingFilter)
        {
            List<DiscountCustomerGrouping> DiscountCustomerGroupings = await UOW.DiscountCustomerGroupingRepository.List(DiscountCustomerGroupingFilter);
            return DiscountCustomerGroupings;
        }

        public async Task<DiscountCustomerGrouping> Get(long Id)
        {
            DiscountCustomerGrouping DiscountCustomerGrouping = await UOW.DiscountCustomerGroupingRepository.Get(Id);
            if (DiscountCustomerGrouping == null)
                return null;
            return DiscountCustomerGrouping;
        }

        public async Task<DiscountCustomerGrouping> Create(DiscountCustomerGrouping DiscountCustomerGrouping)
        {
            if (!await DiscountCustomerGroupingValidator.Create(DiscountCustomerGrouping))
                return DiscountCustomerGrouping;

            try
            {
               
                await UOW.Begin();
                await UOW.DiscountCustomerGroupingRepository.Create(DiscountCustomerGrouping);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(DiscountCustomerGrouping, "", nameof(DiscountCustomerGroupingService));
                return await UOW.DiscountCustomerGroupingRepository.Get(DiscountCustomerGrouping.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(DiscountCustomerGroupingService));
                throw new MessageException(ex);
            }
        }

        public async Task<DiscountCustomerGrouping> Update(DiscountCustomerGrouping DiscountCustomerGrouping)
        {
            if (!await DiscountCustomerGroupingValidator.Update(DiscountCustomerGrouping))
                return DiscountCustomerGrouping;
            try
            {
                var oldData = await UOW.DiscountCustomerGroupingRepository.Get(DiscountCustomerGrouping.Id);

                await UOW.Begin();
                await UOW.DiscountCustomerGroupingRepository.Update(DiscountCustomerGrouping);
                await UOW.Commit();

                var newData = await UOW.DiscountCustomerGroupingRepository.Get(DiscountCustomerGrouping.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(DiscountCustomerGroupingService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(DiscountCustomerGroupingService));
                throw new MessageException(ex);
            }
        }

        public async Task<DiscountCustomerGrouping> Delete(DiscountCustomerGrouping DiscountCustomerGrouping)
        {
            if (!await DiscountCustomerGroupingValidator.Delete(DiscountCustomerGrouping))
                return DiscountCustomerGrouping;

            try
            {
                await UOW.Begin();
                await UOW.DiscountCustomerGroupingRepository.Delete(DiscountCustomerGrouping);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", DiscountCustomerGrouping, nameof(DiscountCustomerGroupingService));
                return DiscountCustomerGrouping;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(DiscountCustomerGroupingService));
                throw new MessageException(ex);
            }
        }
    }
}
