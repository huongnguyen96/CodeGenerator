
using Common;
using ERP.Entities;
using CodeGeneration.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Repositories
{
    public interface IItemGroupingRepository
    {
        Task<int> Count(ItemGroupingFilter ItemGroupingFilter);
        Task<List<ItemGrouping>> List(ItemGroupingFilter ItemGroupingFilter);
        Task<ItemGrouping> Get(Guid Id);
        Task<bool> Create(ItemGrouping ItemGrouping);
        Task<bool> Update(ItemGrouping ItemGrouping);
        Task<bool> Delete(Guid Id);
        
    }
    public class ItemGroupingRepository : IItemGroupingRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public ItemGroupingRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ItemGroupingDAO> DynamicFilter(IQueryable<ItemGroupingDAO> query, ItemGroupingFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.LegalEntityId != null)
                query = query.Where(q => q.LegalEntityId, filter.LegalEntityId);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            return query;
        }
        private IQueryable<ItemGroupingDAO> DynamicOrder(IQueryable<ItemGroupingDAO> query,  ItemGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemGroupingOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ItemGroupingOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case ItemGroupingOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemGroupingOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ItemGroupingOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case ItemGroupingOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        default:
                            query = query.OrderByDescending(q => q.CX);
                            break;
                    }
                    break;
                default:
                    query = query.OrderBy(q => q.CX);
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ItemGrouping>> DynamicSelect(IQueryable<ItemGroupingDAO> query, ItemGroupingFilter filter)
        {
            List <ItemGrouping> ItemGroupings = await query.Select(q => new ItemGrouping()
            {
                
                Id = filter.Selects.Contains(ItemGroupingSelect.Id) ? q.Id : default(Guid),
                BusinessGroupId = filter.Selects.Contains(ItemGroupingSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                LegalEntityId = filter.Selects.Contains(ItemGroupingSelect.LegalEntity) ? q.LegalEntityId : default(Guid),
                Code = filter.Selects.Contains(ItemGroupingSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ItemGroupingSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(ItemGroupingSelect.Description) ? q.Description : default(string),
            }).ToListAsync();
            return ItemGroupings;
        }

        public async Task<int> Count(ItemGroupingFilter filter)
        {
            IQueryable <ItemGroupingDAO> ItemGroupingDAOs = ERPContext.ItemGrouping;
            ItemGroupingDAOs = DynamicFilter(ItemGroupingDAOs, filter);
            return await ItemGroupingDAOs.CountAsync();
        }

        public async Task<List<ItemGrouping>> List(ItemGroupingFilter filter)
        {
            if (filter == null) return new List<ItemGrouping>();
            IQueryable<ItemGroupingDAO> ItemGroupingDAOs = ERPContext.ItemGrouping;
            ItemGroupingDAOs = DynamicFilter(ItemGroupingDAOs, filter);
            ItemGroupingDAOs = DynamicOrder(ItemGroupingDAOs, filter);
            var ItemGroupings = await DynamicSelect(ItemGroupingDAOs, filter);
            return ItemGroupings;
        }

        public async Task<ItemGrouping> Get(Guid Id)
        {
            ItemGrouping ItemGrouping = await ERPContext.ItemGrouping.Where(l => l.Id == Id).Select(ItemGroupingDAO => new ItemGrouping()
            {
                 
                Id = ItemGroupingDAO.Id,
                BusinessGroupId = ItemGroupingDAO.BusinessGroupId,
                LegalEntityId = ItemGroupingDAO.LegalEntityId,
                Code = ItemGroupingDAO.Code,
                Name = ItemGroupingDAO.Name,
                Description = ItemGroupingDAO.Description,
            }).FirstOrDefaultAsync();
            return ItemGrouping;
        }

        public async Task<bool> Create(ItemGrouping ItemGrouping)
        {
            ItemGroupingDAO ItemGroupingDAO = new ItemGroupingDAO();
            
            ItemGroupingDAO.Id = ItemGrouping.Id;
            ItemGroupingDAO.BusinessGroupId = ItemGrouping.BusinessGroupId;
            ItemGroupingDAO.LegalEntityId = ItemGrouping.LegalEntityId;
            ItemGroupingDAO.Code = ItemGrouping.Code;
            ItemGroupingDAO.Name = ItemGrouping.Name;
            ItemGroupingDAO.Description = ItemGrouping.Description;
            ItemGroupingDAO.Disabled = false;
            
            await ERPContext.ItemGrouping.AddAsync(ItemGroupingDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(ItemGrouping ItemGrouping)
        {
            ItemGroupingDAO ItemGroupingDAO = ERPContext.ItemGrouping.Where(b => b.Id == ItemGrouping.Id).FirstOrDefault();
            
            ItemGroupingDAO.Id = ItemGrouping.Id;
            ItemGroupingDAO.BusinessGroupId = ItemGrouping.BusinessGroupId;
            ItemGroupingDAO.LegalEntityId = ItemGrouping.LegalEntityId;
            ItemGroupingDAO.Code = ItemGrouping.Code;
            ItemGroupingDAO.Name = ItemGrouping.Name;
            ItemGroupingDAO.Description = ItemGrouping.Description;
            ItemGroupingDAO.Disabled = false;
            ERPContext.ItemGrouping.Update(ItemGroupingDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            ItemGroupingDAO ItemGroupingDAO = await ERPContext.ItemGrouping.Where(x => x.Id == Id).FirstOrDefaultAsync();
            ItemGroupingDAO.Disabled = true;
            ERPContext.ItemGrouping.Update(ItemGroupingDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
