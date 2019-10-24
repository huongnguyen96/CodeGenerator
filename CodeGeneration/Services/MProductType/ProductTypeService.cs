
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MProductType
{
    public interface IProductTypeService : IServiceScoped
    {
        Task<int> Count(ProductTypeFilter ProductTypeFilter);
        Task<List<ProductType>> List(ProductTypeFilter ProductTypeFilter);
        Task<ProductType> Get(long Id);
        Task<ProductType> Create(ProductType ProductType);
        Task<ProductType> Update(ProductType ProductType);
        Task<ProductType> Delete(ProductType ProductType);
    }

    public class ProductTypeService : IProductTypeService
    {
        public IUOW UOW;
        public IProductTypeValidator ProductTypeValidator;

        public ProductTypeService(
            IUOW UOW, 
            IProductTypeValidator ProductTypeValidator
        )
        {
            this.UOW = UOW;
            this.ProductTypeValidator = ProductTypeValidator;
        }
        public async Task<int> Count(ProductTypeFilter ProductTypeFilter)
        {
            int result = await UOW.ProductTypeRepository.Count(ProductTypeFilter);
            return result;
        }

        public async Task<List<ProductType>> List(ProductTypeFilter ProductTypeFilter)
        {
            List<ProductType> ProductTypes = await UOW.ProductTypeRepository.List(ProductTypeFilter);
            return ProductTypes;
        }

        public async Task<ProductType> Get(long Id)
        {
            ProductType ProductType = await UOW.ProductTypeRepository.Get(Id);
            if (ProductType == null)
                return null;
            return ProductType;
        }

        public async Task<ProductType> Create(ProductType ProductType)
        {
            if (!await ProductTypeValidator.Create(ProductType))
                return ProductType;

            try
            {
               
                await UOW.Begin();
                await UOW.ProductTypeRepository.Create(ProductType);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(ProductType, "", nameof(ProductTypeService));
                return await UOW.ProductTypeRepository.Get(ProductType.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ProductTypeService));
                throw new MessageException(ex);
            }
        }

        public async Task<ProductType> Update(ProductType ProductType)
        {
            if (!await ProductTypeValidator.Update(ProductType))
                return ProductType;
            try
            {
                var oldData = await UOW.ProductTypeRepository.Get(ProductType.Id);

                await UOW.Begin();
                await UOW.ProductTypeRepository.Update(ProductType);
                await UOW.Commit();

                var newData = await UOW.ProductTypeRepository.Get(ProductType.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(ProductTypeService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ProductTypeService));
                throw new MessageException(ex);
            }
        }

        public async Task<ProductType> Delete(ProductType ProductType)
        {
            if (!await ProductTypeValidator.Delete(ProductType))
                return ProductType;

            try
            {
                await UOW.Begin();
                await UOW.ProductTypeRepository.Delete(ProductType);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", ProductType, nameof(ProductTypeService));
                return ProductType;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ProductTypeService));
                throw new MessageException(ex);
            }
        }
    }
}
