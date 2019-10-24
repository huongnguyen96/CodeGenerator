
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MProductType
{
    public interface IProductTypeValidator : IServiceScoped
    {
        Task<bool> Create(ProductType ProductType);
        Task<bool> Update(ProductType ProductType);
        Task<bool> Delete(ProductType ProductType);
    }

    public class ProductTypeValidator : IProductTypeValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public ProductTypeValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(ProductType ProductType)
        {
            ProductTypeFilter ProductTypeFilter = new ProductTypeFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = ProductType.Id },
                Selects = ProductTypeSelect.Id
            };

            int count = await UOW.ProductTypeRepository.Count(ProductTypeFilter);

            if (count == 0)
                ProductType.AddError(nameof(ProductTypeValidator), nameof(ProductType.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(ProductType ProductType)
        {
            return ProductType.IsValidated;
        }

        public async Task<bool> Update(ProductType ProductType)
        {
            if (await ValidateId(ProductType))
            {
            }
            return ProductType.IsValidated;
        }

        public async Task<bool> Delete(ProductType ProductType)
        {
            if (await ValidateId(ProductType))
            {
            }
            return ProductType.IsValidated;
        }
    }
}
