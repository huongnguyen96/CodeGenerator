
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MSupplier
{
    public interface ISupplierService : IServiceScoped
    {
        Task<int> Count(SupplierFilter SupplierFilter);
        Task<List<Supplier>> List(SupplierFilter SupplierFilter);
        Task<Supplier> Get(long Id);
        Task<Supplier> Create(Supplier Supplier);
        Task<Supplier> Update(Supplier Supplier);
        Task<Supplier> Delete(Supplier Supplier);
    }

    public class SupplierService : ISupplierService
    {
        public IUOW UOW;
        public ISupplierValidator SupplierValidator;

        public SupplierService(
            IUOW UOW, 
            ISupplierValidator SupplierValidator
        )
        {
            this.UOW = UOW;
            this.SupplierValidator = SupplierValidator;
        }
        public async Task<int> Count(SupplierFilter SupplierFilter)
        {
            int result = await UOW.SupplierRepository.Count(SupplierFilter);
            return result;
        }

        public async Task<List<Supplier>> List(SupplierFilter SupplierFilter)
        {
            List<Supplier> Suppliers = await UOW.SupplierRepository.List(SupplierFilter);
            return Suppliers;
        }

        public async Task<Supplier> Get(long Id)
        {
            Supplier Supplier = await UOW.SupplierRepository.Get(Id);
            if (Supplier == null)
                return null;
            return Supplier;
        }

        public async Task<Supplier> Create(Supplier Supplier)
        {
            if (!await SupplierValidator.Create(Supplier))
                return Supplier;

            try
            {
               
                await UOW.Begin();
                await UOW.SupplierRepository.Create(Supplier);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(Supplier, "", nameof(SupplierService));
                return await UOW.SupplierRepository.Get(Supplier.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(SupplierService));
                throw new MessageException(ex);
            }
        }

        public async Task<Supplier> Update(Supplier Supplier)
        {
            if (!await SupplierValidator.Update(Supplier))
                return Supplier;
            try
            {
                var oldData = await UOW.SupplierRepository.Get(Supplier.Id);

                await UOW.Begin();
                await UOW.SupplierRepository.Update(Supplier);
                await UOW.Commit();

                var newData = await UOW.SupplierRepository.Get(Supplier.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(SupplierService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(SupplierService));
                throw new MessageException(ex);
            }
        }

        public async Task<Supplier> Delete(Supplier Supplier)
        {
            if (!await SupplierValidator.Delete(Supplier))
                return Supplier;

            try
            {
                await UOW.Begin();
                await UOW.SupplierRepository.Delete(Supplier);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", Supplier, nameof(SupplierService));
                return Supplier;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(SupplierService));
                throw new MessageException(ex);
            }
        }
    }
}
