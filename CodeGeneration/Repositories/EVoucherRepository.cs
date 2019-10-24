
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
    public interface IEVoucherRepository
    {
        Task<int> Count(EVoucherFilter EVoucherFilter);
        Task<List<EVoucher>> List(EVoucherFilter EVoucherFilter);
        Task<EVoucher> Get(long Id);
        Task<bool> Create(EVoucher EVoucher);
        Task<bool> Update(EVoucher EVoucher);
        Task<bool> Delete(EVoucher EVoucher);
        
    }
    public class EVoucherRepository : IEVoucherRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public EVoucherRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<EVoucherDAO> DynamicFilter(IQueryable<EVoucherDAO> query, EVoucherFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.CustomerId != null)
                query = query.Where(q => q.CustomerId, filter.CustomerId);
            if (filter.ProductId != null)
                query = query.Where(q => q.ProductId, filter.ProductId);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Start != null)
                query = query.Where(q => q.Start, filter.Start);
            if (filter.End != null)
                query = query.Where(q => q.End, filter.End);
            if (filter.Quantity != null)
                query = query.Where(q => q.Quantity, filter.Quantity);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<EVoucherDAO> DynamicOrder(IQueryable<EVoucherDAO> query,  EVoucherFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case EVoucherOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case EVoucherOrder.Customer:
                            query = query.OrderBy(q => q.Customer.Id);
                            break;
                        case EVoucherOrder.Product:
                            query = query.OrderBy(q => q.Product.Id);
                            break;
                        case EVoucherOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case EVoucherOrder.Start:
                            query = query.OrderBy(q => q.Start);
                            break;
                        case EVoucherOrder.End:
                            query = query.OrderBy(q => q.End);
                            break;
                        case EVoucherOrder.Quantity:
                            query = query.OrderBy(q => q.Quantity);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case EVoucherOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case EVoucherOrder.Customer:
                            query = query.OrderByDescending(q => q.Customer.Id);
                            break;
                        case EVoucherOrder.Product:
                            query = query.OrderByDescending(q => q.Product.Id);
                            break;
                        case EVoucherOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case EVoucherOrder.Start:
                            query = query.OrderByDescending(q => q.Start);
                            break;
                        case EVoucherOrder.End:
                            query = query.OrderByDescending(q => q.End);
                            break;
                        case EVoucherOrder.Quantity:
                            query = query.OrderByDescending(q => q.Quantity);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<EVoucher>> DynamicSelect(IQueryable<EVoucherDAO> query, EVoucherFilter filter)
        {
            List <EVoucher> EVouchers = await query.Select(q => new EVoucher()
            {
                
                Id = filter.Selects.Contains(EVoucherSelect.Id) ? q.Id : default(long),
                CustomerId = filter.Selects.Contains(EVoucherSelect.Customer) ? q.CustomerId : default(long),
                ProductId = filter.Selects.Contains(EVoucherSelect.Product) ? q.ProductId : default(long?),
                Name = filter.Selects.Contains(EVoucherSelect.Name) ? q.Name : default(string),
                Start = filter.Selects.Contains(EVoucherSelect.Start) ? q.Start : default(DateTime),
                End = filter.Selects.Contains(EVoucherSelect.End) ? q.End : default(DateTime),
                Quantity = filter.Selects.Contains(EVoucherSelect.Quantity) ? q.Quantity : default(long),
                Customer = filter.Selects.Contains(EVoucherSelect.Customer) && q.Customer != null ? new Customer
                {
                    
                    Id = q.Customer.Id,
                    Username = q.Customer.Username,
                    DisplayName = q.Customer.DisplayName,
                    PhoneNumber = q.Customer.PhoneNumber,
                    Email = q.Customer.Email,
                } : null,
                Product = filter.Selects.Contains(EVoucherSelect.Product) && q.Product != null ? new Product
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
            return EVouchers;
        }

        public async Task<int> Count(EVoucherFilter filter)
        {
            IQueryable <EVoucherDAO> EVoucherDAOs = DataContext.EVoucher;
            EVoucherDAOs = DynamicFilter(EVoucherDAOs, filter);
            return await EVoucherDAOs.CountAsync();
        }

        public async Task<List<EVoucher>> List(EVoucherFilter filter)
        {
            if (filter == null) return new List<EVoucher>();
            IQueryable<EVoucherDAO> EVoucherDAOs = DataContext.EVoucher;
            EVoucherDAOs = DynamicFilter(EVoucherDAOs, filter);
            EVoucherDAOs = DynamicOrder(EVoucherDAOs, filter);
            var EVouchers = await DynamicSelect(EVoucherDAOs, filter);
            return EVouchers;
        }

        
        public async Task<EVoucher> Get(long Id)
        {
            EVoucher EVoucher = await DataContext.EVoucher.Where(x => x.Id == Id).Select(EVoucherDAO => new EVoucher()
            {
                 
                Id = EVoucherDAO.Id,
                CustomerId = EVoucherDAO.CustomerId,
                ProductId = EVoucherDAO.ProductId,
                Name = EVoucherDAO.Name,
                Start = EVoucherDAO.Start,
                End = EVoucherDAO.End,
                Quantity = EVoucherDAO.Quantity,
                Customer = EVoucherDAO.Customer == null ? null : new Customer
                {
                    
                    Id = EVoucherDAO.Customer.Id,
                    Username = EVoucherDAO.Customer.Username,
                    DisplayName = EVoucherDAO.Customer.DisplayName,
                    PhoneNumber = EVoucherDAO.Customer.PhoneNumber,
                    Email = EVoucherDAO.Customer.Email,
                },
                Product = EVoucherDAO.Product == null ? null : new Product
                {
                    
                    Id = EVoucherDAO.Product.Id,
                    Code = EVoucherDAO.Product.Code,
                    Name = EVoucherDAO.Product.Name,
                    Description = EVoucherDAO.Product.Description,
                    TypeId = EVoucherDAO.Product.TypeId,
                    StatusId = EVoucherDAO.Product.StatusId,
                    MerchantId = EVoucherDAO.Product.MerchantId,
                    CategoryId = EVoucherDAO.Product.CategoryId,
                    BrandId = EVoucherDAO.Product.BrandId,
                    WarrantyPolicy = EVoucherDAO.Product.WarrantyPolicy,
                    ReturnPolicy = EVoucherDAO.Product.ReturnPolicy,
                    ExpiredDate = EVoucherDAO.Product.ExpiredDate,
                    ConditionOfUse = EVoucherDAO.Product.ConditionOfUse,
                    MaximumPurchaseQuantity = EVoucherDAO.Product.MaximumPurchaseQuantity,
                },
            }).FirstOrDefaultAsync();
            return EVoucher;
        }

        public async Task<bool> Create(EVoucher EVoucher)
        {
            EVoucherDAO EVoucherDAO = new EVoucherDAO();
            
            EVoucherDAO.Id = EVoucher.Id;
            EVoucherDAO.CustomerId = EVoucher.CustomerId;
            EVoucherDAO.ProductId = EVoucher.ProductId;
            EVoucherDAO.Name = EVoucher.Name;
            EVoucherDAO.Start = EVoucher.Start;
            EVoucherDAO.End = EVoucher.End;
            EVoucherDAO.Quantity = EVoucher.Quantity;
            
            await DataContext.EVoucher.AddAsync(EVoucherDAO);
            await DataContext.SaveChangesAsync();
            EVoucher.Id = EVoucherDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(EVoucher EVoucher)
        {
            EVoucherDAO EVoucherDAO = DataContext.EVoucher.Where(x => x.Id == EVoucher.Id).FirstOrDefault();
            
            EVoucherDAO.Id = EVoucher.Id;
            EVoucherDAO.CustomerId = EVoucher.CustomerId;
            EVoucherDAO.ProductId = EVoucher.ProductId;
            EVoucherDAO.Name = EVoucher.Name;
            EVoucherDAO.Start = EVoucher.Start;
            EVoucherDAO.End = EVoucher.End;
            EVoucherDAO.Quantity = EVoucher.Quantity;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(EVoucher EVoucher)
        {
            EVoucherDAO EVoucherDAO = await DataContext.EVoucher.Where(x => x.Id == EVoucher.Id).FirstOrDefaultAsync();
            DataContext.EVoucher.Remove(EVoucherDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
