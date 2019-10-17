
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MItemStock
{
    public interface IItemStockService : IServiceScoped
    {
        Task<int> Count(ItemStockFilter ItemStockFilter);
        Task<List<ItemStock>> List(ItemStockFilter ItemStockFilter);
        Task<ItemStock> Get(long Id);
        Task<ItemStock> Create(ItemStock ItemStock);
        Task<ItemStock> Update(ItemStock ItemStock);
        Task<ItemStock> Delete(ItemStock ItemStock);
    }

    public class ItemStockService : IItemStockService
    {
        public IUOW UOW;
        public IItemStockValidator ItemStockValidator;

        public ItemStockService(
            IUOW UOW, 
            IItemStockValidator ItemStockValidator
        )
        {
            this.UOW = UOW;
            this.ItemStockValidator = ItemStockValidator;
        }
        public async Task<int> Count(ItemStockFilter ItemStockFilter)
        {
            int result = await UOW.ItemStockRepository.Count(ItemStockFilter);
            return result;
        }

        public async Task<List<ItemStock>> List(ItemStockFilter ItemStockFilter)
        {
            List<ItemStock> ItemStocks = await UOW.ItemStockRepository.List(ItemStockFilter);
            return ItemStocks;
        }

        public async Task<ItemStock> Get(long Id)
        {
            ItemStock ItemStock = await UOW.ItemStockRepository.Get(Id);
            if (ItemStock == null)
                return null;
            return ItemStock;
        }

        public async Task<ItemStock> Create(ItemStock ItemStock)
        {
            if (!await ItemStockValidator.Create(ItemStock))
                return ItemStock;

            try
            {
               
                await UOW.Begin();
                await UOW.ItemStockRepository.Create(ItemStock);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(ItemStock, "", nameof(ItemStockService));
                return await UOW.ItemStockRepository.Get(ItemStock.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ItemStockService));
                throw new MessageException(ex);
            }
        }

        public async Task<ItemStock> Update(ItemStock ItemStock)
        {
            if (!await ItemStockValidator.Update(ItemStock))
                return ItemStock;
            try
            {
                var oldData = await UOW.ItemStockRepository.Get(ItemStock.Id);

                await UOW.Begin();
                await UOW.ItemStockRepository.Update(ItemStock);
                await UOW.Commit();

                var newData = await UOW.ItemStockRepository.Get(ItemStock.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(ItemStockService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ItemStockService));
                throw new MessageException(ex);
            }
        }

        public async Task<ItemStock> Delete(ItemStock ItemStock)
        {
            if (!await ItemStockValidator.Delete(ItemStock))
                return ItemStock;

            try
            {
                await UOW.Begin();
                await UOW.ItemStockRepository.Delete(ItemStock);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", ItemStock, nameof(ItemStockService));
                return ItemStock;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ItemStockService));
                throw new MessageException(ex);
            }
        }
    }
}
