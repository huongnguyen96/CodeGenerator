
using Common;
using WG.Entities;
using CodeGeneration.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Repositories
{
    public interface IProductStatusRepository
    {
        Task<int> Count(ProductStatusFilter ProductStatusFilter);
        Task<List<ProductStatus>> List(ProductStatusFilter ProductStatusFilter);
        Task<ProductStatus> Get(long Id);
        Task<bool> Create(ProductStatus ProductStatus);
        Task<bool> Update(ProductStatus ProductStatus);
        Task<bool> Delete(ProductStatus ProductStatus);
        
    }
    public class ProductStatusRepository : IProductStatusRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public ProductStatusRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ProductStatusDAO> DynamicFilter(IQueryable<ProductStatusDAO> query, ProductStatusFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<ProductStatusDAO> DynamicOrder(IQueryable<ProductStatusDAO> query,  ProductStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ProductStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ProductStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ProductStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ProductStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ProductStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ProductStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ProductStatus>> DynamicSelect(IQueryable<ProductStatusDAO> query, ProductStatusFilter filter)
        {
            List <ProductStatus> ProductStatuss = await query.Select(q => new ProductStatus()
            {
                
                Id = filter.Selects.Contains(ProductStatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ProductStatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ProductStatusSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return ProductStatuss;
        }

        public async Task<int> Count(ProductStatusFilter filter)
        {
            IQueryable <ProductStatusDAO> ProductStatusDAOs = DataContext.ProductStatus;
            ProductStatusDAOs = DynamicFilter(ProductStatusDAOs, filter);
            return await ProductStatusDAOs.CountAsync();
        }

        public async Task<List<ProductStatus>> List(ProductStatusFilter filter)
        {
            if (filter == null) return new List<ProductStatus>();
            IQueryable<ProductStatusDAO> ProductStatusDAOs = DataContext.ProductStatus;
            ProductStatusDAOs = DynamicFilter(ProductStatusDAOs, filter);
            ProductStatusDAOs = DynamicOrder(ProductStatusDAOs, filter);
            var ProductStatuss = await DynamicSelect(ProductStatusDAOs, filter);
            return ProductStatuss;
        }

        
        public async Task<ProductStatus> Get(long Id)
        {
            ProductStatus ProductStatus = await DataContext.ProductStatus.Where(x => x.Id == Id).Select(ProductStatusDAO => new ProductStatus()
            {
                 
                Id = ProductStatusDAO.Id,
                Code = ProductStatusDAO.Code,
                Name = ProductStatusDAO.Name,
            }).FirstOrDefaultAsync();
            return ProductStatus;
        }

        public async Task<bool> Create(ProductStatus ProductStatus)
        {
            ProductStatusDAO ProductStatusDAO = new ProductStatusDAO();
            
            ProductStatusDAO.Id = ProductStatus.Id;
            ProductStatusDAO.Code = ProductStatus.Code;
            ProductStatusDAO.Name = ProductStatus.Name;
            
            await DataContext.ProductStatus.AddAsync(ProductStatusDAO);
            await DataContext.SaveChangesAsync();
            ProductStatus.Id = ProductStatusDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(ProductStatus ProductStatus)
        {
            ProductStatusDAO ProductStatusDAO = DataContext.ProductStatus.Where(x => x.Id == ProductStatus.Id).FirstOrDefault();
            
            ProductStatusDAO.Id = ProductStatus.Id;
            ProductStatusDAO.Code = ProductStatus.Code;
            ProductStatusDAO.Name = ProductStatus.Name;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(ProductStatus ProductStatus)
        {
            ProductStatusDAO ProductStatusDAO = await DataContext.ProductStatus.Where(x => x.Id == ProductStatus.Id).FirstOrDefaultAsync();
            DataContext.ProductStatus.Remove(ProductStatusDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
