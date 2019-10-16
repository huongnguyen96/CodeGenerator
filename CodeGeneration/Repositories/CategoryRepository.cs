
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
    public interface ICategoryRepository
    {
        Task<int> Count(CategoryFilter CategoryFilter);
        Task<List<Category>> List(CategoryFilter CategoryFilter);
        Task<Category> Get(long Id);
        Task<bool> Create(Category Category);
        Task<bool> Update(Category Category);
        Task<bool> Delete(Category Category);
        
    }
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public CategoryRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<CategoryDAO> DynamicFilter(IQueryable<CategoryDAO> query, CategoryFilter filter)
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
        private IQueryable<CategoryDAO> DynamicOrder(IQueryable<CategoryDAO> query,  CategoryFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case CategoryOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CategoryOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CategoryOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case CategoryOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CategoryOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CategoryOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Category>> DynamicSelect(IQueryable<CategoryDAO> query, CategoryFilter filter)
        {
            List <Category> Categorys = await query.Select(q => new Category()
            {
                
                Id = filter.Selects.Contains(CategorySelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CategorySelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CategorySelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return Categorys;
        }

        public async Task<int> Count(CategoryFilter filter)
        {
            IQueryable <CategoryDAO> CategoryDAOs = DataContext.Category;
            CategoryDAOs = DynamicFilter(CategoryDAOs, filter);
            return await CategoryDAOs.CountAsync();
        }

        public async Task<List<Category>> List(CategoryFilter filter)
        {
            if (filter == null) return new List<Category>();
            IQueryable<CategoryDAO> CategoryDAOs = DataContext.Category;
            CategoryDAOs = DynamicFilter(CategoryDAOs, filter);
            CategoryDAOs = DynamicOrder(CategoryDAOs, filter);
            var Categorys = await DynamicSelect(CategoryDAOs, filter);
            return Categorys;
        }

        
        public async Task<Category> Get(long Id)
        {
            Category Category = await DataContext.Category.Where(x => x.Id == Id).Select(CategoryDAO => new Category()
            {
                 
                Id = CategoryDAO.Id,
                Code = CategoryDAO.Code,
                Name = CategoryDAO.Name,
            }).FirstOrDefaultAsync();
            return Category;
        }

        public async Task<bool> Create(Category Category)
        {
            CategoryDAO CategoryDAO = new CategoryDAO();
            
            CategoryDAO.Id = Category.Id;
            CategoryDAO.Code = Category.Code;
            CategoryDAO.Name = Category.Name;
            
            await DataContext.Category.AddAsync(CategoryDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        
        public async Task<bool> Update(Category Category)
        {
            CategoryDAO CategoryDAO = DataContext.Category.Where(x => x.Id == Category.Id).FirstOrDefault();
            
            CategoryDAO.Id = Category.Id;
            CategoryDAO.Code = Category.Code;
            CategoryDAO.Name = Category.Name;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Category Category)
        {
            CategoryDAO CategoryDAO = await DataContext.Category.Where(x => x.Id == Category.Id).FirstOrDefaultAsync();
            DataContext.Category.Remove(CategoryDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
