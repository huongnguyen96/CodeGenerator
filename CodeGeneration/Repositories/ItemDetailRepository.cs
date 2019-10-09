
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
    public interface IItemDetailRepository
    {
        Task<int> Count(ItemDetailFilter ItemDetailFilter);
        Task<List<ItemDetail>> List(ItemDetailFilter ItemDetailFilter);
        Task<ItemDetail> Get(Guid Id);
        Task<bool> Create(ItemDetail ItemDetail);
        Task<bool> Update(ItemDetail ItemDetail);
        Task<bool> Delete(Guid Id);
        
    }
    public class ItemDetailRepository : IItemDetailRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public ItemDetailRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ItemDetailDAO> DynamicFilter(IQueryable<ItemDetailDAO> query, ItemDetailFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.ItemId != null)
                query = query.Where(q => q.ItemId, filter.ItemId);
            if (filter.LegalEntityId != null)
                query = query.Where(q => q.LegalEntityId, filter.LegalEntityId);
            if (filter.DefaultValue.HasValue)
                query = query.Where(q => q.DefaultValue.HasValue && q.DefaultValue.Value == filter.DefaultValue.Value);
            if (filter.DefaultValue != null)
                query = query.Where(q => q.DefaultValue, filter.DefaultValue);
            if (filter.InventoryAccountId.HasValue)
                query = query.Where(q => q.InventoryAccountId.HasValue && q.InventoryAccountId.Value == filter.InventoryAccountId.Value);
            if (filter.InventoryAccountId != null)
                query = query.Where(q => q.InventoryAccountId, filter.InventoryAccountId);
            if (filter.ReturnAccountId.HasValue)
                query = query.Where(q => q.ReturnAccountId.HasValue && q.ReturnAccountId.Value == filter.ReturnAccountId.Value);
            if (filter.ReturnAccountId != null)
                query = query.Where(q => q.ReturnAccountId, filter.ReturnAccountId);
            if (filter.SalesAllowancesAccountId.HasValue)
                query = query.Where(q => q.SalesAllowancesAccountId.HasValue && q.SalesAllowancesAccountId.Value == filter.SalesAllowancesAccountId.Value);
            if (filter.SalesAllowancesAccountId != null)
                query = query.Where(q => q.SalesAllowancesAccountId, filter.SalesAllowancesAccountId);
            if (filter.ExpenseAccountId.HasValue)
                query = query.Where(q => q.ExpenseAccountId.HasValue && q.ExpenseAccountId.Value == filter.ExpenseAccountId.Value);
            if (filter.ExpenseAccountId != null)
                query = query.Where(q => q.ExpenseAccountId, filter.ExpenseAccountId);
            if (filter.RevenueAccountId.HasValue)
                query = query.Where(q => q.RevenueAccountId.HasValue && q.RevenueAccountId.Value == filter.RevenueAccountId.Value);
            if (filter.RevenueAccountId != null)
                query = query.Where(q => q.RevenueAccountId, filter.RevenueAccountId);
            if (filter.DiscountAccountId.HasValue)
                query = query.Where(q => q.DiscountAccountId.HasValue && q.DiscountAccountId.Value == filter.DiscountAccountId.Value);
            if (filter.DiscountAccountId != null)
                query = query.Where(q => q.DiscountAccountId, filter.DiscountAccountId);
            if (filter.IsDiscounted.HasValue)
                query = query.Where(q => q.IsDiscounted == filter.IsDiscounted.Value);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<ItemDetailDAO> DynamicOrder(IQueryable<ItemDetailDAO> query,  ItemDetailFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemDetailOrder.DefaultValue:
                            query = query.OrderBy(q => q.DefaultValue);
                            break;
                        case ItemDetailOrder.InventoryAccountId:
                            query = query.OrderBy(q => q.InventoryAccountId);
                            break;
                        case ItemDetailOrder.ReturnAccountId:
                            query = query.OrderBy(q => q.ReturnAccountId);
                            break;
                        case ItemDetailOrder.SalesAllowancesAccountId:
                            query = query.OrderBy(q => q.SalesAllowancesAccountId);
                            break;
                        case ItemDetailOrder.ExpenseAccountId:
                            query = query.OrderBy(q => q.ExpenseAccountId);
                            break;
                        case ItemDetailOrder.RevenueAccountId:
                            query = query.OrderBy(q => q.RevenueAccountId);
                            break;
                        case ItemDetailOrder.DiscountAccountId:
                            query = query.OrderBy(q => q.DiscountAccountId);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemDetailOrder.DefaultValue:
                            query = query.OrderByDescending(q => q.DefaultValue);
                            break;
                        case ItemDetailOrder.InventoryAccountId:
                            query = query.OrderByDescending(q => q.InventoryAccountId);
                            break;
                        case ItemDetailOrder.ReturnAccountId:
                            query = query.OrderByDescending(q => q.ReturnAccountId);
                            break;
                        case ItemDetailOrder.SalesAllowancesAccountId:
                            query = query.OrderByDescending(q => q.SalesAllowancesAccountId);
                            break;
                        case ItemDetailOrder.ExpenseAccountId:
                            query = query.OrderByDescending(q => q.ExpenseAccountId);
                            break;
                        case ItemDetailOrder.RevenueAccountId:
                            query = query.OrderByDescending(q => q.RevenueAccountId);
                            break;
                        case ItemDetailOrder.DiscountAccountId:
                            query = query.OrderByDescending(q => q.DiscountAccountId);
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

        private async Task<List<ItemDetail>> DynamicSelect(IQueryable<ItemDetailDAO> query, ItemDetailFilter filter)
        {
            List <ItemDetail> ItemDetails = await query.Select(q => new ItemDetail()
            {
                
                Id = filter.Selects.Contains(ItemDetailSelect.Id) ? q.Id : default(Guid),
                ItemId = filter.Selects.Contains(ItemDetailSelect.Item) ? q.ItemId : default(Guid),
                LegalEntityId = filter.Selects.Contains(ItemDetailSelect.LegalEntity) ? q.LegalEntityId : default(Guid),
                DefaultValue = filter.Selects.Contains(ItemDetailSelect.DefaultValue) ? q.DefaultValue : default(Guid?),
                InventoryAccountId = filter.Selects.Contains(ItemDetailSelect.InventoryAccount) ? q.InventoryAccountId : default(Guid?),
                ReturnAccountId = filter.Selects.Contains(ItemDetailSelect.ReturnAccount) ? q.ReturnAccountId : default(Guid?),
                SalesAllowancesAccountId = filter.Selects.Contains(ItemDetailSelect.SalesAllowancesAccount) ? q.SalesAllowancesAccountId : default(Guid?),
                ExpenseAccountId = filter.Selects.Contains(ItemDetailSelect.ExpenseAccount) ? q.ExpenseAccountId : default(Guid?),
                RevenueAccountId = filter.Selects.Contains(ItemDetailSelect.RevenueAccount) ? q.RevenueAccountId : default(Guid?),
                DiscountAccountId = filter.Selects.Contains(ItemDetailSelect.DiscountAccount) ? q.DiscountAccountId : default(Guid?),
                BusinessGroupId = filter.Selects.Contains(ItemDetailSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return ItemDetails;
        }

        public async Task<int> Count(ItemDetailFilter filter)
        {
            IQueryable <ItemDetailDAO> ItemDetailDAOs = ERPContext.ItemDetail;
            ItemDetailDAOs = DynamicFilter(ItemDetailDAOs, filter);
            return await ItemDetailDAOs.CountAsync();
        }

        public async Task<List<ItemDetail>> List(ItemDetailFilter filter)
        {
            if (filter == null) return new List<ItemDetail>();
            IQueryable<ItemDetailDAO> ItemDetailDAOs = ERPContext.ItemDetail;
            ItemDetailDAOs = DynamicFilter(ItemDetailDAOs, filter);
            ItemDetailDAOs = DynamicOrder(ItemDetailDAOs, filter);
            var ItemDetails = await DynamicSelect(ItemDetailDAOs, filter);
            return ItemDetails;
        }

        public async Task<ItemDetail> Get(Guid Id)
        {
            ItemDetail ItemDetail = await ERPContext.ItemDetail.Where(l => l.Id == Id).Select(ItemDetailDAO => new ItemDetail()
            {
                 
                Id = ItemDetailDAO.Id,
                ItemId = ItemDetailDAO.ItemId,
                LegalEntityId = ItemDetailDAO.LegalEntityId,
                DefaultValue = ItemDetailDAO.DefaultValue,
                InventoryAccountId = ItemDetailDAO.InventoryAccountId,
                ReturnAccountId = ItemDetailDAO.ReturnAccountId,
                SalesAllowancesAccountId = ItemDetailDAO.SalesAllowancesAccountId,
                ExpenseAccountId = ItemDetailDAO.ExpenseAccountId,
                RevenueAccountId = ItemDetailDAO.RevenueAccountId,
                DiscountAccountId = ItemDetailDAO.DiscountAccountId,
                BusinessGroupId = ItemDetailDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return ItemDetail;
        }

        public async Task<bool> Create(ItemDetail ItemDetail)
        {
            ItemDetailDAO ItemDetailDAO = new ItemDetailDAO();
            
            ItemDetailDAO.Id = ItemDetail.Id;
            ItemDetailDAO.ItemId = ItemDetail.ItemId;
            ItemDetailDAO.LegalEntityId = ItemDetail.LegalEntityId;
            ItemDetailDAO.DefaultValue = ItemDetail.DefaultValue;
            ItemDetailDAO.InventoryAccountId = ItemDetail.InventoryAccountId;
            ItemDetailDAO.ReturnAccountId = ItemDetail.ReturnAccountId;
            ItemDetailDAO.SalesAllowancesAccountId = ItemDetail.SalesAllowancesAccountId;
            ItemDetailDAO.ExpenseAccountId = ItemDetail.ExpenseAccountId;
            ItemDetailDAO.RevenueAccountId = ItemDetail.RevenueAccountId;
            ItemDetailDAO.DiscountAccountId = ItemDetail.DiscountAccountId;
            ItemDetailDAO.BusinessGroupId = ItemDetail.BusinessGroupId;
            ItemDetailDAO.Disabled = false;
            
            await ERPContext.ItemDetail.AddAsync(ItemDetailDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(ItemDetail ItemDetail)
        {
            ItemDetailDAO ItemDetailDAO = ERPContext.ItemDetail.Where(b => b.Id == ItemDetail.Id).FirstOrDefault();
            
            ItemDetailDAO.Id = ItemDetail.Id;
            ItemDetailDAO.ItemId = ItemDetail.ItemId;
            ItemDetailDAO.LegalEntityId = ItemDetail.LegalEntityId;
            ItemDetailDAO.DefaultValue = ItemDetail.DefaultValue;
            ItemDetailDAO.InventoryAccountId = ItemDetail.InventoryAccountId;
            ItemDetailDAO.ReturnAccountId = ItemDetail.ReturnAccountId;
            ItemDetailDAO.SalesAllowancesAccountId = ItemDetail.SalesAllowancesAccountId;
            ItemDetailDAO.ExpenseAccountId = ItemDetail.ExpenseAccountId;
            ItemDetailDAO.RevenueAccountId = ItemDetail.RevenueAccountId;
            ItemDetailDAO.DiscountAccountId = ItemDetail.DiscountAccountId;
            ItemDetailDAO.BusinessGroupId = ItemDetail.BusinessGroupId;
            ItemDetailDAO.Disabled = false;
            ERPContext.ItemDetail.Update(ItemDetailDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            ItemDetailDAO ItemDetailDAO = await ERPContext.ItemDetail.Where(x => x.Id == Id).FirstOrDefaultAsync();
            ItemDetailDAO.Disabled = true;
            ERPContext.ItemDetail.Update(ItemDetailDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
