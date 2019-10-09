
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
    public interface IItemDiscountRepository
    {
        Task<int> Count(ItemDiscountFilter ItemDiscountFilter);
        Task<List<ItemDiscount>> List(ItemDiscountFilter ItemDiscountFilter);
        Task<ItemDiscount> Get(Guid Id);
        Task<bool> Create(ItemDiscount ItemDiscount);
        Task<bool> Update(ItemDiscount ItemDiscount);
        Task<bool> Delete(Guid Id);
        
    }
    public class ItemDiscountRepository : IItemDiscountRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public ItemDiscountRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ItemDiscountDAO> DynamicFilter(IQueryable<ItemDiscountDAO> query, ItemDiscountFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.ItemDetailId != null)
                query = query.Where(q => q.ItemDetailId, filter.ItemDetailId);
            if (filter.QuantityFrom != null)
                query = query.Where(q => q.QuantityFrom, filter.QuantityFrom);
            if (filter.QuantityTo != null)
                query = query.Where(q => q.QuantityTo, filter.QuantityTo);
            if (filter.Rate.HasValue)
                query = query.Where(q => q.Rate.HasValue && q.Rate.Value == filter.Rate.Value);
            if (filter.Rate != null)
                query = query.Where(q => q.Rate, filter.Rate);
            if (filter.DiscountType != null)
                query = query.Where(q => q.DiscountType, filter.DiscountType);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<ItemDiscountDAO> DynamicOrder(IQueryable<ItemDiscountDAO> query,  ItemDiscountFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemDiscountOrder.QuantityFrom:
                            query = query.OrderBy(q => q.QuantityFrom);
                            break;
                        case ItemDiscountOrder.QuantityTo:
                            query = query.OrderBy(q => q.QuantityTo);
                            break;
                        case ItemDiscountOrder.Rate:
                            query = query.OrderBy(q => q.Rate);
                            break;
                        case ItemDiscountOrder.DiscountType:
                            query = query.OrderBy(q => q.DiscountType);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemDiscountOrder.QuantityFrom:
                            query = query.OrderByDescending(q => q.QuantityFrom);
                            break;
                        case ItemDiscountOrder.QuantityTo:
                            query = query.OrderByDescending(q => q.QuantityTo);
                            break;
                        case ItemDiscountOrder.Rate:
                            query = query.OrderByDescending(q => q.Rate);
                            break;
                        case ItemDiscountOrder.DiscountType:
                            query = query.OrderByDescending(q => q.DiscountType);
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

        private async Task<List<ItemDiscount>> DynamicSelect(IQueryable<ItemDiscountDAO> query, ItemDiscountFilter filter)
        {
            List <ItemDiscount> ItemDiscounts = await query.Select(q => new ItemDiscount()
            {
                
                Id = filter.Selects.Contains(ItemDiscountSelect.Id) ? q.Id : default(Guid),
                ItemDetailId = filter.Selects.Contains(ItemDiscountSelect.ItemDetail) ? q.ItemDetailId : default(Guid),
                QuantityFrom = filter.Selects.Contains(ItemDiscountSelect.QuantityFrom) ? q.QuantityFrom : default(int),
                QuantityTo = filter.Selects.Contains(ItemDiscountSelect.QuantityTo) ? q.QuantityTo : default(int),
                Rate = filter.Selects.Contains(ItemDiscountSelect.Rate) ? q.Rate : default(Guid?),
                DiscountType = filter.Selects.Contains(ItemDiscountSelect.DiscountType) ? q.DiscountType : default(string),
                BusinessGroupId = filter.Selects.Contains(ItemDiscountSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return ItemDiscounts;
        }

        public async Task<int> Count(ItemDiscountFilter filter)
        {
            IQueryable <ItemDiscountDAO> ItemDiscountDAOs = ERPContext.ItemDiscount;
            ItemDiscountDAOs = DynamicFilter(ItemDiscountDAOs, filter);
            return await ItemDiscountDAOs.CountAsync();
        }

        public async Task<List<ItemDiscount>> List(ItemDiscountFilter filter)
        {
            if (filter == null) return new List<ItemDiscount>();
            IQueryable<ItemDiscountDAO> ItemDiscountDAOs = ERPContext.ItemDiscount;
            ItemDiscountDAOs = DynamicFilter(ItemDiscountDAOs, filter);
            ItemDiscountDAOs = DynamicOrder(ItemDiscountDAOs, filter);
            var ItemDiscounts = await DynamicSelect(ItemDiscountDAOs, filter);
            return ItemDiscounts;
        }

        public async Task<ItemDiscount> Get(Guid Id)
        {
            ItemDiscount ItemDiscount = await ERPContext.ItemDiscount.Where(l => l.Id == Id).Select(ItemDiscountDAO => new ItemDiscount()
            {
                 
                Id = ItemDiscountDAO.Id,
                ItemDetailId = ItemDiscountDAO.ItemDetailId,
                QuantityFrom = ItemDiscountDAO.QuantityFrom,
                QuantityTo = ItemDiscountDAO.QuantityTo,
                Rate = ItemDiscountDAO.Rate,
                DiscountType = ItemDiscountDAO.DiscountType,
                BusinessGroupId = ItemDiscountDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return ItemDiscount;
        }

        public async Task<bool> Create(ItemDiscount ItemDiscount)
        {
            ItemDiscountDAO ItemDiscountDAO = new ItemDiscountDAO();
            
            ItemDiscountDAO.Id = ItemDiscount.Id;
            ItemDiscountDAO.ItemDetailId = ItemDiscount.ItemDetailId;
            ItemDiscountDAO.QuantityFrom = ItemDiscount.QuantityFrom;
            ItemDiscountDAO.QuantityTo = ItemDiscount.QuantityTo;
            ItemDiscountDAO.Rate = ItemDiscount.Rate;
            ItemDiscountDAO.DiscountType = ItemDiscount.DiscountType;
            ItemDiscountDAO.BusinessGroupId = ItemDiscount.BusinessGroupId;
            ItemDiscountDAO.Disabled = false;
            
            await ERPContext.ItemDiscount.AddAsync(ItemDiscountDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(ItemDiscount ItemDiscount)
        {
            ItemDiscountDAO ItemDiscountDAO = ERPContext.ItemDiscount.Where(b => b.Id == ItemDiscount.Id).FirstOrDefault();
            
            ItemDiscountDAO.Id = ItemDiscount.Id;
            ItemDiscountDAO.ItemDetailId = ItemDiscount.ItemDetailId;
            ItemDiscountDAO.QuantityFrom = ItemDiscount.QuantityFrom;
            ItemDiscountDAO.QuantityTo = ItemDiscount.QuantityTo;
            ItemDiscountDAO.Rate = ItemDiscount.Rate;
            ItemDiscountDAO.DiscountType = ItemDiscount.DiscountType;
            ItemDiscountDAO.BusinessGroupId = ItemDiscount.BusinessGroupId;
            ItemDiscountDAO.Disabled = false;
            ERPContext.ItemDiscount.Update(ItemDiscountDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            ItemDiscountDAO ItemDiscountDAO = await ERPContext.ItemDiscount.Where(x => x.Id == Id).FirstOrDefaultAsync();
            ItemDiscountDAO.Disabled = true;
            ERPContext.ItemDiscount.Update(ItemDiscountDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
