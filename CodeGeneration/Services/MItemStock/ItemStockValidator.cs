
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MItemStock
{
    public interface IItemStockValidator : IServiceScoped
    {
        Task<bool> Create(ItemStock ItemStock);
        Task<bool> Update(ItemStock ItemStock);
        Task<bool> Delete(ItemStock ItemStock);
    }

    public class ItemStockValidator : IItemStockValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public ItemStockValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(ItemStock ItemStock)
        {
            ItemStockFilter ItemStockFilter = new ItemStockFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = ItemStock.Id },
                Selects = ItemStockSelect.Id
            };

            int count = await UOW.ItemStockRepository.Count(ItemStockFilter);

            if (count == 0)
                ItemStock.AddError(nameof(ItemStockValidator), nameof(ItemStock.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(ItemStock ItemStock)
        {
            return ItemStock.IsValidated;
        }

        public async Task<bool> Update(ItemStock ItemStock)
        {
            if (await ValidateId(ItemStock))
            {
            }
            return ItemStock.IsValidated;
        }

        public async Task<bool> Delete(ItemStock ItemStock)
        {
            if (await ValidateId(ItemStock))
            {
            }
            return ItemStock.IsValidated;
        }
    }
}
