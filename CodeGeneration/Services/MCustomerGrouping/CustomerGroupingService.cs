
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MCustomerGrouping
{
    public interface ICustomerGroupingService : IServiceScoped
    {
        Task<int> Count(CustomerGroupingFilter CustomerGroupingFilter);
        Task<List<CustomerGrouping>> List(CustomerGroupingFilter CustomerGroupingFilter);
        Task<CustomerGrouping> Get(long Id);
        Task<CustomerGrouping> Create(CustomerGrouping CustomerGrouping);
        Task<CustomerGrouping> Update(CustomerGrouping CustomerGrouping);
        Task<CustomerGrouping> Delete(CustomerGrouping CustomerGrouping);
    }

    public class CustomerGroupingService : ICustomerGroupingService
    {
        public IUOW UOW;
        public ICustomerGroupingValidator CustomerGroupingValidator;

        public CustomerGroupingService(
            IUOW UOW, 
            ICustomerGroupingValidator CustomerGroupingValidator
        )
        {
            this.UOW = UOW;
            this.CustomerGroupingValidator = CustomerGroupingValidator;
        }
        public async Task<int> Count(CustomerGroupingFilter CustomerGroupingFilter)
        {
            int result = await UOW.CustomerGroupingRepository.Count(CustomerGroupingFilter);
            return result;
        }

        public async Task<List<CustomerGrouping>> List(CustomerGroupingFilter CustomerGroupingFilter)
        {
            List<CustomerGrouping> CustomerGroupings = await UOW.CustomerGroupingRepository.List(CustomerGroupingFilter);
            return CustomerGroupings;
        }

        public async Task<CustomerGrouping> Get(long Id)
        {
            CustomerGrouping CustomerGrouping = await UOW.CustomerGroupingRepository.Get(Id);
            if (CustomerGrouping == null)
                return null;
            return CustomerGrouping;
        }

        public async Task<CustomerGrouping> Create(CustomerGrouping CustomerGrouping)
        {
            if (!await CustomerGroupingValidator.Create(CustomerGrouping))
                return CustomerGrouping;

            try
            {
               
                await UOW.Begin();
                await UOW.CustomerGroupingRepository.Create(CustomerGrouping);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(CustomerGrouping, "", nameof(CustomerGroupingService));
                return await UOW.CustomerGroupingRepository.Get(CustomerGrouping.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(CustomerGroupingService));
                throw new MessageException(ex);
            }
        }

        public async Task<CustomerGrouping> Update(CustomerGrouping CustomerGrouping)
        {
            if (!await CustomerGroupingValidator.Update(CustomerGrouping))
                return CustomerGrouping;
            try
            {
                var oldData = await UOW.CustomerGroupingRepository.Get(CustomerGrouping.Id);

                await UOW.Begin();
                await UOW.CustomerGroupingRepository.Update(CustomerGrouping);
                await UOW.Commit();

                var newData = await UOW.CustomerGroupingRepository.Get(CustomerGrouping.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(CustomerGroupingService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(CustomerGroupingService));
                throw new MessageException(ex);
            }
        }

        public async Task<CustomerGrouping> Delete(CustomerGrouping CustomerGrouping)
        {
            if (!await CustomerGroupingValidator.Delete(CustomerGrouping))
                return CustomerGrouping;

            try
            {
                await UOW.Begin();
                await UOW.CustomerGroupingRepository.Delete(CustomerGrouping);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", CustomerGrouping, nameof(CustomerGroupingService));
                return CustomerGrouping;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(CustomerGroupingService));
                throw new MessageException(ex);
            }
        }
    }
}
