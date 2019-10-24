
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
    public interface IProductRepository
    {
        Task<int> Count(ProductFilter ProductFilter);
        Task<List<Product>> List(ProductFilter ProductFilter);
        Task<Product> Get(long Id);
        Task<bool> Create(Product Product);
        Task<bool> Update(Product Product);
        Task<bool> Delete(Product Product);
        
    }
    public class ProductRepository : IProductRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public ProductRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ProductDAO> DynamicFilter(IQueryable<ProductDAO> query, ProductFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.TypeId != null)
                query = query.Where(q => q.TypeId, filter.TypeId);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.MerchantId != null)
                query = query.Where(q => q.MerchantId, filter.MerchantId);
            if (filter.CategoryId != null)
                query = query.Where(q => q.CategoryId, filter.CategoryId);
            if (filter.BrandId != null)
                query = query.Where(q => q.BrandId, filter.BrandId);
            if (filter.WarrantyPolicy != null)
                query = query.Where(q => q.WarrantyPolicy, filter.WarrantyPolicy);
            if (filter.ReturnPolicy != null)
                query = query.Where(q => q.ReturnPolicy, filter.ReturnPolicy);
            if (filter.ExpiredDate != null)
                query = query.Where(q => q.ExpiredDate, filter.ExpiredDate);
            if (filter.ConditionOfUse != null)
                query = query.Where(q => q.ConditionOfUse, filter.ConditionOfUse);
            if (filter.MaximumPurchaseQuantity != null)
                query = query.Where(q => q.MaximumPurchaseQuantity, filter.MaximumPurchaseQuantity);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<ProductDAO> DynamicOrder(IQueryable<ProductDAO> query,  ProductFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ProductOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ProductOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ProductOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case ProductOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case ProductOrder.Type:
                            query = query.OrderBy(q => q.Type.Id);
                            break;
                        case ProductOrder.Status:
                            query = query.OrderBy(q => q.Status.Id);
                            break;
                        case ProductOrder.Merchant:
                            query = query.OrderBy(q => q.Merchant.Id);
                            break;
                        case ProductOrder.Category:
                            query = query.OrderBy(q => q.Category.Id);
                            break;
                        case ProductOrder.Brand:
                            query = query.OrderBy(q => q.Brand.Id);
                            break;
                        case ProductOrder.WarrantyPolicy:
                            query = query.OrderBy(q => q.WarrantyPolicy);
                            break;
                        case ProductOrder.ReturnPolicy:
                            query = query.OrderBy(q => q.ReturnPolicy);
                            break;
                        case ProductOrder.ExpiredDate:
                            query = query.OrderBy(q => q.ExpiredDate);
                            break;
                        case ProductOrder.ConditionOfUse:
                            query = query.OrderBy(q => q.ConditionOfUse);
                            break;
                        case ProductOrder.MaximumPurchaseQuantity:
                            query = query.OrderBy(q => q.MaximumPurchaseQuantity);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ProductOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ProductOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ProductOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case ProductOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case ProductOrder.Type:
                            query = query.OrderByDescending(q => q.Type.Id);
                            break;
                        case ProductOrder.Status:
                            query = query.OrderByDescending(q => q.Status.Id);
                            break;
                        case ProductOrder.Merchant:
                            query = query.OrderByDescending(q => q.Merchant.Id);
                            break;
                        case ProductOrder.Category:
                            query = query.OrderByDescending(q => q.Category.Id);
                            break;
                        case ProductOrder.Brand:
                            query = query.OrderByDescending(q => q.Brand.Id);
                            break;
                        case ProductOrder.WarrantyPolicy:
                            query = query.OrderByDescending(q => q.WarrantyPolicy);
                            break;
                        case ProductOrder.ReturnPolicy:
                            query = query.OrderByDescending(q => q.ReturnPolicy);
                            break;
                        case ProductOrder.ExpiredDate:
                            query = query.OrderByDescending(q => q.ExpiredDate);
                            break;
                        case ProductOrder.ConditionOfUse:
                            query = query.OrderByDescending(q => q.ConditionOfUse);
                            break;
                        case ProductOrder.MaximumPurchaseQuantity:
                            query = query.OrderByDescending(q => q.MaximumPurchaseQuantity);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Product>> DynamicSelect(IQueryable<ProductDAO> query, ProductFilter filter)
        {
            List <Product> Products = await query.Select(q => new Product()
            {
                
                Id = filter.Selects.Contains(ProductSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ProductSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ProductSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(ProductSelect.Description) ? q.Description : default(string),
                TypeId = filter.Selects.Contains(ProductSelect.Type) ? q.TypeId : default(long),
                StatusId = filter.Selects.Contains(ProductSelect.Status) ? q.StatusId : default(long),
                MerchantId = filter.Selects.Contains(ProductSelect.Merchant) ? q.MerchantId : default(long),
                CategoryId = filter.Selects.Contains(ProductSelect.Category) ? q.CategoryId : default(long),
                BrandId = filter.Selects.Contains(ProductSelect.Brand) ? q.BrandId : default(long),
                WarrantyPolicy = filter.Selects.Contains(ProductSelect.WarrantyPolicy) ? q.WarrantyPolicy : default(string),
                ReturnPolicy = filter.Selects.Contains(ProductSelect.ReturnPolicy) ? q.ReturnPolicy : default(string),
                ExpiredDate = filter.Selects.Contains(ProductSelect.ExpiredDate) ? q.ExpiredDate : default(string),
                ConditionOfUse = filter.Selects.Contains(ProductSelect.ConditionOfUse) ? q.ConditionOfUse : default(string),
                MaximumPurchaseQuantity = filter.Selects.Contains(ProductSelect.MaximumPurchaseQuantity) ? q.MaximumPurchaseQuantity : default(long?),
                Brand = filter.Selects.Contains(ProductSelect.Brand) && q.Brand != null ? new Brand
                {
                    
                    Id = q.Brand.Id,
                    Name = q.Brand.Name,
                    CategoryId = q.Brand.CategoryId,
                } : null,
                Category = filter.Selects.Contains(ProductSelect.Category) && q.Category != null ? new Category
                {
                    
                    Id = q.Category.Id,
                    Code = q.Category.Code,
                    Name = q.Category.Name,
                    ParentId = q.Category.ParentId,
                    Icon = q.Category.Icon,
                } : null,
                Merchant = filter.Selects.Contains(ProductSelect.Merchant) && q.Merchant != null ? new Merchant
                {
                    
                    Id = q.Merchant.Id,
                    Name = q.Merchant.Name,
                    Phone = q.Merchant.Phone,
                    ContactPerson = q.Merchant.ContactPerson,
                    Address = q.Merchant.Address,
                } : null,
                Status = filter.Selects.Contains(ProductSelect.Status) && q.Status != null ? new ProductStatus
                {
                    
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                Type = filter.Selects.Contains(ProductSelect.Type) && q.Type != null ? new ProductType
                {
                    
                    Id = q.Type.Id,
                    Code = q.Type.Code,
                    Name = q.Type.Name,
                } : null,
            }).ToListAsync();
            return Products;
        }

        public async Task<int> Count(ProductFilter filter)
        {
            IQueryable <ProductDAO> ProductDAOs = DataContext.Product;
            ProductDAOs = DynamicFilter(ProductDAOs, filter);
            return await ProductDAOs.CountAsync();
        }

        public async Task<List<Product>> List(ProductFilter filter)
        {
            if (filter == null) return new List<Product>();
            IQueryable<ProductDAO> ProductDAOs = DataContext.Product;
            ProductDAOs = DynamicFilter(ProductDAOs, filter);
            ProductDAOs = DynamicOrder(ProductDAOs, filter);
            var Products = await DynamicSelect(ProductDAOs, filter);
            return Products;
        }

        
        public async Task<Product> Get(long Id)
        {
            Product Product = await DataContext.Product.Where(x => x.Id == Id).Select(ProductDAO => new Product()
            {
                 
                Id = ProductDAO.Id,
                Code = ProductDAO.Code,
                Name = ProductDAO.Name,
                Description = ProductDAO.Description,
                TypeId = ProductDAO.TypeId,
                StatusId = ProductDAO.StatusId,
                MerchantId = ProductDAO.MerchantId,
                CategoryId = ProductDAO.CategoryId,
                BrandId = ProductDAO.BrandId,
                WarrantyPolicy = ProductDAO.WarrantyPolicy,
                ReturnPolicy = ProductDAO.ReturnPolicy,
                ExpiredDate = ProductDAO.ExpiredDate,
                ConditionOfUse = ProductDAO.ConditionOfUse,
                MaximumPurchaseQuantity = ProductDAO.MaximumPurchaseQuantity,
                Brand = ProductDAO.Brand == null ? null : new Brand
                {
                    
                    Id = ProductDAO.Brand.Id,
                    Name = ProductDAO.Brand.Name,
                    CategoryId = ProductDAO.Brand.CategoryId,
                },
                Category = ProductDAO.Category == null ? null : new Category
                {
                    
                    Id = ProductDAO.Category.Id,
                    Code = ProductDAO.Category.Code,
                    Name = ProductDAO.Category.Name,
                    ParentId = ProductDAO.Category.ParentId,
                    Icon = ProductDAO.Category.Icon,
                },
                Merchant = ProductDAO.Merchant == null ? null : new Merchant
                {
                    
                    Id = ProductDAO.Merchant.Id,
                    Name = ProductDAO.Merchant.Name,
                    Phone = ProductDAO.Merchant.Phone,
                    ContactPerson = ProductDAO.Merchant.ContactPerson,
                    Address = ProductDAO.Merchant.Address,
                },
                Status = ProductDAO.Status == null ? null : new ProductStatus
                {
                    
                    Id = ProductDAO.Status.Id,
                    Code = ProductDAO.Status.Code,
                    Name = ProductDAO.Status.Name,
                },
                Type = ProductDAO.Type == null ? null : new ProductType
                {
                    
                    Id = ProductDAO.Type.Id,
                    Code = ProductDAO.Type.Code,
                    Name = ProductDAO.Type.Name,
                },
            }).FirstOrDefaultAsync();
            return Product;
        }

        public async Task<bool> Create(Product Product)
        {
            ProductDAO ProductDAO = new ProductDAO();
            
            ProductDAO.Id = Product.Id;
            ProductDAO.Code = Product.Code;
            ProductDAO.Name = Product.Name;
            ProductDAO.Description = Product.Description;
            ProductDAO.TypeId = Product.TypeId;
            ProductDAO.StatusId = Product.StatusId;
            ProductDAO.MerchantId = Product.MerchantId;
            ProductDAO.CategoryId = Product.CategoryId;
            ProductDAO.BrandId = Product.BrandId;
            ProductDAO.WarrantyPolicy = Product.WarrantyPolicy;
            ProductDAO.ReturnPolicy = Product.ReturnPolicy;
            ProductDAO.ExpiredDate = Product.ExpiredDate;
            ProductDAO.ConditionOfUse = Product.ConditionOfUse;
            ProductDAO.MaximumPurchaseQuantity = Product.MaximumPurchaseQuantity;
            
            await DataContext.Product.AddAsync(ProductDAO);
            await DataContext.SaveChangesAsync();
            Product.Id = ProductDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(Product Product)
        {
            ProductDAO ProductDAO = DataContext.Product.Where(x => x.Id == Product.Id).FirstOrDefault();
            
            ProductDAO.Id = Product.Id;
            ProductDAO.Code = Product.Code;
            ProductDAO.Name = Product.Name;
            ProductDAO.Description = Product.Description;
            ProductDAO.TypeId = Product.TypeId;
            ProductDAO.StatusId = Product.StatusId;
            ProductDAO.MerchantId = Product.MerchantId;
            ProductDAO.CategoryId = Product.CategoryId;
            ProductDAO.BrandId = Product.BrandId;
            ProductDAO.WarrantyPolicy = Product.WarrantyPolicy;
            ProductDAO.ReturnPolicy = Product.ReturnPolicy;
            ProductDAO.ExpiredDate = Product.ExpiredDate;
            ProductDAO.ConditionOfUse = Product.ConditionOfUse;
            ProductDAO.MaximumPurchaseQuantity = Product.MaximumPurchaseQuantity;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Product Product)
        {
            ProductDAO ProductDAO = await DataContext.Product.Where(x => x.Id == Product.Id).FirstOrDefaultAsync();
            DataContext.Product.Remove(ProductDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
