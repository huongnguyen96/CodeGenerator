
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MEVoucher
{
    public interface IEVoucherService : IServiceScoped
    {
        Task<int> Count(EVoucherFilter EVoucherFilter);
        Task<List<EVoucher>> List(EVoucherFilter EVoucherFilter);
        Task<EVoucher> Get(long Id);
        Task<EVoucher> Create(EVoucher EVoucher);
        Task<EVoucher> Update(EVoucher EVoucher);
        Task<EVoucher> Delete(EVoucher EVoucher);
    }

    public class EVoucherService : IEVoucherService
    {
        public IUOW UOW;
        public IEVoucherValidator EVoucherValidator;

        public EVoucherService(
            IUOW UOW, 
            IEVoucherValidator EVoucherValidator
        )
        {
            this.UOW = UOW;
            this.EVoucherValidator = EVoucherValidator;
        }
        public async Task<int> Count(EVoucherFilter EVoucherFilter)
        {
            int result = await UOW.EVoucherRepository.Count(EVoucherFilter);
            return result;
        }

        public async Task<List<EVoucher>> List(EVoucherFilter EVoucherFilter)
        {
            List<EVoucher> EVouchers = await UOW.EVoucherRepository.List(EVoucherFilter);
            return EVouchers;
        }

        public async Task<EVoucher> Get(long Id)
        {
            EVoucher EVoucher = await UOW.EVoucherRepository.Get(Id);
            if (EVoucher == null)
                return null;
            return EVoucher;
        }

        public async Task<EVoucher> Create(EVoucher EVoucher)
        {
            if (!await EVoucherValidator.Create(EVoucher))
                return EVoucher;

            try
            {
               
                await UOW.Begin();
                await UOW.EVoucherRepository.Create(EVoucher);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(EVoucher, "", nameof(EVoucherService));
                return await UOW.EVoucherRepository.Get(EVoucher.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(EVoucherService));
                throw new MessageException(ex);
            }
        }

        public async Task<EVoucher> Update(EVoucher EVoucher)
        {
            if (!await EVoucherValidator.Update(EVoucher))
                return EVoucher;
            try
            {
                var oldData = await UOW.EVoucherRepository.Get(EVoucher.Id);

                await UOW.Begin();
                await UOW.EVoucherRepository.Update(EVoucher);
                await UOW.Commit();

                var newData = await UOW.EVoucherRepository.Get(EVoucher.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(EVoucherService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(EVoucherService));
                throw new MessageException(ex);
            }
        }

        public async Task<EVoucher> Delete(EVoucher EVoucher)
        {
            if (!await EVoucherValidator.Delete(EVoucher))
                return EVoucher;

            try
            {
                await UOW.Begin();
                await UOW.EVoucherRepository.Delete(EVoucher);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", EVoucher, nameof(EVoucherService));
                return EVoucher;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(EVoucherService));
                throw new MessageException(ex);
            }
        }
    }
}
