
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MProductStatus
{
    public interface IProductStatusService : IServiceScoped
    {
        Task<int> Count(ProductStatusFilter ProductStatusFilter);
        Task<List<ProductStatus>> List(ProductStatusFilter ProductStatusFilter);
        Task<ProductStatus> Get(long Id);
        Task<ProductStatus> Create(ProductStatus ProductStatus);
        Task<ProductStatus> Update(ProductStatus ProductStatus);
        Task<ProductStatus> Delete(ProductStatus ProductStatus);
    }

    public class ProductStatusService : IProductStatusService
    {
        public IUOW UOW;
        public IProductStatusValidator ProductStatusValidator;

        public ProductStatusService(
            IUOW UOW, 
            IProductStatusValidator ProductStatusValidator
        )
        {
            this.UOW = UOW;
            this.ProductStatusValidator = ProductStatusValidator;
        }
        public async Task<int> Count(ProductStatusFilter ProductStatusFilter)
        {
            int result = await UOW.ProductStatusRepository.Count(ProductStatusFilter);
            return result;
        }

        public async Task<List<ProductStatus>> List(ProductStatusFilter ProductStatusFilter)
        {
            List<ProductStatus> ProductStatuss = await UOW.ProductStatusRepository.List(ProductStatusFilter);
            return ProductStatuss;
        }

        public async Task<ProductStatus> Get(long Id)
        {
            ProductStatus ProductStatus = await UOW.ProductStatusRepository.Get(Id);
            if (ProductStatus == null)
                return null;
            return ProductStatus;
        }

        public async Task<ProductStatus> Create(ProductStatus ProductStatus)
        {
            if (!await ProductStatusValidator.Create(ProductStatus))
                return ProductStatus;

            try
            {
               
                await UOW.Begin();
                await UOW.ProductStatusRepository.Create(ProductStatus);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(ProductStatus, "", nameof(ProductStatusService));
                return await UOW.ProductStatusRepository.Get(ProductStatus.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ProductStatusService));
                throw new MessageException(ex);
            }
        }

        public async Task<ProductStatus> Update(ProductStatus ProductStatus)
        {
            if (!await ProductStatusValidator.Update(ProductStatus))
                return ProductStatus;
            try
            {
                var oldData = await UOW.ProductStatusRepository.Get(ProductStatus.Id);

                await UOW.Begin();
                await UOW.ProductStatusRepository.Update(ProductStatus);
                await UOW.Commit();

                var newData = await UOW.ProductStatusRepository.Get(ProductStatus.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(ProductStatusService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ProductStatusService));
                throw new MessageException(ex);
            }
        }

        public async Task<ProductStatus> Delete(ProductStatus ProductStatus)
        {
            if (!await ProductStatusValidator.Delete(ProductStatus))
                return ProductStatus;

            try
            {
                await UOW.Begin();
                await UOW.ProductStatusRepository.Delete(ProductStatus);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", ProductStatus, nameof(ProductStatusService));
                return ProductStatus;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ProductStatusService));
                throw new MessageException(ex);
            }
        }
    }
}
