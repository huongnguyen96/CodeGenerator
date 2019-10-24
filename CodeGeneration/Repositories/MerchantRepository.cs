
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
    public interface IMerchantRepository
    {
        Task<int> Count(MerchantFilter MerchantFilter);
        Task<List<Merchant>> List(MerchantFilter MerchantFilter);
        Task<Merchant> Get(long Id);
        Task<bool> Create(Merchant Merchant);
        Task<bool> Update(Merchant Merchant);
        Task<bool> Delete(Merchant Merchant);
        
    }
    public class MerchantRepository : IMerchantRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public MerchantRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<MerchantDAO> DynamicFilter(IQueryable<MerchantDAO> query, MerchantFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Phone != null)
                query = query.Where(q => q.Phone, filter.Phone);
            if (filter.ContactPerson != null)
                query = query.Where(q => q.ContactPerson, filter.ContactPerson);
            if (filter.Address != null)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<MerchantDAO> DynamicOrder(IQueryable<MerchantDAO> query,  MerchantFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case MerchantOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case MerchantOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case MerchantOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                        case MerchantOrder.ContactPerson:
                            query = query.OrderBy(q => q.ContactPerson);
                            break;
                        case MerchantOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case MerchantOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case MerchantOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case MerchantOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                        case MerchantOrder.ContactPerson:
                            query = query.OrderByDescending(q => q.ContactPerson);
                            break;
                        case MerchantOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Merchant>> DynamicSelect(IQueryable<MerchantDAO> query, MerchantFilter filter)
        {
            List <Merchant> Merchants = await query.Select(q => new Merchant()
            {
                
                Id = filter.Selects.Contains(MerchantSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(MerchantSelect.Name) ? q.Name : default(string),
                Phone = filter.Selects.Contains(MerchantSelect.Phone) ? q.Phone : default(string),
                ContactPerson = filter.Selects.Contains(MerchantSelect.ContactPerson) ? q.ContactPerson : default(string),
                Address = filter.Selects.Contains(MerchantSelect.Address) ? q.Address : default(string),
            }).ToListAsync();
            return Merchants;
        }

        public async Task<int> Count(MerchantFilter filter)
        {
            IQueryable <MerchantDAO> MerchantDAOs = DataContext.Merchant;
            MerchantDAOs = DynamicFilter(MerchantDAOs, filter);
            return await MerchantDAOs.CountAsync();
        }

        public async Task<List<Merchant>> List(MerchantFilter filter)
        {
            if (filter == null) return new List<Merchant>();
            IQueryable<MerchantDAO> MerchantDAOs = DataContext.Merchant;
            MerchantDAOs = DynamicFilter(MerchantDAOs, filter);
            MerchantDAOs = DynamicOrder(MerchantDAOs, filter);
            var Merchants = await DynamicSelect(MerchantDAOs, filter);
            return Merchants;
        }

        
        public async Task<Merchant> Get(long Id)
        {
            Merchant Merchant = await DataContext.Merchant.Where(x => x.Id == Id).Select(MerchantDAO => new Merchant()
            {
                 
                Id = MerchantDAO.Id,
                Name = MerchantDAO.Name,
                Phone = MerchantDAO.Phone,
                ContactPerson = MerchantDAO.ContactPerson,
                Address = MerchantDAO.Address,
            }).FirstOrDefaultAsync();
            return Merchant;
        }

        public async Task<bool> Create(Merchant Merchant)
        {
            MerchantDAO MerchantDAO = new MerchantDAO();
            
            MerchantDAO.Id = Merchant.Id;
            MerchantDAO.Name = Merchant.Name;
            MerchantDAO.Phone = Merchant.Phone;
            MerchantDAO.ContactPerson = Merchant.ContactPerson;
            MerchantDAO.Address = Merchant.Address;
            
            await DataContext.Merchant.AddAsync(MerchantDAO);
            await DataContext.SaveChangesAsync();
            Merchant.Id = MerchantDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(Merchant Merchant)
        {
            MerchantDAO MerchantDAO = DataContext.Merchant.Where(x => x.Id == Merchant.Id).FirstOrDefault();
            
            MerchantDAO.Id = Merchant.Id;
            MerchantDAO.Name = Merchant.Name;
            MerchantDAO.Phone = Merchant.Phone;
            MerchantDAO.ContactPerson = Merchant.ContactPerson;
            MerchantDAO.Address = Merchant.Address;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Merchant Merchant)
        {
            MerchantDAO MerchantDAO = await DataContext.Merchant.Where(x => x.Id == Merchant.Id).FirstOrDefaultAsync();
            DataContext.Merchant.Remove(MerchantDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
