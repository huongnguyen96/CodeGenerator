
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
    public interface IVariationGroupingRepository
    {
        Task<int> Count(VariationGroupingFilter VariationGroupingFilter);
        Task<List<VariationGrouping>> List(VariationGroupingFilter VariationGroupingFilter);
        Task<VariationGrouping> Get(long Id);
        Task<bool> Create(VariationGrouping VariationGrouping);
        Task<bool> Update(VariationGrouping VariationGrouping);
        Task<bool> Delete(VariationGrouping VariationGrouping);
        
    }
    public class VariationGroupingRepository : IVariationGroupingRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public VariationGroupingRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<VariationGroupingDAO> DynamicFilter(IQueryable<VariationGroupingDAO> query, VariationGroupingFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.ItemId != null)
                query = query.Where(q => q.ItemId, filter.ItemId);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<VariationGroupingDAO> DynamicOrder(IQueryable<VariationGroupingDAO> query,  VariationGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case VariationGroupingOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case VariationGroupingOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case VariationGroupingOrder.Item:
                            query = query.OrderBy(q => q.Item.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case VariationGroupingOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case VariationGroupingOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case VariationGroupingOrder.Item:
                            query = query.OrderByDescending(q => q.Item.Id);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<VariationGrouping>> DynamicSelect(IQueryable<VariationGroupingDAO> query, VariationGroupingFilter filter)
        {
            List <VariationGrouping> VariationGroupings = await query.Select(q => new VariationGrouping()
            {
                
                Id = filter.Selects.Contains(VariationGroupingSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(VariationGroupingSelect.Name) ? q.Name : default(string),
                ItemId = filter.Selects.Contains(VariationGroupingSelect.Item) ? q.ItemId : default(long),
                Item = filter.Selects.Contains(VariationGroupingSelect.Item) && q.Item != null ? new Item
                {
                    
                    Id = q.Item.Id,
                    Code = q.Item.Code,
                    Name = q.Item.Name,
                    SKU = q.Item.SKU,
                    Description = q.Item.Description,
                    TypeId = q.Item.TypeId,
                    StatusId = q.Item.StatusId,
                    PartnerId = q.Item.PartnerId,
                    CategoryId = q.Item.CategoryId,
                    BrandId = q.Item.BrandId,
                } : null,
            }).ToListAsync();
            return VariationGroupings;
        }

        public async Task<int> Count(VariationGroupingFilter filter)
        {
            IQueryable <VariationGroupingDAO> VariationGroupingDAOs = DataContext.VariationGrouping;
            VariationGroupingDAOs = DynamicFilter(VariationGroupingDAOs, filter);
            return await VariationGroupingDAOs.CountAsync();
        }

        public async Task<List<VariationGrouping>> List(VariationGroupingFilter filter)
        {
            if (filter == null) return new List<VariationGrouping>();
            IQueryable<VariationGroupingDAO> VariationGroupingDAOs = DataContext.VariationGrouping;
            VariationGroupingDAOs = DynamicFilter(VariationGroupingDAOs, filter);
            VariationGroupingDAOs = DynamicOrder(VariationGroupingDAOs, filter);
            var VariationGroupings = await DynamicSelect(VariationGroupingDAOs, filter);
            return VariationGroupings;
        }

        
        public async Task<VariationGrouping> Get(long Id)
        {
            VariationGrouping VariationGrouping = await DataContext.VariationGrouping.Where(x => x.Id == Id).Select(VariationGroupingDAO => new VariationGrouping()
            {
                 
                Id = VariationGroupingDAO.Id,
                Name = VariationGroupingDAO.Name,
                ItemId = VariationGroupingDAO.ItemId,
                Item = VariationGroupingDAO.Item == null ? null : new Item
                {
                    
                    Id = VariationGroupingDAO.Item.Id,
                    Code = VariationGroupingDAO.Item.Code,
                    Name = VariationGroupingDAO.Item.Name,
                    SKU = VariationGroupingDAO.Item.SKU,
                    Description = VariationGroupingDAO.Item.Description,
                    TypeId = VariationGroupingDAO.Item.TypeId,
                    StatusId = VariationGroupingDAO.Item.StatusId,
                    PartnerId = VariationGroupingDAO.Item.PartnerId,
                    CategoryId = VariationGroupingDAO.Item.CategoryId,
                    BrandId = VariationGroupingDAO.Item.BrandId,
                },
            }).FirstOrDefaultAsync();
            return VariationGrouping;
        }

        public async Task<bool> Create(VariationGrouping VariationGrouping)
        {
            VariationGroupingDAO VariationGroupingDAO = new VariationGroupingDAO();
            
            VariationGroupingDAO.Id = VariationGrouping.Id;
            VariationGroupingDAO.Name = VariationGrouping.Name;
            VariationGroupingDAO.ItemId = VariationGrouping.ItemId;
            
            await DataContext.VariationGrouping.AddAsync(VariationGroupingDAO);
            await DataContext.SaveChangesAsync();
            VariationGrouping.Id = VariationGroupingDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(VariationGrouping VariationGrouping)
        {
            VariationGroupingDAO VariationGroupingDAO = DataContext.VariationGrouping.Where(x => x.Id == VariationGrouping.Id).FirstOrDefault();
            
            VariationGroupingDAO.Id = VariationGrouping.Id;
            VariationGroupingDAO.Name = VariationGrouping.Name;
            VariationGroupingDAO.ItemId = VariationGrouping.ItemId;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(VariationGrouping VariationGrouping)
        {
            VariationGroupingDAO VariationGroupingDAO = await DataContext.VariationGrouping.Where(x => x.Id == VariationGrouping.Id).FirstOrDefaultAsync();
            DataContext.VariationGrouping.Remove(VariationGroupingDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
