
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MProductStatus
{
    public interface IProductStatusValidator : IServiceScoped
    {
        Task<bool> Create(ProductStatus ProductStatus);
        Task<bool> Update(ProductStatus ProductStatus);
        Task<bool> Delete(ProductStatus ProductStatus);
    }

    public class ProductStatusValidator : IProductStatusValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public ProductStatusValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(ProductStatus ProductStatus)
        {
            ProductStatusFilter ProductStatusFilter = new ProductStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = ProductStatus.Id },
                Selects = ProductStatusSelect.Id
            };

            int count = await UOW.ProductStatusRepository.Count(ProductStatusFilter);

            if (count == 0)
                ProductStatus.AddError(nameof(ProductStatusValidator), nameof(ProductStatus.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(ProductStatus ProductStatus)
        {
            return ProductStatus.IsValidated;
        }

        public async Task<bool> Update(ProductStatus ProductStatus)
        {
            if (await ValidateId(ProductStatus))
            {
            }
            return ProductStatus.IsValidated;
        }

        public async Task<bool> Delete(ProductStatus ProductStatus)
        {
            if (await ValidateId(ProductStatus))
            {
            }
            return ProductStatus.IsValidated;
        }
    }
}
