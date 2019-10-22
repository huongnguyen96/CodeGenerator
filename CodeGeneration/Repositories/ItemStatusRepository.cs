
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
    public interface IItemStatusRepository
    {
        Task<int> Count(ItemStatusFilter ItemStatusFilter);
        Task<List<ItemStatus>> List(ItemStatusFilter ItemStatusFilter);
        Task<ItemStatus> Get(long Id);
        Task<bool> Create(ItemStatus ItemStatus);
        Task<bool> Update(ItemStatus ItemStatus);
        Task<bool> Delete(ItemStatus ItemStatus);
        
    }
    public class ItemStatusRepository : IItemStatusRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public ItemStatusRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ItemStatusDAO> DynamicFilter(IQueryable<ItemStatusDAO> query, ItemStatusFilter filter)
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
        private IQueryable<ItemStatusDAO> DynamicOrder(IQueryable<ItemStatusDAO> query,  ItemStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ItemStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ItemStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ItemStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ItemStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ItemStatus>> DynamicSelect(IQueryable<ItemStatusDAO> query, ItemStatusFilter filter)
        {
            List <ItemStatus> ItemStatuss = await query.Select(q => new ItemStatus()
            {
                
                Id = filter.Selects.Contains(ItemStatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ItemStatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ItemStatusSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return ItemStatuss;
        }

        public async Task<int> Count(ItemStatusFilter filter)
        {
            IQueryable <ItemStatusDAO> ItemStatusDAOs = DataContext.ItemStatus;
            ItemStatusDAOs = DynamicFilter(ItemStatusDAOs, filter);
            return await ItemStatusDAOs.CountAsync();
        }

        public async Task<List<ItemStatus>> List(ItemStatusFilter filter)
        {
            if (filter == null) return new List<ItemStatus>();
            IQueryable<ItemStatusDAO> ItemStatusDAOs = DataContext.ItemStatus;
            ItemStatusDAOs = DynamicFilter(ItemStatusDAOs, filter);
            ItemStatusDAOs = DynamicOrder(ItemStatusDAOs, filter);
            var ItemStatuss = await DynamicSelect(ItemStatusDAOs, filter);
            return ItemStatuss;
        }

        
        public async Task<ItemStatus> Get(long Id)
        {
            ItemStatus ItemStatus = await DataContext.ItemStatus.Where(x => x.Id == Id).Select(ItemStatusDAO => new ItemStatus()
            {
                 
                Id = ItemStatusDAO.Id,
                Code = ItemStatusDAO.Code,
                Name = ItemStatusDAO.Name,
            }).FirstOrDefaultAsync();
            return ItemStatus;
        }

        public async Task<bool> Create(ItemStatus ItemStatus)
        {
            ItemStatusDAO ItemStatusDAO = new ItemStatusDAO();
            
            ItemStatusDAO.Id = ItemStatus.Id;
            ItemStatusDAO.Code = ItemStatus.Code;
            ItemStatusDAO.Name = ItemStatus.Name;
            
            await DataContext.ItemStatus.AddAsync(ItemStatusDAO);
            await DataContext.SaveChangesAsync();
            ItemStatus.Id = ItemStatusDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(ItemStatus ItemStatus)
        {
            ItemStatusDAO ItemStatusDAO = DataContext.ItemStatus.Where(x => x.Id == ItemStatus.Id).FirstOrDefault();
            
            ItemStatusDAO.Id = ItemStatus.Id;
            ItemStatusDAO.Code = ItemStatus.Code;
            ItemStatusDAO.Name = ItemStatus.Name;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(ItemStatus ItemStatus)
        {
            ItemStatusDAO ItemStatusDAO = await DataContext.ItemStatus.Where(x => x.Id == ItemStatus.Id).FirstOrDefaultAsync();
            DataContext.ItemStatus.Remove(ItemStatusDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
