
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
            if (filter.ProductId != null)
                query = query.Where(q => q.ProductId, filter.ProductId);
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
                        case VariationGroupingOrder.Product:
                            query = query.OrderBy(q => q.Product.Id);
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
                        case VariationGroupingOrder.Product:
                            query = query.OrderByDescending(q => q.Product.Id);
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
                ProductId = filter.Selects.Contains(VariationGroupingSelect.Product) ? q.ProductId : default(long),
                Product = filter.Selects.Contains(VariationGroupingSelect.Product) && q.Product != null ? new Product
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
                ProductId = VariationGroupingDAO.ProductId,
                Product = VariationGroupingDAO.Product == null ? null : new Product
                {
                    
                    Id = VariationGroupingDAO.Product.Id,
                    Code = VariationGroupingDAO.Product.Code,
                    Name = VariationGroupingDAO.Product.Name,
                    Description = VariationGroupingDAO.Product.Description,
                    TypeId = VariationGroupingDAO.Product.TypeId,
                    StatusId = VariationGroupingDAO.Product.StatusId,
                    MerchantId = VariationGroupingDAO.Product.MerchantId,
                    CategoryId = VariationGroupingDAO.Product.CategoryId,
                    BrandId = VariationGroupingDAO.Product.BrandId,
                    WarrantyPolicy = VariationGroupingDAO.Product.WarrantyPolicy,
                    ReturnPolicy = VariationGroupingDAO.Product.ReturnPolicy,
                    ExpiredDate = VariationGroupingDAO.Product.ExpiredDate,
                    ConditionOfUse = VariationGroupingDAO.Product.ConditionOfUse,
                    MaximumPurchaseQuantity = VariationGroupingDAO.Product.MaximumPurchaseQuantity,
                },
            }).FirstOrDefaultAsync();
            return VariationGrouping;
        }

        public async Task<bool> Create(VariationGrouping VariationGrouping)
        {
            VariationGroupingDAO VariationGroupingDAO = new VariationGroupingDAO();
            
            VariationGroupingDAO.Id = VariationGrouping.Id;
            VariationGroupingDAO.Name = VariationGrouping.Name;
            VariationGroupingDAO.ProductId = VariationGrouping.ProductId;
            
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
            VariationGroupingDAO.ProductId = VariationGrouping.ProductId;
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
