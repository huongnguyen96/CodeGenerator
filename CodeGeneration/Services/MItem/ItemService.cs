
using Common;
using WeGift.Entities;
using WeGift.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WeGift.Services.MItem
{
    public interface IItemService : IServiceScoped
    {
        Task<int> Count(ItemFilter ItemFilter);
        Task<List<Item>> List(ItemFilter ItemFilter);
        Task<Item> Get(long Id);
        Task<Item> Create(Item Item);
        Task<Item> Update(Item Item);
        Task<Item> Delete(Item Item);
    }

    public class ItemService : IItemService
    {
        public IUOW UOW;
        public IItemValidator ItemValidator;

        public ItemService(
            IUOW UOW, 
            IItemValidator ItemValidator
        )
        {
            this.UOW = UOW;
            this.ItemValidator = ItemValidator;
        }
        public async Task<int> Count(ItemFilter ItemFilter)
        {
            int result = await UOW.ItemRepository.Count(ItemFilter);
            return result;
        }

        public async Task<List<Item>> List(ItemFilter ItemFilter)
        {
            List<Item> Items = await UOW.ItemRepository.List(ItemFilter);
            return Items;
        }

        public async Task<Item> Get(long Id)
        {
            Item Item = await UOW.ItemRepository.Get(Id);
            if (Item == null)
                return null;
            return Item;
        }

        public async Task<Item> Create(Item Item)
        {
            if (!await ItemValidator.Create(Item))
                return Item;

            try
            {
               
                await UOW.Begin();
                await UOW.ItemRepository.Create(Item);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(Item, "", nameof(ItemService));
                return await UOW.ItemRepository.Get(Item.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ItemService));
                throw new MessageException(ex);
            }
        }

        public async Task<Item> Update(Item Item)
        {
            if (!await ItemValidator.Update(Item))
                return Item;
            try
            {
                var oldData = await UOW.ItemRepository.Get(Item.Id);

                await UOW.Begin();
                await UOW.ItemRepository.Update(Item);
                await UOW.Commit();

                var newData = await UOW.ItemRepository.Get(Item.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(ItemService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ItemService));
                throw new MessageException(ex);
            }
        }

        public async Task<Item> Delete(Item Item)
        {
            if (!await ItemValidator.Delete(Item))
                return Item;

            try
            {
                await UOW.Begin();
                await UOW.ItemRepository.Delete(Item);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", Item, nameof(ItemService));
                return Item;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ItemService));
                throw new MessageException(ex);
            }
        }
    }
}
