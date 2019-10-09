
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
    public interface IItemRepository
    {
        Task<int> Count(ItemFilter ItemFilter);
        Task<List<Item>> List(ItemFilter ItemFilter);
        Task<Item> Get(Guid Id);
        Task<bool> Create(Item Item);
        Task<bool> Update(Item Item);
        Task<bool> Delete(Guid Id);
        
    }
    public class ItemRepository : IItemRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public ItemRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ItemDAO> DynamicFilter(IQueryable<ItemDAO> query, ItemFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.CodeFromSupplier != null)
                query = query.Where(q => q.CodeFromSupplier, filter.CodeFromSupplier);
            if (filter.CodeFromMarket != null)
                query = query.Where(q => q.CodeFromMarket, filter.CodeFromMarket);
            if (filter.CharacteristicId != null)
                query = query.Where(q => q.CharacteristicId, filter.CharacteristicId);
            if (filter.UnitOfMeasureId != null)
                query = query.Where(q => q.UnitOfMeasureId, filter.UnitOfMeasureId);
            if (filter.Weight.HasValue)
                query = query.Where(q => q.Weight.HasValue && q.Weight.Value == filter.Weight.Value);
            if (filter.Weight != null)
                query = query.Where(q => q.Weight, filter.Weight);
            if (filter.Height.HasValue)
                query = query.Where(q => q.Height.HasValue && q.Height.Value == filter.Height.Value);
            if (filter.Height != null)
                query = query.Where(q => q.Height, filter.Height);
            if (filter.Length.HasValue)
                query = query.Where(q => q.Length.HasValue && q.Length.Value == filter.Length.Value);
            if (filter.Length != null)
                query = query.Where(q => q.Length, filter.Length);
            if (filter.Width.HasValue)
                query = query.Where(q => q.Width.HasValue && q.Width.Value == filter.Width.Value);
            if (filter.Width != null)
                query = query.Where(q => q.Width, filter.Width);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.PrimaryPrice != null)
                query = query.Where(q => q.PrimaryPrice, filter.PrimaryPrice);
            if (filter.VATId.HasValue)
                query = query.Where(q => q.VATId.HasValue && q.VATId.Value == filter.VATId.Value);
            if (filter.VATId != null)
                query = query.Where(q => q.VATId, filter.VATId);
            if (filter.ImportTaxId.HasValue)
                query = query.Where(q => q.ImportTaxId.HasValue && q.ImportTaxId.Value == filter.ImportTaxId.Value);
            if (filter.ImportTaxId != null)
                query = query.Where(q => q.ImportTaxId, filter.ImportTaxId);
            if (filter.ExportTaxId.HasValue)
                query = query.Where(q => q.ExportTaxId.HasValue && q.ExportTaxId.Value == filter.ExportTaxId.Value);
            if (filter.ExportTaxId != null)
                query = query.Where(q => q.ExportTaxId, filter.ExportTaxId);
            if (filter.NaturalResourceTaxId.HasValue)
                query = query.Where(q => q.NaturalResourceTaxId.HasValue && q.NaturalResourceTaxId.Value == filter.NaturalResourceTaxId.Value);
            if (filter.NaturalResourceTaxId != null)
                query = query.Where(q => q.NaturalResourceTaxId, filter.NaturalResourceTaxId);
            if (filter.EnvironmentTaxId.HasValue)
                query = query.Where(q => q.EnvironmentTaxId.HasValue && q.EnvironmentTaxId.Value == filter.EnvironmentTaxId.Value);
            if (filter.EnvironmentTaxId != null)
                query = query.Where(q => q.EnvironmentTaxId, filter.EnvironmentTaxId);
            if (filter.SpecialConsumptionTaxId.HasValue)
                query = query.Where(q => q.SpecialConsumptionTaxId.HasValue && q.SpecialConsumptionTaxId.Value == filter.SpecialConsumptionTaxId.Value);
            if (filter.SpecialConsumptionTaxId != null)
                query = query.Where(q => q.SpecialConsumptionTaxId, filter.SpecialConsumptionTaxId);
            return query;
        }
        private IQueryable<ItemDAO> DynamicOrder(IQueryable<ItemDAO> query,  ItemFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ItemOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case ItemOrder.CodeFromSupplier:
                            query = query.OrderBy(q => q.CodeFromSupplier);
                            break;
                        case ItemOrder.CodeFromMarket:
                            query = query.OrderBy(q => q.CodeFromMarket);
                            break;
                        case ItemOrder.Weight:
                            query = query.OrderBy(q => q.Weight);
                            break;
                        case ItemOrder.Height:
                            query = query.OrderBy(q => q.Height);
                            break;
                        case ItemOrder.Length:
                            query = query.OrderBy(q => q.Length);
                            break;
                        case ItemOrder.Width:
                            query = query.OrderBy(q => q.Width);
                            break;
                        case ItemOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case ItemOrder.PrimaryPrice:
                            query = query.OrderBy(q => q.PrimaryPrice);
                            break;
                        case ItemOrder.VATId:
                            query = query.OrderBy(q => q.VATId);
                            break;
                        case ItemOrder.ImportTaxId:
                            query = query.OrderBy(q => q.ImportTaxId);
                            break;
                        case ItemOrder.ExportTaxId:
                            query = query.OrderBy(q => q.ExportTaxId);
                            break;
                        case ItemOrder.NaturalResourceTaxId:
                            query = query.OrderBy(q => q.NaturalResourceTaxId);
                            break;
                        case ItemOrder.EnvironmentTaxId:
                            query = query.OrderBy(q => q.EnvironmentTaxId);
                            break;
                        case ItemOrder.SpecialConsumptionTaxId:
                            query = query.OrderBy(q => q.SpecialConsumptionTaxId);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ItemOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case ItemOrder.CodeFromSupplier:
                            query = query.OrderByDescending(q => q.CodeFromSupplier);
                            break;
                        case ItemOrder.CodeFromMarket:
                            query = query.OrderByDescending(q => q.CodeFromMarket);
                            break;
                        case ItemOrder.Weight:
                            query = query.OrderByDescending(q => q.Weight);
                            break;
                        case ItemOrder.Height:
                            query = query.OrderByDescending(q => q.Height);
                            break;
                        case ItemOrder.Length:
                            query = query.OrderByDescending(q => q.Length);
                            break;
                        case ItemOrder.Width:
                            query = query.OrderByDescending(q => q.Width);
                            break;
                        case ItemOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case ItemOrder.PrimaryPrice:
                            query = query.OrderByDescending(q => q.PrimaryPrice);
                            break;
                        case ItemOrder.VATId:
                            query = query.OrderByDescending(q => q.VATId);
                            break;
                        case ItemOrder.ImportTaxId:
                            query = query.OrderByDescending(q => q.ImportTaxId);
                            break;
                        case ItemOrder.ExportTaxId:
                            query = query.OrderByDescending(q => q.ExportTaxId);
                            break;
                        case ItemOrder.NaturalResourceTaxId:
                            query = query.OrderByDescending(q => q.NaturalResourceTaxId);
                            break;
                        case ItemOrder.EnvironmentTaxId:
                            query = query.OrderByDescending(q => q.EnvironmentTaxId);
                            break;
                        case ItemOrder.SpecialConsumptionTaxId:
                            query = query.OrderByDescending(q => q.SpecialConsumptionTaxId);
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

        private async Task<List<Item>> DynamicSelect(IQueryable<ItemDAO> query, ItemFilter filter)
        {
            List <Item> Items = await query.Select(q => new Item()
            {
                
                Id = filter.Selects.Contains(ItemSelect.Id) ? q.Id : default(Guid),
                Code = filter.Selects.Contains(ItemSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ItemSelect.Name) ? q.Name : default(string),
                BusinessGroupId = filter.Selects.Contains(ItemSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                CodeFromSupplier = filter.Selects.Contains(ItemSelect.CodeFromSupplier) ? q.CodeFromSupplier : default(string),
                CodeFromMarket = filter.Selects.Contains(ItemSelect.CodeFromMarket) ? q.CodeFromMarket : default(string),
                CharacteristicId = filter.Selects.Contains(ItemSelect.Characteristic) ? q.CharacteristicId : default(Guid),
                UnitOfMeasureId = filter.Selects.Contains(ItemSelect.UnitOfMeasure) ? q.UnitOfMeasureId : default(Guid),
                Weight = filter.Selects.Contains(ItemSelect.Weight) ? q.Weight : default(Guid?),
                Height = filter.Selects.Contains(ItemSelect.Height) ? q.Height : default(Guid?),
                Length = filter.Selects.Contains(ItemSelect.Length) ? q.Length : default(Guid?),
                Width = filter.Selects.Contains(ItemSelect.Width) ? q.Width : default(Guid?),
                StatusId = filter.Selects.Contains(ItemSelect.Status) ? q.StatusId : default(Guid),
                Description = filter.Selects.Contains(ItemSelect.Description) ? q.Description : default(string),
                PrimaryPrice = filter.Selects.Contains(ItemSelect.PrimaryPrice) ? q.PrimaryPrice : default(double),
                VATId = filter.Selects.Contains(ItemSelect.VAT) ? q.VATId : default(Guid?),
                ImportTaxId = filter.Selects.Contains(ItemSelect.ImportTax) ? q.ImportTaxId : default(Guid?),
                ExportTaxId = filter.Selects.Contains(ItemSelect.ExportTax) ? q.ExportTaxId : default(Guid?),
                NaturalResourceTaxId = filter.Selects.Contains(ItemSelect.NaturalResourceTax) ? q.NaturalResourceTaxId : default(Guid?),
                EnvironmentTaxId = filter.Selects.Contains(ItemSelect.EnvironmentTax) ? q.EnvironmentTaxId : default(Guid?),
                SpecialConsumptionTaxId = filter.Selects.Contains(ItemSelect.SpecialConsumptionTax) ? q.SpecialConsumptionTaxId : default(Guid?),
            }).ToListAsync();
            return Items;
        }

        public async Task<int> Count(ItemFilter filter)
        {
            IQueryable <ItemDAO> ItemDAOs = ERPContext.Item;
            ItemDAOs = DynamicFilter(ItemDAOs, filter);
            return await ItemDAOs.CountAsync();
        }

        public async Task<List<Item>> List(ItemFilter filter)
        {
            if (filter == null) return new List<Item>();
            IQueryable<ItemDAO> ItemDAOs = ERPContext.Item;
            ItemDAOs = DynamicFilter(ItemDAOs, filter);
            ItemDAOs = DynamicOrder(ItemDAOs, filter);
            var Items = await DynamicSelect(ItemDAOs, filter);
            return Items;
        }

        public async Task<Item> Get(Guid Id)
        {
            Item Item = await ERPContext.Item.Where(l => l.Id == Id).Select(ItemDAO => new Item()
            {
                 
                Id = ItemDAO.Id,
                Code = ItemDAO.Code,
                Name = ItemDAO.Name,
                BusinessGroupId = ItemDAO.BusinessGroupId,
                CodeFromSupplier = ItemDAO.CodeFromSupplier,
                CodeFromMarket = ItemDAO.CodeFromMarket,
                CharacteristicId = ItemDAO.CharacteristicId,
                UnitOfMeasureId = ItemDAO.UnitOfMeasureId,
                Weight = ItemDAO.Weight,
                Height = ItemDAO.Height,
                Length = ItemDAO.Length,
                Width = ItemDAO.Width,
                StatusId = ItemDAO.StatusId,
                Description = ItemDAO.Description,
                PrimaryPrice = ItemDAO.PrimaryPrice,
                VATId = ItemDAO.VATId,
                ImportTaxId = ItemDAO.ImportTaxId,
                ExportTaxId = ItemDAO.ExportTaxId,
                NaturalResourceTaxId = ItemDAO.NaturalResourceTaxId,
                EnvironmentTaxId = ItemDAO.EnvironmentTaxId,
                SpecialConsumptionTaxId = ItemDAO.SpecialConsumptionTaxId,
            }).FirstOrDefaultAsync();
            return Item;
        }

        public async Task<bool> Create(Item Item)
        {
            ItemDAO ItemDAO = new ItemDAO();
            
            ItemDAO.Id = Item.Id;
            ItemDAO.Code = Item.Code;
            ItemDAO.Name = Item.Name;
            ItemDAO.BusinessGroupId = Item.BusinessGroupId;
            ItemDAO.CodeFromSupplier = Item.CodeFromSupplier;
            ItemDAO.CodeFromMarket = Item.CodeFromMarket;
            ItemDAO.CharacteristicId = Item.CharacteristicId;
            ItemDAO.UnitOfMeasureId = Item.UnitOfMeasureId;
            ItemDAO.Weight = Item.Weight;
            ItemDAO.Height = Item.Height;
            ItemDAO.Length = Item.Length;
            ItemDAO.Width = Item.Width;
            ItemDAO.StatusId = Item.StatusId;
            ItemDAO.Description = Item.Description;
            ItemDAO.PrimaryPrice = Item.PrimaryPrice;
            ItemDAO.VATId = Item.VATId;
            ItemDAO.ImportTaxId = Item.ImportTaxId;
            ItemDAO.ExportTaxId = Item.ExportTaxId;
            ItemDAO.NaturalResourceTaxId = Item.NaturalResourceTaxId;
            ItemDAO.EnvironmentTaxId = Item.EnvironmentTaxId;
            ItemDAO.SpecialConsumptionTaxId = Item.SpecialConsumptionTaxId;
            ItemDAO.Disabled = false;
            
            await ERPContext.Item.AddAsync(ItemDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Item Item)
        {
            ItemDAO ItemDAO = ERPContext.Item.Where(b => b.Id == Item.Id).FirstOrDefault();
            
            ItemDAO.Id = Item.Id;
            ItemDAO.Code = Item.Code;
            ItemDAO.Name = Item.Name;
            ItemDAO.BusinessGroupId = Item.BusinessGroupId;
            ItemDAO.CodeFromSupplier = Item.CodeFromSupplier;
            ItemDAO.CodeFromMarket = Item.CodeFromMarket;
            ItemDAO.CharacteristicId = Item.CharacteristicId;
            ItemDAO.UnitOfMeasureId = Item.UnitOfMeasureId;
            ItemDAO.Weight = Item.Weight;
            ItemDAO.Height = Item.Height;
            ItemDAO.Length = Item.Length;
            ItemDAO.Width = Item.Width;
            ItemDAO.StatusId = Item.StatusId;
            ItemDAO.Description = Item.Description;
            ItemDAO.PrimaryPrice = Item.PrimaryPrice;
            ItemDAO.VATId = Item.VATId;
            ItemDAO.ImportTaxId = Item.ImportTaxId;
            ItemDAO.ExportTaxId = Item.ExportTaxId;
            ItemDAO.NaturalResourceTaxId = Item.NaturalResourceTaxId;
            ItemDAO.EnvironmentTaxId = Item.EnvironmentTaxId;
            ItemDAO.SpecialConsumptionTaxId = Item.SpecialConsumptionTaxId;
            ItemDAO.Disabled = false;
            ERPContext.Item.Update(ItemDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            ItemDAO ItemDAO = await ERPContext.Item.Where(x => x.Id == Id).FirstOrDefaultAsync();
            ItemDAO.Disabled = true;
            ERPContext.Item.Update(ItemDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
