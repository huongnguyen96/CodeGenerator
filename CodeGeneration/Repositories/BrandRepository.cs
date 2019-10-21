
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
    public interface IBrandRepository
    {
        Task<int> Count(BrandFilter BrandFilter);
        Task<List<Brand>> List(BrandFilter BrandFilter);
        Task<Brand> Get(long Id);
        Task<bool> Create(Brand Brand);
        Task<bool> Update(Brand Brand);
        Task<bool> Delete(Brand Brand);
        
    }
    public class BrandRepository : IBrandRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public BrandRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<BrandDAO> DynamicFilter(IQueryable<BrandDAO> query, BrandFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.CategoryId != null)
                query = query.Where(q => q.CategoryId, filter.CategoryId);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<BrandDAO> DynamicOrder(IQueryable<BrandDAO> query,  BrandFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case BrandOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case BrandOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case BrandOrder.Category:
                            query = query.OrderBy(q => q.Category.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case BrandOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case BrandOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case BrandOrder.Category:
                            query = query.OrderByDescending(q => q.Category.Id);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Brand>> DynamicSelect(IQueryable<BrandDAO> query, BrandFilter filter)
        {
            List <Brand> Brands = await query.Select(q => new Brand()
            {
                
                Id = filter.Selects.Contains(BrandSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(BrandSelect.Name) ? q.Name : default(string),
                CategoryId = filter.Selects.Contains(BrandSelect.Category) ? q.CategoryId : default(long),
                Category = filter.Selects.Contains(BrandSelect.Category) && q.Category != null ? new Category
                {
                    
                    Id = q.Category.Id,
                    Code = q.Category.Code,
                    Name = q.Category.Name,
                    ParentId = q.Category.ParentId,
                    Icon = q.Category.Icon,
                } : null,
            }).ToListAsync();
            return Brands;
        }

        public async Task<int> Count(BrandFilter filter)
        {
            IQueryable <BrandDAO> BrandDAOs = DataContext.Brand;
            BrandDAOs = DynamicFilter(BrandDAOs, filter);
            return await BrandDAOs.CountAsync();
        }

        public async Task<List<Brand>> List(BrandFilter filter)
        {
            if (filter == null) return new List<Brand>();
            IQueryable<BrandDAO> BrandDAOs = DataContext.Brand;
            BrandDAOs = DynamicFilter(BrandDAOs, filter);
            BrandDAOs = DynamicOrder(BrandDAOs, filter);
            var Brands = await DynamicSelect(BrandDAOs, filter);
            return Brands;
        }

        
        public async Task<Brand> Get(long Id)
        {
            Brand Brand = await DataContext.Brand.Where(x => x.Id == Id).Select(BrandDAO => new Brand()
            {
                 
                Id = BrandDAO.Id,
                Name = BrandDAO.Name,
                CategoryId = BrandDAO.CategoryId,
                Category = BrandDAO.Category == null ? null : new Category
                {
                    
                    Id = BrandDAO.Category.Id,
                    Code = BrandDAO.Category.Code,
                    Name = BrandDAO.Category.Name,
                    ParentId = BrandDAO.Category.ParentId,
                    Icon = BrandDAO.Category.Icon,
                },
            }).FirstOrDefaultAsync();
            return Brand;
        }

        public async Task<bool> Create(Brand Brand)
        {
            BrandDAO BrandDAO = new BrandDAO();
            
            BrandDAO.Id = Brand.Id;
            BrandDAO.Name = Brand.Name;
            BrandDAO.CategoryId = Brand.CategoryId;
            
            await DataContext.Brand.AddAsync(BrandDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        
        public async Task<bool> Update(Brand Brand)
        {
            BrandDAO BrandDAO = DataContext.Brand.Where(x => x.Id == Brand.Id).FirstOrDefault();
            
            BrandDAO.Id = Brand.Id;
            BrandDAO.Name = Brand.Name;
            BrandDAO.CategoryId = Brand.CategoryId;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Brand Brand)
        {
            BrandDAO BrandDAO = await DataContext.Brand.Where(x => x.Id == Brand.Id).FirstOrDefaultAsync();
            DataContext.Brand.Remove(BrandDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
