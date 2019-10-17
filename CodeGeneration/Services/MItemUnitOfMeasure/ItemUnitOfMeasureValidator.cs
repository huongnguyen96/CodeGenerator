
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MItemUnitOfMeasure
{
    public interface IItemUnitOfMeasureValidator : IServiceScoped
    {
        Task<bool> Create(ItemUnitOfMeasure ItemUnitOfMeasure);
        Task<bool> Update(ItemUnitOfMeasure ItemUnitOfMeasure);
        Task<bool> Delete(ItemUnitOfMeasure ItemUnitOfMeasure);
    }

    public class ItemUnitOfMeasureValidator : IItemUnitOfMeasureValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public ItemUnitOfMeasureValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(ItemUnitOfMeasure ItemUnitOfMeasure)
        {
            ItemUnitOfMeasureFilter ItemUnitOfMeasureFilter = new ItemUnitOfMeasureFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = ItemUnitOfMeasure.Id },
                Selects = ItemUnitOfMeasureSelect.Id
            };

            int count = await UOW.ItemUnitOfMeasureRepository.Count(ItemUnitOfMeasureFilter);

            if (count == 0)
                ItemUnitOfMeasure.AddError(nameof(ItemUnitOfMeasureValidator), nameof(ItemUnitOfMeasure.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(ItemUnitOfMeasure ItemUnitOfMeasure)
        {
            return ItemUnitOfMeasure.IsValidated;
        }

        public async Task<bool> Update(ItemUnitOfMeasure ItemUnitOfMeasure)
        {
            if (await ValidateId(ItemUnitOfMeasure))
            {
            }
            return ItemUnitOfMeasure.IsValidated;
        }

        public async Task<bool> Delete(ItemUnitOfMeasure ItemUnitOfMeasure)
        {
            if (await ValidateId(ItemUnitOfMeasure))
            {
            }
            return ItemUnitOfMeasure.IsValidated;
        }
    }
}
