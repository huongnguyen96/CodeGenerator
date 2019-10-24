
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
    public interface IProductTypeRepository
    {
        Task<int> Count(ProductTypeFilter ProductTypeFilter);
        Task<List<ProductType>> List(ProductTypeFilter ProductTypeFilter);
        Task<ProductType> Get(long Id);
        Task<bool> Create(ProductType ProductType);
        Task<bool> Update(ProductType ProductType);
        Task<bool> Delete(ProductType ProductType);
        
    }
    public class ProductTypeRepository : IProductTypeRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public ProductTypeRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ProductTypeDAO> DynamicFilter(IQueryable<ProductTypeDAO> query, ProductTypeFilter filter)
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
        private IQueryable<ProductTypeDAO> DynamicOrder(IQueryable<ProductTypeDAO> query,  ProductTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ProductTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ProductTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ProductTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ProductTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ProductTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ProductTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ProductType>> DynamicSelect(IQueryable<ProductTypeDAO> query, ProductTypeFilter filter)
        {
            List <ProductType> ProductTypes = await query.Select(q => new ProductType()
            {
                
                Id = filter.Selects.Contains(ProductTypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ProductTypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ProductTypeSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return ProductTypes;
        }

        public async Task<int> Count(ProductTypeFilter filter)
        {
            IQueryable <ProductTypeDAO> ProductTypeDAOs = DataContext.ProductType;
            ProductTypeDAOs = DynamicFilter(ProductTypeDAOs, filter);
            return await ProductTypeDAOs.CountAsync();
        }

        public async Task<List<ProductType>> List(ProductTypeFilter filter)
        {
            if (filter == null) return new List<ProductType>();
            IQueryable<ProductTypeDAO> ProductTypeDAOs = DataContext.ProductType;
            ProductTypeDAOs = DynamicFilter(ProductTypeDAOs, filter);
            ProductTypeDAOs = DynamicOrder(ProductTypeDAOs, filter);
            var ProductTypes = await DynamicSelect(ProductTypeDAOs, filter);
            return ProductTypes;
        }

        
        public async Task<ProductType> Get(long Id)
        {
            ProductType ProductType = await DataContext.ProductType.Where(x => x.Id == Id).Select(ProductTypeDAO => new ProductType()
            {
                 
                Id = ProductTypeDAO.Id,
                Code = ProductTypeDAO.Code,
                Name = ProductTypeDAO.Name,
            }).FirstOrDefaultAsync();
            return ProductType;
        }

        public async Task<bool> Create(ProductType ProductType)
        {
            ProductTypeDAO ProductTypeDAO = new ProductTypeDAO();
            
            ProductTypeDAO.Id = ProductType.Id;
            ProductTypeDAO.Code = ProductType.Code;
            ProductTypeDAO.Name = ProductType.Name;
            
            await DataContext.ProductType.AddAsync(ProductTypeDAO);
            await DataContext.SaveChangesAsync();
            ProductType.Id = ProductTypeDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(ProductType ProductType)
        {
            ProductTypeDAO ProductTypeDAO = DataContext.ProductType.Where(x => x.Id == ProductType.Id).FirstOrDefault();
            
            ProductTypeDAO.Id = ProductType.Id;
            ProductTypeDAO.Code = ProductType.Code;
            ProductTypeDAO.Name = ProductType.Name;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(ProductType ProductType)
        {
            ProductTypeDAO ProductTypeDAO = await DataContext.ProductType.Where(x => x.Id == ProductType.Id).FirstOrDefaultAsync();
            DataContext.ProductType.Remove(ProductTypeDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
