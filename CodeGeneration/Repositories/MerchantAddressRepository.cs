
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
    public interface IMerchantAddressRepository
    {
        Task<int> Count(MerchantAddressFilter MerchantAddressFilter);
        Task<List<MerchantAddress>> List(MerchantAddressFilter MerchantAddressFilter);
        Task<MerchantAddress> Get(long Id);
        Task<bool> Create(MerchantAddress MerchantAddress);
        Task<bool> Update(MerchantAddress MerchantAddress);
        Task<bool> Delete(MerchantAddress MerchantAddress);
        
    }
    public class MerchantAddressRepository : IMerchantAddressRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public MerchantAddressRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<MerchantAddressDAO> DynamicFilter(IQueryable<MerchantAddressDAO> query, MerchantAddressFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.MerchantId != null)
                query = query.Where(q => q.MerchantId, filter.MerchantId);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Address != null)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.Contact != null)
                query = query.Where(q => q.Contact, filter.Contact);
            if (filter.Phone != null)
                query = query.Where(q => q.Phone, filter.Phone);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<MerchantAddressDAO> DynamicOrder(IQueryable<MerchantAddressDAO> query,  MerchantAddressFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case MerchantAddressOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case MerchantAddressOrder.Merchant:
                            query = query.OrderBy(q => q.Merchant.Id);
                            break;
                        case MerchantAddressOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case MerchantAddressOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        case MerchantAddressOrder.Contact:
                            query = query.OrderBy(q => q.Contact);
                            break;
                        case MerchantAddressOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case MerchantAddressOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case MerchantAddressOrder.Merchant:
                            query = query.OrderByDescending(q => q.Merchant.Id);
                            break;
                        case MerchantAddressOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case MerchantAddressOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        case MerchantAddressOrder.Contact:
                            query = query.OrderByDescending(q => q.Contact);
                            break;
                        case MerchantAddressOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<MerchantAddress>> DynamicSelect(IQueryable<MerchantAddressDAO> query, MerchantAddressFilter filter)
        {
            List <MerchantAddress> MerchantAddresss = await query.Select(q => new MerchantAddress()
            {
                
                Id = filter.Selects.Contains(MerchantAddressSelect.Id) ? q.Id : default(long),
                MerchantId = filter.Selects.Contains(MerchantAddressSelect.Merchant) ? q.MerchantId : default(long),
                Code = filter.Selects.Contains(MerchantAddressSelect.Code) ? q.Code : default(string),
                Address = filter.Selects.Contains(MerchantAddressSelect.Address) ? q.Address : default(string),
                Contact = filter.Selects.Contains(MerchantAddressSelect.Contact) ? q.Contact : default(string),
                Phone = filter.Selects.Contains(MerchantAddressSelect.Phone) ? q.Phone : default(string),
                Merchant = filter.Selects.Contains(MerchantAddressSelect.Merchant) && q.Merchant != null ? new Merchant
                {
                    
                    Id = q.Merchant.Id,
                    Name = q.Merchant.Name,
                    Phone = q.Merchant.Phone,
                    ContactPerson = q.Merchant.ContactPerson,
                    Address = q.Merchant.Address,
                } : null,
            }).ToListAsync();
            return MerchantAddresss;
        }

        public async Task<int> Count(MerchantAddressFilter filter)
        {
            IQueryable <MerchantAddressDAO> MerchantAddressDAOs = DataContext.MerchantAddress;
            MerchantAddressDAOs = DynamicFilter(MerchantAddressDAOs, filter);
            return await MerchantAddressDAOs.CountAsync();
        }

        public async Task<List<MerchantAddress>> List(MerchantAddressFilter filter)
        {
            if (filter == null) return new List<MerchantAddress>();
            IQueryable<MerchantAddressDAO> MerchantAddressDAOs = DataContext.MerchantAddress;
            MerchantAddressDAOs = DynamicFilter(MerchantAddressDAOs, filter);
            MerchantAddressDAOs = DynamicOrder(MerchantAddressDAOs, filter);
            var MerchantAddresss = await DynamicSelect(MerchantAddressDAOs, filter);
            return MerchantAddresss;
        }

        
        public async Task<MerchantAddress> Get(long Id)
        {
            MerchantAddress MerchantAddress = await DataContext.MerchantAddress.Where(x => x.Id == Id).Select(MerchantAddressDAO => new MerchantAddress()
            {
                 
                Id = MerchantAddressDAO.Id,
                MerchantId = MerchantAddressDAO.MerchantId,
                Code = MerchantAddressDAO.Code,
                Address = MerchantAddressDAO.Address,
                Contact = MerchantAddressDAO.Contact,
                Phone = MerchantAddressDAO.Phone,
                Merchant = MerchantAddressDAO.Merchant == null ? null : new Merchant
                {
                    
                    Id = MerchantAddressDAO.Merchant.Id,
                    Name = MerchantAddressDAO.Merchant.Name,
                    Phone = MerchantAddressDAO.Merchant.Phone,
                    ContactPerson = MerchantAddressDAO.Merchant.ContactPerson,
                    Address = MerchantAddressDAO.Merchant.Address,
                },
            }).FirstOrDefaultAsync();
            return MerchantAddress;
        }

        public async Task<bool> Create(MerchantAddress MerchantAddress)
        {
            MerchantAddressDAO MerchantAddressDAO = new MerchantAddressDAO();
            
            MerchantAddressDAO.Id = MerchantAddress.Id;
            MerchantAddressDAO.MerchantId = MerchantAddress.MerchantId;
            MerchantAddressDAO.Code = MerchantAddress.Code;
            MerchantAddressDAO.Address = MerchantAddress.Address;
            MerchantAddressDAO.Contact = MerchantAddress.Contact;
            MerchantAddressDAO.Phone = MerchantAddress.Phone;
            
            await DataContext.MerchantAddress.AddAsync(MerchantAddressDAO);
            await DataContext.SaveChangesAsync();
            MerchantAddress.Id = MerchantAddressDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(MerchantAddress MerchantAddress)
        {
            MerchantAddressDAO MerchantAddressDAO = DataContext.MerchantAddress.Where(x => x.Id == MerchantAddress.Id).FirstOrDefault();
            
            MerchantAddressDAO.Id = MerchantAddress.Id;
            MerchantAddressDAO.MerchantId = MerchantAddress.MerchantId;
            MerchantAddressDAO.Code = MerchantAddress.Code;
            MerchantAddressDAO.Address = MerchantAddress.Address;
            MerchantAddressDAO.Contact = MerchantAddress.Contact;
            MerchantAddressDAO.Phone = MerchantAddress.Phone;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(MerchantAddress MerchantAddress)
        {
            MerchantAddressDAO MerchantAddressDAO = await DataContext.MerchantAddress.Where(x => x.Id == MerchantAddress.Id).FirstOrDefaultAsync();
            DataContext.MerchantAddress.Remove(MerchantAddressDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
