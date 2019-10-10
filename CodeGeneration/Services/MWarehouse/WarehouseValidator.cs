
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WeGift.Entities;
using WeGift.Repositories;

namespace WeGift.Services.MWarehouse
{
    public interface IWarehouseValidator : IServiceScoped
    {
        Task<bool> Create(Warehouse Warehouse);
        Task<bool> Update(Warehouse Warehouse);
        Task<bool> Delete(Warehouse Warehouse);
    }

    public class WarehouseValidator : IWarehouseValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public WarehouseValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Warehouse Warehouse)
        {
            WarehouseFilter WarehouseFilter = new WarehouseFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = Warehouse.Id },
                Selects = WarehouseSelect.Id
            };

            int count = await UOW.WarehouseRepository.Count(WarehouseFilter);

            if (count == 0)
                Warehouse.AddError(nameof(WarehouseValidator), nameof(Warehouse.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(Warehouse Warehouse)
        {
            return Warehouse.IsValidated;
        }

        public async Task<bool> Update(Warehouse Warehouse)
        {
            if (await ValidateId(Warehouse))
            {
            }
            return Warehouse.IsValidated;
        }

        public async Task<bool> Delete(Warehouse Warehouse)
        {
            if (await ValidateId(Warehouse))
            {
            }
            return Warehouse.IsValidated;
        }
    }
}
