
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
    public interface IProduct_MerchantAddressRepository
    {
        Task<int> Count(Product_MerchantAddressFilter Product_MerchantAddressFilter);
        Task<List<Product_MerchantAddress>> List(Product_MerchantAddressFilter Product_MerchantAddressFilter);
        Task<Product_MerchantAddress> Get(long ProductId, long MerchantAddressId);
        Task<bool> Create(Product_MerchantAddress Product_MerchantAddress);
        Task<bool> Update(Product_MerchantAddress Product_MerchantAddress);
        Task<bool> Delete(Product_MerchantAddress Product_MerchantAddress);
        
    }
    public class Product_MerchantAddressRepository : IProduct_MerchantAddressRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public Product_MerchantAddressRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<Product_MerchantAddressDAO> DynamicFilter(IQueryable<Product_MerchantAddressDAO> query, Product_MerchantAddressFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.ProductId != null)
                query = query.Where(q => q.ProductId, filter.ProductId);
            if (filter.MerchantAddressId != null)
                query = query.Where(q => q.MerchantAddressId, filter.MerchantAddressId);
            return query;
        }
        private IQueryable<Product_MerchantAddressDAO> DynamicOrder(IQueryable<Product_MerchantAddressDAO> query,  Product_MerchantAddressFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case Product_MerchantAddressOrder.Product:
                            query = query.OrderBy(q => q.Product.Id);
                            break;
                        case Product_MerchantAddressOrder.MerchantAddress:
                            query = query.OrderBy(q => q.MerchantAddress.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case Product_MerchantAddressOrder.Product:
                            query = query.OrderByDescending(q => q.Product.Id);
                            break;
                        case Product_MerchantAddressOrder.MerchantAddress:
                            query = query.OrderByDescending(q => q.MerchantAddress.Id);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Product_MerchantAddress>> DynamicSelect(IQueryable<Product_MerchantAddressDAO> query, Product_MerchantAddressFilter filter)
        {
            List <Product_MerchantAddress> Product_MerchantAddresss = await query.Select(q => new Product_MerchantAddress()
            {
                
                ProductId = filter.Selects.Contains(Product_MerchantAddressSelect.Product) ? q.ProductId : default(long),
                MerchantAddressId = filter.Selects.Contains(Product_MerchantAddressSelect.MerchantAddress) ? q.MerchantAddressId : default(long),
                MerchantAddress = filter.Selects.Contains(Product_MerchantAddressSelect.MerchantAddress) && q.MerchantAddress != null ? new MerchantAddress
                {
                    
                    Id = q.MerchantAddress.Id,
                    MerchantId = q.MerchantAddress.MerchantId,
                    Code = q.MerchantAddress.Code,
                    Address = q.MerchantAddress.Address,
                    Contact = q.MerchantAddress.Contact,
                    Phone = q.MerchantAddress.Phone,
                } : null,
                Product = filter.Selects.Contains(Product_MerchantAddressSelect.Product) && q.Product != null ? new Product
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
            return Product_MerchantAddresss;
        }

        public async Task<int> Count(Product_MerchantAddressFilter filter)
        {
            IQueryable <Product_MerchantAddressDAO> Product_MerchantAddressDAOs = DataContext.Product_MerchantAddress;
            Product_MerchantAddressDAOs = DynamicFilter(Product_MerchantAddressDAOs, filter);
            return await Product_MerchantAddressDAOs.CountAsync();
        }

        public async Task<List<Product_MerchantAddress>> List(Product_MerchantAddressFilter filter)
        {
            if (filter == null) return new List<Product_MerchantAddress>();
            IQueryable<Product_MerchantAddressDAO> Product_MerchantAddressDAOs = DataContext.Product_MerchantAddress;
            Product_MerchantAddressDAOs = DynamicFilter(Product_MerchantAddressDAOs, filter);
            Product_MerchantAddressDAOs = DynamicOrder(Product_MerchantAddressDAOs, filter);
            var Product_MerchantAddresss = await DynamicSelect(Product_MerchantAddressDAOs, filter);
            return Product_MerchantAddresss;
        }

        
        public async Task<Product_MerchantAddress> Get(long ProductId, long MerchantAddressId)
        {
            Product_MerchantAddress Product_MerchantAddress = await DataContext.Product_MerchantAddress.Where(x => x.ProductId == ProductId && x.MerchantAddressId == MerchantAddressId).Select(Product_MerchantAddressDAO => new Product_MerchantAddress()
            {
                 
                ProductId = Product_MerchantAddressDAO.ProductId,
                MerchantAddressId = Product_MerchantAddressDAO.MerchantAddressId,
                MerchantAddress = Product_MerchantAddressDAO.MerchantAddress == null ? null : new MerchantAddress
                {
                    
                    Id = Product_MerchantAddressDAO.MerchantAddress.Id,
                    MerchantId = Product_MerchantAddressDAO.MerchantAddress.MerchantId,
                    Code = Product_MerchantAddressDAO.MerchantAddress.Code,
                    Address = Product_MerchantAddressDAO.MerchantAddress.Address,
                    Contact = Product_MerchantAddressDAO.MerchantAddress.Contact,
                    Phone = Product_MerchantAddressDAO.MerchantAddress.Phone,
                },
                Product = Product_MerchantAddressDAO.Product == null ? null : new Product
                {
                    
                    Id = Product_MerchantAddressDAO.Product.Id,
                    Code = Product_MerchantAddressDAO.Product.Code,
                    Name = Product_MerchantAddressDAO.Product.Name,
                    Description = Product_MerchantAddressDAO.Product.Description,
                    TypeId = Product_MerchantAddressDAO.Product.TypeId,
                    StatusId = Product_MerchantAddressDAO.Product.StatusId,
                    MerchantId = Product_MerchantAddressDAO.Product.MerchantId,
                    CategoryId = Product_MerchantAddressDAO.Product.CategoryId,
                    BrandId = Product_MerchantAddressDAO.Product.BrandId,
                    WarrantyPolicy = Product_MerchantAddressDAO.Product.WarrantyPolicy,
                    ReturnPolicy = Product_MerchantAddressDAO.Product.ReturnPolicy,
                    ExpiredDate = Product_MerchantAddressDAO.Product.ExpiredDate,
                    ConditionOfUse = Product_MerchantAddressDAO.Product.ConditionOfUse,
                    MaximumPurchaseQuantity = Product_MerchantAddressDAO.Product.MaximumPurchaseQuantity,
                },
            }).FirstOrDefaultAsync();
            return Product_MerchantAddress;
        }

        public async Task<bool> Create(Product_MerchantAddress Product_MerchantAddress)
        {
            Product_MerchantAddressDAO Product_MerchantAddressDAO = new Product_MerchantAddressDAO();
            
            Product_MerchantAddressDAO.ProductId = Product_MerchantAddress.ProductId;
            Product_MerchantAddressDAO.MerchantAddressId = Product_MerchantAddress.MerchantAddressId;
            
            await DataContext.Product_MerchantAddress.AddAsync(Product_MerchantAddressDAO);
            await DataContext.SaveChangesAsync();
            
            return true;
        }

        
        
        public async Task<bool> Update(Product_MerchantAddress Product_MerchantAddress)
        {
            Product_MerchantAddressDAO Product_MerchantAddressDAO = DataContext.Product_MerchantAddress.Where(x => x.ProductId == Product_MerchantAddress.ProductId && x.MerchantAddressId == Product_MerchantAddress.MerchantAddressId).FirstOrDefault();
            
            Product_MerchantAddressDAO.ProductId = Product_MerchantAddress.ProductId;
            Product_MerchantAddressDAO.MerchantAddressId = Product_MerchantAddress.MerchantAddressId;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Product_MerchantAddress Product_MerchantAddress)
        {
            Product_MerchantAddressDAO Product_MerchantAddressDAO = await DataContext.Product_MerchantAddress.Where(x => x.ProductId == Product_MerchantAddress.ProductId && x.MerchantAddressId == Product_MerchantAddress.MerchantAddressId).FirstOrDefaultAsync();
            DataContext.Product_MerchantAddress.Remove(Product_MerchantAddressDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
