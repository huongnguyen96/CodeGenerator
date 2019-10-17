
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MItemType
{
    public interface IItemTypeService : IServiceScoped
    {
        Task<int> Count(ItemTypeFilter ItemTypeFilter);
        Task<List<ItemType>> List(ItemTypeFilter ItemTypeFilter);
        Task<ItemType> Get(long Id);
        Task<ItemType> Create(ItemType ItemType);
        Task<ItemType> Update(ItemType ItemType);
        Task<ItemType> Delete(ItemType ItemType);
    }

    public class ItemTypeService : IItemTypeService
    {
        public IUOW UOW;
        public IItemTypeValidator ItemTypeValidator;

        public ItemTypeService(
            IUOW UOW, 
            IItemTypeValidator ItemTypeValidator
        )
        {
            this.UOW = UOW;
            this.ItemTypeValidator = ItemTypeValidator;
        }
        public async Task<int> Count(ItemTypeFilter ItemTypeFilter)
        {
            int result = await UOW.ItemTypeRepository.Count(ItemTypeFilter);
            return result;
        }

        public async Task<List<ItemType>> List(ItemTypeFilter ItemTypeFilter)
        {
            List<ItemType> ItemTypes = await UOW.ItemTypeRepository.List(ItemTypeFilter);
            return ItemTypes;
        }

        public async Task<ItemType> Get(long Id)
        {
            ItemType ItemType = await UOW.ItemTypeRepository.Get(Id);
            if (ItemType == null)
                return null;
            return ItemType;
        }

        public async Task<ItemType> Create(ItemType ItemType)
        {
            if (!await ItemTypeValidator.Create(ItemType))
                return ItemType;

            try
            {
               
                await UOW.Begin();
                await UOW.ItemTypeRepository.Create(ItemType);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(ItemType, "", nameof(ItemTypeService));
                return await UOW.ItemTypeRepository.Get(ItemType.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ItemTypeService));
                throw new MessageException(ex);
            }
        }

        public async Task<ItemType> Update(ItemType ItemType)
        {
            if (!await ItemTypeValidator.Update(ItemType))
                return ItemType;
            try
            {
                var oldData = await UOW.ItemTypeRepository.Get(ItemType.Id);

                await UOW.Begin();
                await UOW.ItemTypeRepository.Update(ItemType);
                await UOW.Commit();

                var newData = await UOW.ItemTypeRepository.Get(ItemType.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(ItemTypeService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ItemTypeService));
                throw new MessageException(ex);
            }
        }

        public async Task<ItemType> Delete(ItemType ItemType)
        {
            if (!await ItemTypeValidator.Delete(ItemType))
                return ItemType;

            try
            {
                await UOW.Begin();
                await UOW.ItemTypeRepository.Delete(ItemType);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", ItemType, nameof(ItemTypeService));
                return ItemType;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ItemTypeService));
                throw new MessageException(ex);
            }
        }
    }
}
