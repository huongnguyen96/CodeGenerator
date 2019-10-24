
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
    public interface IItemRepository
    {
        Task<int> Count(ItemFilter ItemFilter);
        Task<List<Item>> List(ItemFilter ItemFilter);
        Task<Item> Get(long Id);
        Task<bool> Create(Item Item);
        Task<bool> Update(Item Item);
        Task<bool> Delete(Item Item);
        
    }
    public class ItemRepository : IItemRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public ItemRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ItemDAO> DynamicFilter(IQueryable<ItemDAO> query, ItemFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.ProductId != null)
                query = query.Where(q => q.ProductId, filter.ProductId);
            if (filter.FirstVariationId != null)
                query = query.Where(q => q.FirstVariationId, filter.FirstVariationId);
            if (filter.SecondVariationId != null)
                query = query.Where(q => q.SecondVariationId, filter.SecondVariationId);
            if (filter.SKU != null)
                query = query.Where(q => q.SKU, filter.SKU);
            if (filter.Price != null)
                query = query.Where(q => q.Price, filter.Price);
            if (filter.MinPrice != null)
                query = query.Where(q => q.MinPrice, filter.MinPrice);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<ItemDAO> DynamicOrder(IQueryable<ItemDAO> query,  ItemFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ItemOrder.Product:
                            query = query.OrderBy(q => q.Product.Id);
                            break;
                        case ItemOrder.FirstVariation:
                            query = query.OrderBy(q => q.FirstVariation.Id);
                            break;
                        case ItemOrder.SecondVariation:
                            query = query.OrderBy(q => q.SecondVariation.Id);
                            break;
                        case ItemOrder.SKU:
                            query = query.OrderBy(q => q.SKU);
                            break;
                        case ItemOrder.Price:
                            query = query.OrderBy(q => q.Price);
                            break;
                        case ItemOrder.MinPrice:
                            query = query.OrderBy(q => q.MinPrice);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ItemOrder.Product:
                            query = query.OrderByDescending(q => q.Product.Id);
                            break;
                        case ItemOrder.FirstVariation:
                            query = query.OrderByDescending(q => q.FirstVariation.Id);
                            break;
                        case ItemOrder.SecondVariation:
                            query = query.OrderByDescending(q => q.SecondVariation.Id);
                            break;
                        case ItemOrder.SKU:
                            query = query.OrderByDescending(q => q.SKU);
                            break;
                        case ItemOrder.Price:
                            query = query.OrderByDescending(q => q.Price);
                            break;
                        case ItemOrder.MinPrice:
                            query = query.OrderByDescending(q => q.MinPrice);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Item>> DynamicSelect(IQueryable<ItemDAO> query, ItemFilter filter)
        {
            List <Item> Items = await query.Select(q => new Item()
            {
                
                Id = filter.Selects.Contains(ItemSelect.Id) ? q.Id : default(long),
                ProductId = filter.Selects.Contains(ItemSelect.Product) ? q.ProductId : default(long),
                FirstVariationId = filter.Selects.Contains(ItemSelect.FirstVariation) ? q.FirstVariationId : default(long),
                SecondVariationId = filter.Selects.Contains(ItemSelect.SecondVariation) ? q.SecondVariationId : default(long?),
                SKU = filter.Selects.Contains(ItemSelect.SKU) ? q.SKU : default(string),
                Price = filter.Selects.Contains(ItemSelect.Price) ? q.Price : default(long),
                MinPrice = filter.Selects.Contains(ItemSelect.MinPrice) ? q.MinPrice : default(long),
                FirstVariation = filter.Selects.Contains(ItemSelect.FirstVariation) && q.FirstVariation != null ? new Variation
                {
                    
                    Id = q.FirstVariation.Id,
                    Name = q.FirstVariation.Name,
                    VariationGroupingId = q.FirstVariation.VariationGroupingId,
                } : null,
                Product = filter.Selects.Contains(ItemSelect.Product) && q.Product != null ? new Product
                {
                    
                    Id = q.Product.Id,
                    Code = q.Product.Code,
                    Name = q.Product.Name,
                    Description = q.Product.Description,
                    TypeId = q.Product.TypeId,
                    StatusId = q.Product.StatusId,
                    MerchantId = q.Product.MerchantId,
                    CategoryId = q.Product.CategoryId,
                    BrandId = q.Product.BrandId,
                    WarrantyPolicy = q.Product.WarrantyPolicy,
                    ReturnPolicy = q.Product.ReturnPolicy,
                    ExpiredDate = q.Product.ExpiredDate,
                    ConditionOfUse = q.Product.ConditionOfUse,
                    MaximumPurchaseQuantity = q.Product.MaximumPurchaseQuantity,
                } : null,
                SecondVariation = filter.Selects.Contains(ItemSelect.SecondVariation) && q.SecondVariation != null ? new Variation
                {
                    
                    Id = q.SecondVariation.Id,
                    Name = q.SecondVariation.Name,
                    VariationGroupingId = q.SecondVariation.VariationGroupingId,
                } : null,
            }).ToListAsync();
            return Items;
        }

        public async Task<int> Count(ItemFilter filter)
        {
            IQueryable <ItemDAO> ItemDAOs = DataContext.Item;
            ItemDAOs = DynamicFilter(ItemDAOs, filter);
            return await ItemDAOs.CountAsync();
        }

        public async Task<List<Item>> List(ItemFilter filter)
        {
            if (filter == null) return new List<Item>();
            IQueryable<ItemDAO> ItemDAOs = DataContext.Item;
            ItemDAOs = DynamicFilter(ItemDAOs, filter);
            ItemDAOs = DynamicOrder(ItemDAOs, filter);
            var Items = await DynamicSelect(ItemDAOs, filter);
            return Items;
        }

        
        public async Task<Item> Get(long Id)
        {
            Item Item = await DataContext.Item.Where(x => x.Id == Id).Select(ItemDAO => new Item()
            {
                 
                Id = ItemDAO.Id,
                ProductId = ItemDAO.ProductId,
                FirstVariationId = ItemDAO.FirstVariationId,
                SecondVariationId = ItemDAO.SecondVariationId,
                SKU = ItemDAO.SKU,
                Price = ItemDAO.Price,
                MinPrice = ItemDAO.MinPrice,
                FirstVariation = ItemDAO.FirstVariation == null ? null : new Variation
                {
                    
                    Id = ItemDAO.FirstVariation.Id,
                    Name = ItemDAO.FirstVariation.Name,
                    VariationGroupingId = ItemDAO.FirstVariation.VariationGroupingId,
                },
                Product = ItemDAO.Product == null ? null : new Product
                {
                    
                    Id = ItemDAO.Product.Id,
                    Code = ItemDAO.Product.Code,
                    Name = ItemDAO.Product.Name,
                    Description = ItemDAO.Product.Description,
                    TypeId = ItemDAO.Product.TypeId,
                    StatusId = ItemDAO.Product.StatusId,
                    MerchantId = ItemDAO.Product.MerchantId,
                    CategoryId = ItemDAO.Product.CategoryId,
                    BrandId = ItemDAO.Product.BrandId,
                    WarrantyPolicy = ItemDAO.Product.WarrantyPolicy,
                    ReturnPolicy = ItemDAO.Product.ReturnPolicy,
                    ExpiredDate = ItemDAO.Product.ExpiredDate,
                    ConditionOfUse = ItemDAO.Product.ConditionOfUse,
                    MaximumPurchaseQuantity = ItemDAO.Product.MaximumPurchaseQuantity,
                },
                SecondVariation = ItemDAO.SecondVariation == null ? null : new Variation
                {
                    
                    Id = ItemDAO.SecondVariation.Id,
                    Name = ItemDAO.SecondVariation.Name,
                    VariationGroupingId = ItemDAO.SecondVariation.VariationGroupingId,
                },
            }).FirstOrDefaultAsync();
            return Item;
        }

        public async Task<bool> Create(Item Item)
        {
            ItemDAO ItemDAO = new ItemDAO();
            
            ItemDAO.Id = Item.Id;
            ItemDAO.ProductId = Item.ProductId;
            ItemDAO.FirstVariationId = Item.FirstVariationId;
            ItemDAO.SecondVariationId = Item.SecondVariationId;
            ItemDAO.SKU = Item.SKU;
            ItemDAO.Price = Item.Price;
            ItemDAO.MinPrice = Item.MinPrice;
            
            await DataContext.Item.AddAsync(ItemDAO);
            await DataContext.SaveChangesAsync();
            Item.Id = ItemDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(Item Item)
        {
            ItemDAO ItemDAO = DataContext.Item.Where(x => x.Id == Item.Id).FirstOrDefault();
            
            ItemDAO.Id = Item.Id;
            ItemDAO.ProductId = Item.ProductId;
            ItemDAO.FirstVariationId = Item.FirstVariationId;
            ItemDAO.SecondVariationId = Item.SecondVariationId;
            ItemDAO.SKU = Item.SKU;
            ItemDAO.Price = Item.Price;
            ItemDAO.MinPrice = Item.MinPrice;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Item Item)
        {
            ItemDAO ItemDAO = await DataContext.Item.Where(x => x.Id == Item.Id).FirstOrDefaultAsync();
            DataContext.Item.Remove(ItemDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
