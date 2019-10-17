
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MItemUnitOfMeasure
{
    public interface IItemUnitOfMeasureService : IServiceScoped
    {
        Task<int> Count(ItemUnitOfMeasureFilter ItemUnitOfMeasureFilter);
        Task<List<ItemUnitOfMeasure>> List(ItemUnitOfMeasureFilter ItemUnitOfMeasureFilter);
        Task<ItemUnitOfMeasure> Get(long Id);
        Task<ItemUnitOfMeasure> Create(ItemUnitOfMeasure ItemUnitOfMeasure);
        Task<ItemUnitOfMeasure> Update(ItemUnitOfMeasure ItemUnitOfMeasure);
        Task<ItemUnitOfMeasure> Delete(ItemUnitOfMeasure ItemUnitOfMeasure);
    }

    public class ItemUnitOfMeasureService : IItemUnitOfMeasureService
    {
        public IUOW UOW;
        public IItemUnitOfMeasureValidator ItemUnitOfMeasureValidator;

        public ItemUnitOfMeasureService(
            IUOW UOW, 
            IItemUnitOfMeasureValidator ItemUnitOfMeasureValidator
        )
        {
            this.UOW = UOW;
            this.ItemUnitOfMeasureValidator = ItemUnitOfMeasureValidator;
        }
        public async Task<int> Count(ItemUnitOfMeasureFilter ItemUnitOfMeasureFilter)
        {
            int result = await UOW.ItemUnitOfMeasureRepository.Count(ItemUnitOfMeasureFilter);
            return result;
        }

        public async Task<List<ItemUnitOfMeasure>> List(ItemUnitOfMeasureFilter ItemUnitOfMeasureFilter)
        {
            List<ItemUnitOfMeasure> ItemUnitOfMeasures = await UOW.ItemUnitOfMeasureRepository.List(ItemUnitOfMeasureFilter);
            return ItemUnitOfMeasures;
        }

        public async Task<ItemUnitOfMeasure> Get(long Id)
        {
            ItemUnitOfMeasure ItemUnitOfMeasure = await UOW.ItemUnitOfMeasureRepository.Get(Id);
            if (ItemUnitOfMeasure == null)
                return null;
            return ItemUnitOfMeasure;
        }

        public async Task<ItemUnitOfMeasure> Create(ItemUnitOfMeasure ItemUnitOfMeasure)
        {
            if (!await ItemUnitOfMeasureValidator.Create(ItemUnitOfMeasure))
                return ItemUnitOfMeasure;

            try
            {
               
                await UOW.Begin();
                await UOW.ItemUnitOfMeasureRepository.Create(ItemUnitOfMeasure);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(ItemUnitOfMeasure, "", nameof(ItemUnitOfMeasureService));
                return await UOW.ItemUnitOfMeasureRepository.Get(ItemUnitOfMeasure.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ItemUnitOfMeasureService));
                throw new MessageException(ex);
            }
        }

        public async Task<ItemUnitOfMeasure> Update(ItemUnitOfMeasure ItemUnitOfMeasure)
        {
            if (!await ItemUnitOfMeasureValidator.Update(ItemUnitOfMeasure))
                return ItemUnitOfMeasure;
            try
            {
                var oldData = await UOW.ItemUnitOfMeasureRepository.Get(ItemUnitOfMeasure.Id);

                await UOW.Begin();
                await UOW.ItemUnitOfMeasureRepository.Update(ItemUnitOfMeasure);
                await UOW.Commit();

                var newData = await UOW.ItemUnitOfMeasureRepository.Get(ItemUnitOfMeasure.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(ItemUnitOfMeasureService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ItemUnitOfMeasureService));
                throw new MessageException(ex);
            }
        }

        public async Task<ItemUnitOfMeasure> Delete(ItemUnitOfMeasure ItemUnitOfMeasure)
        {
            if (!await ItemUnitOfMeasureValidator.Delete(ItemUnitOfMeasure))
                return ItemUnitOfMeasure;

            try
            {
                await UOW.Begin();
                await UOW.ItemUnitOfMeasureRepository.Delete(ItemUnitOfMeasure);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", ItemUnitOfMeasure, nameof(ItemUnitOfMeasureService));
                return ItemUnitOfMeasure;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ItemUnitOfMeasureService));
                throw new MessageException(ex);
            }
        }
    }
}
