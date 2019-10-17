
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MSupplier
{
    public interface ISupplierValidator : IServiceScoped
    {
        Task<bool> Create(Supplier Supplier);
        Task<bool> Update(Supplier Supplier);
        Task<bool> Delete(Supplier Supplier);
    }

    public class SupplierValidator : ISupplierValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public SupplierValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Supplier Supplier)
        {
            SupplierFilter SupplierFilter = new SupplierFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = Supplier.Id },
                Selects = SupplierSelect.Id
            };

            int count = await UOW.SupplierRepository.Count(SupplierFilter);

            if (count == 0)
                Supplier.AddError(nameof(SupplierValidator), nameof(Supplier.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(Supplier Supplier)
        {
            return Supplier.IsValidated;
        }

        public async Task<bool> Update(Supplier Supplier)
        {
            if (await ValidateId(Supplier))
            {
            }
            return Supplier.IsValidated;
        }

        public async Task<bool> Delete(Supplier Supplier)
        {
            if (await ValidateId(Supplier))
            {
            }
            return Supplier.IsValidated;
        }
    }
}
