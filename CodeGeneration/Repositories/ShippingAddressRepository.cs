
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
    public interface IShippingAddressRepository
    {
        Task<int> Count(ShippingAddressFilter ShippingAddressFilter);
        Task<List<ShippingAddress>> List(ShippingAddressFilter ShippingAddressFilter);
        Task<ShippingAddress> Get(long Id);
        Task<bool> Create(ShippingAddress ShippingAddress);
        Task<bool> Update(ShippingAddress ShippingAddress);
        Task<bool> Delete(ShippingAddress ShippingAddress);
        
    }
    public class ShippingAddressRepository : IShippingAddressRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public ShippingAddressRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ShippingAddressDAO> DynamicFilter(IQueryable<ShippingAddressDAO> query, ShippingAddressFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.CustomerId != null)
                query = query.Where(q => q.CustomerId, filter.CustomerId);
            if (filter.FullName != null)
                query = query.Where(q => q.FullName, filter.FullName);
            if (filter.CompanyName != null)
                query = query.Where(q => q.CompanyName, filter.CompanyName);
            if (filter.PhoneNumber != null)
                query = query.Where(q => q.PhoneNumber, filter.PhoneNumber);
            if (filter.ProvinceId != null)
                query = query.Where(q => q.ProvinceId, filter.ProvinceId);
            if (filter.DistrictId != null)
                query = query.Where(q => q.DistrictId, filter.DistrictId);
            if (filter.WardId != null)
                query = query.Where(q => q.WardId, filter.WardId);
            if (filter.Address != null)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<ShippingAddressDAO> DynamicOrder(IQueryable<ShippingAddressDAO> query,  ShippingAddressFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ShippingAddressOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ShippingAddressOrder.Customer:
                            query = query.OrderBy(q => q.Customer.Id);
                            break;
                        case ShippingAddressOrder.FullName:
                            query = query.OrderBy(q => q.FullName);
                            break;
                        case ShippingAddressOrder.CompanyName:
                            query = query.OrderBy(q => q.CompanyName);
                            break;
                        case ShippingAddressOrder.PhoneNumber:
                            query = query.OrderBy(q => q.PhoneNumber);
                            break;
                        case ShippingAddressOrder.Province:
                            query = query.OrderBy(q => q.Province.Id);
                            break;
                        case ShippingAddressOrder.District:
                            query = query.OrderBy(q => q.District.Id);
                            break;
                        case ShippingAddressOrder.Ward:
                            query = query.OrderBy(q => q.Ward.Id);
                            break;
                        case ShippingAddressOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ShippingAddressOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ShippingAddressOrder.Customer:
                            query = query.OrderByDescending(q => q.Customer.Id);
                            break;
                        case ShippingAddressOrder.FullName:
                            query = query.OrderByDescending(q => q.FullName);
                            break;
                        case ShippingAddressOrder.CompanyName:
                            query = query.OrderByDescending(q => q.CompanyName);
                            break;
                        case ShippingAddressOrder.PhoneNumber:
                            query = query.OrderByDescending(q => q.PhoneNumber);
                            break;
                        case ShippingAddressOrder.Province:
                            query = query.OrderByDescending(q => q.Province.Id);
                            break;
                        case ShippingAddressOrder.District:
                            query = query.OrderByDescending(q => q.District.Id);
                            break;
                        case ShippingAddressOrder.Ward:
                            query = query.OrderByDescending(q => q.Ward.Id);
                            break;
                        case ShippingAddressOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ShippingAddress>> DynamicSelect(IQueryable<ShippingAddressDAO> query, ShippingAddressFilter filter)
        {
            List <ShippingAddress> ShippingAddresss = await query.Select(q => new ShippingAddress()
            {
                
                Id = filter.Selects.Contains(ShippingAddressSelect.Id) ? q.Id : default(long),
                CustomerId = filter.Selects.Contains(ShippingAddressSelect.Customer) ? q.CustomerId : default(long),
                FullName = filter.Selects.Contains(ShippingAddressSelect.FullName) ? q.FullName : default(string),
                CompanyName = filter.Selects.Contains(ShippingAddressSelect.CompanyName) ? q.CompanyName : default(string),
                PhoneNumber = filter.Selects.Contains(ShippingAddressSelect.PhoneNumber) ? q.PhoneNumber : default(string),
                ProvinceId = filter.Selects.Contains(ShippingAddressSelect.Province) ? q.ProvinceId : default(long),
                DistrictId = filter.Selects.Contains(ShippingAddressSelect.District) ? q.DistrictId : default(long),
                WardId = filter.Selects.Contains(ShippingAddressSelect.Ward) ? q.WardId : default(long),
                Address = filter.Selects.Contains(ShippingAddressSelect.Address) ? q.Address : default(string),
                IsDefault = filter.Selects.Contains(ShippingAddressSelect.IsDefault) ? q.IsDefault : default(bool),
                Customer = filter.Selects.Contains(ShippingAddressSelect.Customer) && q.Customer != null ? new Customer
                {
                    
                    Id = q.Customer.Id,
                    Username = q.Customer.Username,
                    DisplayName = q.Customer.DisplayName,
                } : null,
                District = filter.Selects.Contains(ShippingAddressSelect.District) && q.District != null ? new District
                {
                    
                    Id = q.District.Id,
                    Name = q.District.Name,
                    OrderNumber = q.District.OrderNumber,
                    ProvinceId = q.District.ProvinceId,
                } : null,
                Province = filter.Selects.Contains(ShippingAddressSelect.Province) && q.Province != null ? new Province
                {
                    
                    Id = q.Province.Id,
                    Name = q.Province.Name,
                    OrderNumber = q.Province.OrderNumber,
                } : null,
                Ward = filter.Selects.Contains(ShippingAddressSelect.Ward) && q.Ward != null ? new Ward
                {
                    
                    Id = q.Ward.Id,
                    Name = q.Ward.Name,
                    OrderNumber = q.Ward.OrderNumber,
                    DistrictId = q.Ward.DistrictId,
                } : null,
            }).ToListAsync();
            return ShippingAddresss;
        }

        public async Task<int> Count(ShippingAddressFilter filter)
        {
            IQueryable <ShippingAddressDAO> ShippingAddressDAOs = DataContext.ShippingAddress;
            ShippingAddressDAOs = DynamicFilter(ShippingAddressDAOs, filter);
            return await ShippingAddressDAOs.CountAsync();
        }

        public async Task<List<ShippingAddress>> List(ShippingAddressFilter filter)
        {
            if (filter == null) return new List<ShippingAddress>();
            IQueryable<ShippingAddressDAO> ShippingAddressDAOs = DataContext.ShippingAddress;
            ShippingAddressDAOs = DynamicFilter(ShippingAddressDAOs, filter);
            ShippingAddressDAOs = DynamicOrder(ShippingAddressDAOs, filter);
            var ShippingAddresss = await DynamicSelect(ShippingAddressDAOs, filter);
            return ShippingAddresss;
        }

        
        public async Task<ShippingAddress> Get(long Id)
        {
            ShippingAddress ShippingAddress = await DataContext.ShippingAddress.Where(x => x.Id == Id).Select(ShippingAddressDAO => new ShippingAddress()
            {
                 
                Id = ShippingAddressDAO.Id,
                CustomerId = ShippingAddressDAO.CustomerId,
                FullName = ShippingAddressDAO.FullName,
                CompanyName = ShippingAddressDAO.CompanyName,
                PhoneNumber = ShippingAddressDAO.PhoneNumber,
                ProvinceId = ShippingAddressDAO.ProvinceId,
                DistrictId = ShippingAddressDAO.DistrictId,
                WardId = ShippingAddressDAO.WardId,
                Address = ShippingAddressDAO.Address,
                IsDefault = ShippingAddressDAO.IsDefault,
                Customer = ShippingAddressDAO.Customer == null ? null : new Customer
                {
                    
                    Id = ShippingAddressDAO.Customer.Id,
                    Username = ShippingAddressDAO.Customer.Username,
                    DisplayName = ShippingAddressDAO.Customer.DisplayName,
                },
                District = ShippingAddressDAO.District == null ? null : new District
                {
                    
                    Id = ShippingAddressDAO.District.Id,
                    Name = ShippingAddressDAO.District.Name,
                    OrderNumber = ShippingAddressDAO.District.OrderNumber,
                    ProvinceId = ShippingAddressDAO.District.ProvinceId,
                },
                Province = ShippingAddressDAO.Province == null ? null : new Province
                {
                    
                    Id = ShippingAddressDAO.Province.Id,
                    Name = ShippingAddressDAO.Province.Name,
                    OrderNumber = ShippingAddressDAO.Province.OrderNumber,
                },
                Ward = ShippingAddressDAO.Ward == null ? null : new Ward
                {
                    
                    Id = ShippingAddressDAO.Ward.Id,
                    Name = ShippingAddressDAO.Ward.Name,
                    OrderNumber = ShippingAddressDAO.Ward.OrderNumber,
                    DistrictId = ShippingAddressDAO.Ward.DistrictId,
                },
            }).FirstOrDefaultAsync();
            return ShippingAddress;
        }

        public async Task<bool> Create(ShippingAddress ShippingAddress)
        {
            ShippingAddressDAO ShippingAddressDAO = new ShippingAddressDAO();
            
            ShippingAddressDAO.Id = ShippingAddress.Id;
            ShippingAddressDAO.CustomerId = ShippingAddress.CustomerId;
            ShippingAddressDAO.FullName = ShippingAddress.FullName;
            ShippingAddressDAO.CompanyName = ShippingAddress.CompanyName;
            ShippingAddressDAO.PhoneNumber = ShippingAddress.PhoneNumber;
            ShippingAddressDAO.ProvinceId = ShippingAddress.ProvinceId;
            ShippingAddressDAO.DistrictId = ShippingAddress.DistrictId;
            ShippingAddressDAO.WardId = ShippingAddress.WardId;
            ShippingAddressDAO.Address = ShippingAddress.Address;
            ShippingAddressDAO.IsDefault = ShippingAddress.IsDefault;
            
            await DataContext.ShippingAddress.AddAsync(ShippingAddressDAO);
            await DataContext.SaveChangesAsync();
            ShippingAddress.Id = ShippingAddressDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(ShippingAddress ShippingAddress)
        {
            ShippingAddressDAO ShippingAddressDAO = DataContext.ShippingAddress.Where(x => x.Id == ShippingAddress.Id).FirstOrDefault();
            
            ShippingAddressDAO.Id = ShippingAddress.Id;
            ShippingAddressDAO.CustomerId = ShippingAddress.CustomerId;
            ShippingAddressDAO.FullName = ShippingAddress.FullName;
            ShippingAddressDAO.CompanyName = ShippingAddress.CompanyName;
            ShippingAddressDAO.PhoneNumber = ShippingAddress.PhoneNumber;
            ShippingAddressDAO.ProvinceId = ShippingAddress.ProvinceId;
            ShippingAddressDAO.DistrictId = ShippingAddress.DistrictId;
            ShippingAddressDAO.WardId = ShippingAddress.WardId;
            ShippingAddressDAO.Address = ShippingAddress.Address;
            ShippingAddressDAO.IsDefault = ShippingAddress.IsDefault;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(ShippingAddress ShippingAddress)
        {
            ShippingAddressDAO ShippingAddressDAO = await DataContext.ShippingAddress.Where(x => x.Id == ShippingAddress.Id).FirstOrDefaultAsync();
            DataContext.ShippingAddress.Remove(ShippingAddressDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
