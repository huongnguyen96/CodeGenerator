
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MItemStatus
{
    public interface IItemStatusValidator : IServiceScoped
    {
        Task<bool> Create(ItemStatus ItemStatus);
        Task<bool> Update(ItemStatus ItemStatus);
        Task<bool> Delete(ItemStatus ItemStatus);
    }

    public class ItemStatusValidator : IItemStatusValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public ItemStatusValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(ItemStatus ItemStatus)
        {
            ItemStatusFilter ItemStatusFilter = new ItemStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = ItemStatus.Id },
                Selects = ItemStatusSelect.Id
            };

            int count = await UOW.ItemStatusRepository.Count(ItemStatusFilter);

            if (count == 0)
                ItemStatus.AddError(nameof(ItemStatusValidator), nameof(ItemStatus.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(ItemStatus ItemStatus)
        {
            return ItemStatus.IsValidated;
        }

        public async Task<bool> Update(ItemStatus ItemStatus)
        {
            if (await ValidateId(ItemStatus))
            {
            }
            return ItemStatus.IsValidated;
        }

        public async Task<bool> Delete(ItemStatus ItemStatus)
        {
            if (await ValidateId(ItemStatus))
            {
            }
            return ItemStatus.IsValidated;
        }
    }
}
