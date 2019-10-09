
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
    public interface IItemMaterialRepository
    {
        Task<int> Count(ItemMaterialFilter ItemMaterialFilter);
        Task<List<ItemMaterial>> List(ItemMaterialFilter ItemMaterialFilter);
        Task<ItemMaterial> Get(Guid Id);
        Task<bool> Create(ItemMaterial ItemMaterial);
        Task<bool> Update(ItemMaterial ItemMaterial);
        Task<bool> Delete(Guid Id);
        
    }
    public class ItemMaterialRepository : IItemMaterialRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public ItemMaterialRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ItemMaterialDAO> DynamicFilter(IQueryable<ItemMaterialDAO> query, ItemMaterialFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.SourceItemId != null)
                query = query.Where(q => q.SourceItemId, filter.SourceItemId);
            if (filter.UnitOfMeasureId != null)
                query = query.Where(q => q.UnitOfMeasureId, filter.UnitOfMeasureId);
            if (filter.Quantity.HasValue)
                query = query.Where(q => q.Quantity.HasValue && q.Quantity.Value == filter.Quantity.Value);
            if (filter.Quantity != null)
                query = query.Where(q => q.Quantity, filter.Quantity);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.ItemDetailId != null)
                query = query.Where(q => q.ItemDetailId, filter.ItemDetailId);
            return query;
        }
        private IQueryable<ItemMaterialDAO> DynamicOrder(IQueryable<ItemMaterialDAO> query,  ItemMaterialFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemMaterialOrder.Quantity:
                            query = query.OrderBy(q => q.Quantity);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemMaterialOrder.Quantity:
                            query = query.OrderByDescending(q => q.Quantity);
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

        private async Task<List<ItemMaterial>> DynamicSelect(IQueryable<ItemMaterialDAO> query, ItemMaterialFilter filter)
        {
            List <ItemMaterial> ItemMaterials = await query.Select(q => new ItemMaterial()
            {
                
                Id = filter.Selects.Contains(ItemMaterialSelect.Id) ? q.Id : default(Guid),
                SourceItemId = filter.Selects.Contains(ItemMaterialSelect.SourceItem) ? q.SourceItemId : default(Guid),
                UnitOfMeasureId = filter.Selects.Contains(ItemMaterialSelect.UnitOfMeasure) ? q.UnitOfMeasureId : default(Guid),
                Quantity = filter.Selects.Contains(ItemMaterialSelect.Quantity) ? q.Quantity : default(Guid?),
                BusinessGroupId = filter.Selects.Contains(ItemMaterialSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                ItemDetailId = filter.Selects.Contains(ItemMaterialSelect.ItemDetail) ? q.ItemDetailId : default(Guid),
            }).ToListAsync();
            return ItemMaterials;
        }

        public async Task<int> Count(ItemMaterialFilter filter)
        {
            IQueryable <ItemMaterialDAO> ItemMaterialDAOs = ERPContext.ItemMaterial;
            ItemMaterialDAOs = DynamicFilter(ItemMaterialDAOs, filter);
            return await ItemMaterialDAOs.CountAsync();
        }

        public async Task<List<ItemMaterial>> List(ItemMaterialFilter filter)
        {
            if (filter == null) return new List<ItemMaterial>();
            IQueryable<ItemMaterialDAO> ItemMaterialDAOs = ERPContext.ItemMaterial;
            ItemMaterialDAOs = DynamicFilter(ItemMaterialDAOs, filter);
            ItemMaterialDAOs = DynamicOrder(ItemMaterialDAOs, filter);
            var ItemMaterials = await DynamicSelect(ItemMaterialDAOs, filter);
            return ItemMaterials;
        }

        public async Task<ItemMaterial> Get(Guid Id)
        {
            ItemMaterial ItemMaterial = await ERPContext.ItemMaterial.Where(l => l.Id == Id).Select(ItemMaterialDAO => new ItemMaterial()
            {
                 
                Id = ItemMaterialDAO.Id,
                SourceItemId = ItemMaterialDAO.SourceItemId,
                UnitOfMeasureId = ItemMaterialDAO.UnitOfMeasureId,
                Quantity = ItemMaterialDAO.Quantity,
                BusinessGroupId = ItemMaterialDAO.BusinessGroupId,
                ItemDetailId = ItemMaterialDAO.ItemDetailId,
            }).FirstOrDefaultAsync();
            return ItemMaterial;
        }

        public async Task<bool> Create(ItemMaterial ItemMaterial)
        {
            ItemMaterialDAO ItemMaterialDAO = new ItemMaterialDAO();
            
            ItemMaterialDAO.Id = ItemMaterial.Id;
            ItemMaterialDAO.SourceItemId = ItemMaterial.SourceItemId;
            ItemMaterialDAO.UnitOfMeasureId = ItemMaterial.UnitOfMeasureId;
            ItemMaterialDAO.Quantity = ItemMaterial.Quantity;
            ItemMaterialDAO.BusinessGroupId = ItemMaterial.BusinessGroupId;
            ItemMaterialDAO.ItemDetailId = ItemMaterial.ItemDetailId;
            ItemMaterialDAO.Disabled = false;
            
            await ERPContext.ItemMaterial.AddAsync(ItemMaterialDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(ItemMaterial ItemMaterial)
        {
            ItemMaterialDAO ItemMaterialDAO = ERPContext.ItemMaterial.Where(b => b.Id == ItemMaterial.Id).FirstOrDefault();
            
            ItemMaterialDAO.Id = ItemMaterial.Id;
            ItemMaterialDAO.SourceItemId = ItemMaterial.SourceItemId;
            ItemMaterialDAO.UnitOfMeasureId = ItemMaterial.UnitOfMeasureId;
            ItemMaterialDAO.Quantity = ItemMaterial.Quantity;
            ItemMaterialDAO.BusinessGroupId = ItemMaterial.BusinessGroupId;
            ItemMaterialDAO.ItemDetailId = ItemMaterial.ItemDetailId;
            ItemMaterialDAO.Disabled = false;
            ERPContext.ItemMaterial.Update(ItemMaterialDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            ItemMaterialDAO ItemMaterialDAO = await ERPContext.ItemMaterial.Where(x => x.Id == Id).FirstOrDefaultAsync();
            ItemMaterialDAO.Disabled = true;
            ERPContext.ItemMaterial.Update(ItemMaterialDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
