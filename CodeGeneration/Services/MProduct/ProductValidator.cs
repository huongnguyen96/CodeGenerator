
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MProduct
{
    public interface IProductValidator : IServiceScoped
    {
        Task<bool> Create(Product Product);
        Task<bool> Update(Product Product);
        Task<bool> Delete(Product Product);
    }

    public class ProductValidator : IProductValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public ProductValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Product Product)
        {
            ProductFilter ProductFilter = new ProductFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = Product.Id },
                Selects = ProductSelect.Id
            };

            int count = await UOW.ProductRepository.Count(ProductFilter);

            if (count == 0)
                Product.AddError(nameof(ProductValidator), nameof(Product.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(Product Product)
        {
            return Product.IsValidated;
        }

        public async Task<bool> Update(Product Product)
        {
            if (await ValidateId(Product))
            {
            }
            return Product.IsValidated;
        }

        public async Task<bool> Delete(Product Product)
        {
            if (await ValidateId(Product))
            {
            }
            return Product.IsValidated;
        }
    }
}
