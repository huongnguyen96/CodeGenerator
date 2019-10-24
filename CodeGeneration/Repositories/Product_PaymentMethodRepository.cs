
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
    public interface IProduct_PaymentMethodRepository
    {
        Task<int> Count(Product_PaymentMethodFilter Product_PaymentMethodFilter);
        Task<List<Product_PaymentMethod>> List(Product_PaymentMethodFilter Product_PaymentMethodFilter);
        Task<Product_PaymentMethod> Get(long ProductId, long PaymentMethodId);
        Task<bool> Create(Product_PaymentMethod Product_PaymentMethod);
        Task<bool> Update(Product_PaymentMethod Product_PaymentMethod);
        Task<bool> Delete(Product_PaymentMethod Product_PaymentMethod);
        
    }
    public class Product_PaymentMethodRepository : IProduct_PaymentMethodRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public Product_PaymentMethodRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<Product_PaymentMethodDAO> DynamicFilter(IQueryable<Product_PaymentMethodDAO> query, Product_PaymentMethodFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.ProductId != null)
                query = query.Where(q => q.ProductId, filter.ProductId);
            if (filter.PaymentMethodId != null)
                query = query.Where(q => q.PaymentMethodId, filter.PaymentMethodId);
            return query;
        }
        private IQueryable<Product_PaymentMethodDAO> DynamicOrder(IQueryable<Product_PaymentMethodDAO> query,  Product_PaymentMethodFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case Product_PaymentMethodOrder.Product:
                            query = query.OrderBy(q => q.Product.Id);
                            break;
                        case Product_PaymentMethodOrder.PaymentMethod:
                            query = query.OrderBy(q => q.PaymentMethod.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case Product_PaymentMethodOrder.Product:
                            query = query.OrderByDescending(q => q.Product.Id);
                            break;
                        case Product_PaymentMethodOrder.PaymentMethod:
                            query = query.OrderByDescending(q => q.PaymentMethod.Id);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Product_PaymentMethod>> DynamicSelect(IQueryable<Product_PaymentMethodDAO> query, Product_PaymentMethodFilter filter)
        {
            List <Product_PaymentMethod> Product_PaymentMethods = await query.Select(q => new Product_PaymentMethod()
            {
                
                ProductId = filter.Selects.Contains(Product_PaymentMethodSelect.Product) ? q.ProductId : default(long),
                PaymentMethodId = filter.Selects.Contains(Product_PaymentMethodSelect.PaymentMethod) ? q.PaymentMethodId : default(long),
                PaymentMethod = filter.Selects.Contains(Product_PaymentMethodSelect.PaymentMethod) && q.PaymentMethod != null ? new PaymentMethod
                {
                    
                    Id = q.PaymentMethod.Id,
                    Code = q.PaymentMethod.Code,
                    Name = q.PaymentMethod.Name,
                    Description = q.PaymentMethod.Description,
                } : null,
                Product = filter.Selects.Contains(Product_PaymentMethodSelect.Product) && q.Product != null ? new Product
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
            return Product_PaymentMethods;
        }

        public async Task<int> Count(Product_PaymentMethodFilter filter)
        {
            IQueryable <Product_PaymentMethodDAO> Product_PaymentMethodDAOs = DataContext.Product_PaymentMethod;
            Product_PaymentMethodDAOs = DynamicFilter(Product_PaymentMethodDAOs, filter);
            return await Product_PaymentMethodDAOs.CountAsync();
        }

        public async Task<List<Product_PaymentMethod>> List(Product_PaymentMethodFilter filter)
        {
            if (filter == null) return new List<Product_PaymentMethod>();
            IQueryable<Product_PaymentMethodDAO> Product_PaymentMethodDAOs = DataContext.Product_PaymentMethod;
            Product_PaymentMethodDAOs = DynamicFilter(Product_PaymentMethodDAOs, filter);
            Product_PaymentMethodDAOs = DynamicOrder(Product_PaymentMethodDAOs, filter);
            var Product_PaymentMethods = await DynamicSelect(Product_PaymentMethodDAOs, filter);
            return Product_PaymentMethods;
        }

        
        public async Task<Product_PaymentMethod> Get(long ProductId, long PaymentMethodId)
        {
            Product_PaymentMethod Product_PaymentMethod = await DataContext.Product_PaymentMethod.Where(x => x.ProductId == ProductId && x.PaymentMethodId == PaymentMethodId).Select(Product_PaymentMethodDAO => new Product_PaymentMethod()
            {
                 
                ProductId = Product_PaymentMethodDAO.ProductId,
                PaymentMethodId = Product_PaymentMethodDAO.PaymentMethodId,
                PaymentMethod = Product_PaymentMethodDAO.PaymentMethod == null ? null : new PaymentMethod
                {
                    
                    Id = Product_PaymentMethodDAO.PaymentMethod.Id,
                    Code = Product_PaymentMethodDAO.PaymentMethod.Code,
                    Name = Product_PaymentMethodDAO.PaymentMethod.Name,
                    Description = Product_PaymentMethodDAO.PaymentMethod.Description,
                },
                Product = Product_PaymentMethodDAO.Product == null ? null : new Product
                {
                    
                    Id = Product_PaymentMethodDAO.Product.Id,
                    Code = Product_PaymentMethodDAO.Product.Code,
                    Name = Product_PaymentMethodDAO.Product.Name,
                    Description = Product_PaymentMethodDAO.Product.Description,
                    TypeId = Product_PaymentMethodDAO.Product.TypeId,
                    StatusId = Product_PaymentMethodDAO.Product.StatusId,
                    MerchantId = Product_PaymentMethodDAO.Product.MerchantId,
                    CategoryId = Product_PaymentMethodDAO.Product.CategoryId,
                    BrandId = Product_PaymentMethodDAO.Product.BrandId,
                    WarrantyPolicy = Product_PaymentMethodDAO.Product.WarrantyPolicy,
                    ReturnPolicy = Product_PaymentMethodDAO.Product.ReturnPolicy,
                    ExpiredDate = Product_PaymentMethodDAO.Product.ExpiredDate,
                    ConditionOfUse = Product_PaymentMethodDAO.Product.ConditionOfUse,
                    MaximumPurchaseQuantity = Product_PaymentMethodDAO.Product.MaximumPurchaseQuantity,
                },
            }).FirstOrDefaultAsync();
            return Product_PaymentMethod;
        }

        public async Task<bool> Create(Product_PaymentMethod Product_PaymentMethod)
        {
            Product_PaymentMethodDAO Product_PaymentMethodDAO = new Product_PaymentMethodDAO();
            
            Product_PaymentMethodDAO.ProductId = Product_PaymentMethod.ProductId;
            Product_PaymentMethodDAO.PaymentMethodId = Product_PaymentMethod.PaymentMethodId;
            
            await DataContext.Product_PaymentMethod.AddAsync(Product_PaymentMethodDAO);
            await DataContext.SaveChangesAsync();
            
            return true;
        }

        
        
        public async Task<bool> Update(Product_PaymentMethod Product_PaymentMethod)
        {
            Product_PaymentMethodDAO Product_PaymentMethodDAO = DataContext.Product_PaymentMethod.Where(x => x.ProductId == Product_PaymentMethod.ProductId && x.PaymentMethodId == Product_PaymentMethod.PaymentMethodId).FirstOrDefault();
            
            Product_PaymentMethodDAO.ProductId = Product_PaymentMethod.ProductId;
            Product_PaymentMethodDAO.PaymentMethodId = Product_PaymentMethod.PaymentMethodId;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Product_PaymentMethod Product_PaymentMethod)
        {
            Product_PaymentMethodDAO Product_PaymentMethodDAO = await DataContext.Product_PaymentMethod.Where(x => x.ProductId == Product_PaymentMethod.ProductId && x.PaymentMethodId == Product_PaymentMethod.PaymentMethodId).FirstOrDefaultAsync();
            DataContext.Product_PaymentMethod.Remove(Product_PaymentMethodDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
