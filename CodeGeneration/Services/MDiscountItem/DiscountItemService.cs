
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MDiscountItem
{
    public interface IDiscountItemService : IServiceScoped
    {
        Task<int> Count(DiscountItemFilter DiscountItemFilter);
        Task<List<DiscountItem>> List(DiscountItemFilter DiscountItemFilter);
        Task<DiscountItem> Get(long Id);
        Task<DiscountItem> Create(DiscountItem DiscountItem);
        Task<DiscountItem> Update(DiscountItem DiscountItem);
        Task<DiscountItem> Delete(DiscountItem DiscountItem);
    }

    public class DiscountItemService : IDiscountItemService
    {
        public IUOW UOW;
        public IDiscountItemValidator DiscountItemValidator;

        public DiscountItemService(
            IUOW UOW, 
            IDiscountItemValidator DiscountItemValidator
        )
        {
            this.UOW = UOW;
            this.DiscountItemValidator = DiscountItemValidator;
        }
        public async Task<int> Count(DiscountItemFilter DiscountItemFilter)
        {
            int result = await UOW.DiscountItemRepository.Count(DiscountItemFilter);
            return result;
        }

        public async Task<List<DiscountItem>> List(DiscountItemFilter DiscountItemFilter)
        {
            List<DiscountItem> DiscountItems = await UOW.DiscountItemRepository.List(DiscountItemFilter);
            return DiscountItems;
        }

        public async Task<DiscountItem> Get(long Id)
        {
            DiscountItem DiscountItem = await UOW.DiscountItemRepository.Get(Id);
            if (DiscountItem == null)
                return null;
            return DiscountItem;
        }

        public async Task<DiscountItem> Create(DiscountItem DiscountItem)
        {
            if (!await DiscountItemValidator.Create(DiscountItem))
                return DiscountItem;

            try
            {
               
                await UOW.Begin();
                await UOW.DiscountItemRepository.Create(DiscountItem);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(DiscountItem, "", nameof(DiscountItemService));
                return await UOW.DiscountItemRepository.Get(DiscountItem.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(DiscountItemService));
                throw new MessageException(ex);
            }
        }

        public async Task<DiscountItem> Update(DiscountItem DiscountItem)
        {
            if (!await DiscountItemValidator.Update(DiscountItem))
                return DiscountItem;
            try
            {
                var oldData = await UOW.DiscountItemRepository.Get(DiscountItem.Id);

                await UOW.Begin();
                await UOW.DiscountItemRepository.Update(DiscountItem);
                await UOW.Commit();

                var newData = await UOW.DiscountItemRepository.Get(DiscountItem.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(DiscountItemService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(DiscountItemService));
                throw new MessageException(ex);
            }
        }

        public async Task<DiscountItem> Delete(DiscountItem DiscountItem)
        {
            if (!await DiscountItemValidator.Delete(DiscountItem))
                return DiscountItem;

            try
            {
                await UOW.Begin();
                await UOW.DiscountItemRepository.Delete(DiscountItem);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", DiscountItem, nameof(DiscountItemService));
                return DiscountItem;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(DiscountItemService));
                throw new MessageException(ex);
            }
        }
    }
}
