
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MProduct
{
    public interface IProductService : IServiceScoped
    {
        Task<int> Count(ProductFilter ProductFilter);
        Task<List<Product>> List(ProductFilter ProductFilter);
        Task<Product> Get(long Id);
        Task<Product> Create(Product Product);
        Task<Product> Update(Product Product);
        Task<Product> Delete(Product Product);
    }

    public class ProductService : IProductService
    {
        public IUOW UOW;
        public IProductValidator ProductValidator;

        public ProductService(
            IUOW UOW, 
            IProductValidator ProductValidator
        )
        {
            this.UOW = UOW;
            this.ProductValidator = ProductValidator;
        }
        public async Task<int> Count(ProductFilter ProductFilter)
        {
            int result = await UOW.ProductRepository.Count(ProductFilter);
            return result;
        }

        public async Task<List<Product>> List(ProductFilter ProductFilter)
        {
            List<Product> Products = await UOW.ProductRepository.List(ProductFilter);
            return Products;
        }

        public async Task<Product> Get(long Id)
        {
            Product Product = await UOW.ProductRepository.Get(Id);
            if (Product == null)
                return null;
            return Product;
        }

        public async Task<Product> Create(Product Product)
        {
            if (!await ProductValidator.Create(Product))
                return Product;

            try
            {
               
                await UOW.Begin();
                await UOW.ProductRepository.Create(Product);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(Product, "", nameof(ProductService));
                return await UOW.ProductRepository.Get(Product.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ProductService));
                throw new MessageException(ex);
            }
        }

        public async Task<Product> Update(Product Product)
        {
            if (!await ProductValidator.Update(Product))
                return Product;
            try
            {
                var oldData = await UOW.ProductRepository.Get(Product.Id);

                await UOW.Begin();
                await UOW.ProductRepository.Update(Product);
                await UOW.Commit();

                var newData = await UOW.ProductRepository.Get(Product.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(ProductService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ProductService));
                throw new MessageException(ex);
            }
        }

        public async Task<Product> Delete(Product Product)
        {
            if (!await ProductValidator.Delete(Product))
                return Product;

            try
            {
                await UOW.Begin();
                await UOW.ProductRepository.Delete(Product);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", Product, nameof(ProductService));
                return Product;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ProductService));
                throw new MessageException(ex);
            }
        }
    }
}
