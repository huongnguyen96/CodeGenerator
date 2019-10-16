
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MItem
{
    public interface IItemValidator : IServiceScoped
    {
        Task<bool> Create(Item Item);
        Task<bool> Update(Item Item);
        Task<bool> Delete(Item Item);
    }

    public class ItemValidator : IItemValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public ItemValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Item Item)
        {
            ItemFilter ItemFilter = new ItemFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = Item.Id },
                Selects = ItemSelect.Id
            };

            int count = await UOW.ItemRepository.Count(ItemFilter);

            if (count == 0)
                Item.AddError(nameof(ItemValidator), nameof(Item.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(Item Item)
        {
            return Item.IsValidated;
        }

        public async Task<bool> Update(Item Item)
        {
            if (await ValidateId(Item))
            {
            }
            return Item.IsValidated;
        }

        public async Task<bool> Delete(Item Item)
        {
            if (await ValidateId(Item))
            {
            }
            return Item.IsValidated;
        }
    }
}
