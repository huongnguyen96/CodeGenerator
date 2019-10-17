
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MItemType
{
    public interface IItemTypeValidator : IServiceScoped
    {
        Task<bool> Create(ItemType ItemType);
        Task<bool> Update(ItemType ItemType);
        Task<bool> Delete(ItemType ItemType);
    }

    public class ItemTypeValidator : IItemTypeValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public ItemTypeValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(ItemType ItemType)
        {
            ItemTypeFilter ItemTypeFilter = new ItemTypeFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = ItemType.Id },
                Selects = ItemTypeSelect.Id
            };

            int count = await UOW.ItemTypeRepository.Count(ItemTypeFilter);

            if (count == 0)
                ItemType.AddError(nameof(ItemTypeValidator), nameof(ItemType.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(ItemType ItemType)
        {
            return ItemType.IsValidated;
        }

        public async Task<bool> Update(ItemType ItemType)
        {
            if (await ValidateId(ItemType))
            {
            }
            return ItemType.IsValidated;
        }

        public async Task<bool> Delete(ItemType ItemType)
        {
            if (await ValidateId(ItemType))
            {
            }
            return ItemType.IsValidated;
        }
    }
}
