
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MWarehouse
{
    public interface IWarehouseService : IServiceScoped
    {
        Task<int> Count(WarehouseFilter WarehouseFilter);
        Task<List<Warehouse>> List(WarehouseFilter WarehouseFilter);
        Task<Warehouse> Get(long Id);
        Task<Warehouse> Create(Warehouse Warehouse);
        Task<Warehouse> Update(Warehouse Warehouse);
        Task<Warehouse> Delete(Warehouse Warehouse);
    }

    public class WarehouseService : IWarehouseService
    {
        public IUOW UOW;
        public IWarehouseValidator WarehouseValidator;

        public WarehouseService(
            IUOW UOW, 
            IWarehouseValidator WarehouseValidator
        )
        {
            this.UOW = UOW;
            this.WarehouseValidator = WarehouseValidator;
        }
        public async Task<int> Count(WarehouseFilter WarehouseFilter)
        {
            int result = await UOW.WarehouseRepository.Count(WarehouseFilter);
            return result;
        }

        public async Task<List<Warehouse>> List(WarehouseFilter WarehouseFilter)
        {
            List<Warehouse> Warehouses = await UOW.WarehouseRepository.List(WarehouseFilter);
            return Warehouses;
        }

        public async Task<Warehouse> Get(long Id)
        {
            Warehouse Warehouse = await UOW.WarehouseRepository.Get(Id);
            if (Warehouse == null)
                return null;
            return Warehouse;
        }

        public async Task<Warehouse> Create(Warehouse Warehouse)
        {
            if (!await WarehouseValidator.Create(Warehouse))
                return Warehouse;

            try
            {
               
                await UOW.Begin();
                await UOW.WarehouseRepository.Create(Warehouse);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(Warehouse, "", nameof(WarehouseService));
                return await UOW.WarehouseRepository.Get(Warehouse.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(WarehouseService));
                throw new MessageException(ex);
            }
        }

        public async Task<Warehouse> Update(Warehouse Warehouse)
        {
            if (!await WarehouseValidator.Update(Warehouse))
                return Warehouse;
            try
            {
                var oldData = await UOW.WarehouseRepository.Get(Warehouse.Id);

                await UOW.Begin();
                await UOW.WarehouseRepository.Update(Warehouse);
                await UOW.Commit();

                var newData = await UOW.WarehouseRepository.Get(Warehouse.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(WarehouseService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(WarehouseService));
                throw new MessageException(ex);
            }
        }

        public async Task<Warehouse> Delete(Warehouse Warehouse)
        {
            if (!await WarehouseValidator.Delete(Warehouse))
                return Warehouse;

            try
            {
                await UOW.Begin();
                await UOW.WarehouseRepository.Delete(Warehouse);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", Warehouse, nameof(WarehouseService));
                return Warehouse;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(WarehouseService));
                throw new MessageException(ex);
            }
        }
    }
}
