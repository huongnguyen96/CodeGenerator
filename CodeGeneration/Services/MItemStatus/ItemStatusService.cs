
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MItemStatus
{
    public interface IItemStatusService : IServiceScoped
    {
        Task<int> Count(ItemStatusFilter ItemStatusFilter);
        Task<List<ItemStatus>> List(ItemStatusFilter ItemStatusFilter);
        Task<ItemStatus> Get(long Id);
        Task<ItemStatus> Create(ItemStatus ItemStatus);
        Task<ItemStatus> Update(ItemStatus ItemStatus);
        Task<ItemStatus> Delete(ItemStatus ItemStatus);
    }

    public class ItemStatusService : IItemStatusService
    {
        public IUOW UOW;
        public IItemStatusValidator ItemStatusValidator;

        public ItemStatusService(
            IUOW UOW, 
            IItemStatusValidator ItemStatusValidator
        )
        {
            this.UOW = UOW;
            this.ItemStatusValidator = ItemStatusValidator;
        }
        public async Task<int> Count(ItemStatusFilter ItemStatusFilter)
        {
            int result = await UOW.ItemStatusRepository.Count(ItemStatusFilter);
            return result;
        }

        public async Task<List<ItemStatus>> List(ItemStatusFilter ItemStatusFilter)
        {
            List<ItemStatus> ItemStatuss = await UOW.ItemStatusRepository.List(ItemStatusFilter);
            return ItemStatuss;
        }

        public async Task<ItemStatus> Get(long Id)
        {
            ItemStatus ItemStatus = await UOW.ItemStatusRepository.Get(Id);
            if (ItemStatus == null)
                return null;
            return ItemStatus;
        }

        public async Task<ItemStatus> Create(ItemStatus ItemStatus)
        {
            if (!await ItemStatusValidator.Create(ItemStatus))
                return ItemStatus;

            try
            {
               
                await UOW.Begin();
                await UOW.ItemStatusRepository.Create(ItemStatus);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(ItemStatus, "", nameof(ItemStatusService));
                return await UOW.ItemStatusRepository.Get(ItemStatus.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ItemStatusService));
                throw new MessageException(ex);
            }
        }

        public async Task<ItemStatus> Update(ItemStatus ItemStatus)
        {
            if (!await ItemStatusValidator.Update(ItemStatus))
                return ItemStatus;
            try
            {
                var oldData = await UOW.ItemStatusRepository.Get(ItemStatus.Id);

                await UOW.Begin();
                await UOW.ItemStatusRepository.Update(ItemStatus);
                await UOW.Commit();

                var newData = await UOW.ItemStatusRepository.Get(ItemStatus.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(ItemStatusService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ItemStatusService));
                throw new MessageException(ex);
            }
        }

        public async Task<ItemStatus> Delete(ItemStatus ItemStatus)
        {
            if (!await ItemStatusValidator.Delete(ItemStatus))
                return ItemStatus;

            try
            {
                await UOW.Begin();
                await UOW.ItemStatusRepository.Delete(ItemStatus);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", ItemStatus, nameof(ItemStatusService));
                return ItemStatus;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ItemStatusService));
                throw new MessageException(ex);
            }
        }
    }
}
