
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
    public interface IPartnerRepository
    {
        Task<int> Count(PartnerFilter PartnerFilter);
        Task<List<Partner>> List(PartnerFilter PartnerFilter);
        Task<Partner> Get(long Id);
        Task<bool> Create(Partner Partner);
        Task<bool> Update(Partner Partner);
        Task<bool> Delete(Partner Partner);
        
    }
    public class PartnerRepository : IPartnerRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public PartnerRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<PartnerDAO> DynamicFilter(IQueryable<PartnerDAO> query, PartnerFilter filter)
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
        private IQueryable<PartnerDAO> DynamicOrder(IQueryable<PartnerDAO> query,  PartnerFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case PartnerOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case PartnerOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case PartnerOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                        case PartnerOrder.ContactPerson:
                            query = query.OrderBy(q => q.ContactPerson);
                            break;
                        case PartnerOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case PartnerOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case PartnerOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case PartnerOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                        case PartnerOrder.ContactPerson:
                            query = query.OrderByDescending(q => q.ContactPerson);
                            break;
                        case PartnerOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Partner>> DynamicSelect(IQueryable<PartnerDAO> query, PartnerFilter filter)
        {
            List <Partner> Partners = await query.Select(q => new Partner()
            {
                
                Id = filter.Selects.Contains(PartnerSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(PartnerSelect.Name) ? q.Name : default(string),
                Phone = filter.Selects.Contains(PartnerSelect.Phone) ? q.Phone : default(string),
                ContactPerson = filter.Selects.Contains(PartnerSelect.ContactPerson) ? q.ContactPerson : default(string),
                Address = filter.Selects.Contains(PartnerSelect.Address) ? q.Address : default(string),
            }).ToListAsync();
            return Partners;
        }

        public async Task<int> Count(PartnerFilter filter)
        {
            IQueryable <PartnerDAO> PartnerDAOs = DataContext.Partner;
            PartnerDAOs = DynamicFilter(PartnerDAOs, filter);
            return await PartnerDAOs.CountAsync();
        }

        public async Task<List<Partner>> List(PartnerFilter filter)
        {
            if (filter == null) return new List<Partner>();
            IQueryable<PartnerDAO> PartnerDAOs = DataContext.Partner;
            PartnerDAOs = DynamicFilter(PartnerDAOs, filter);
            PartnerDAOs = DynamicOrder(PartnerDAOs, filter);
            var Partners = await DynamicSelect(PartnerDAOs, filter);
            return Partners;
        }

        
        public async Task<Partner> Get(long Id)
        {
            Partner Partner = await DataContext.Partner.Where(x => x.Id == Id).Select(PartnerDAO => new Partner()
            {
                 
                Id = PartnerDAO.Id,
                Name = PartnerDAO.Name,
                Phone = PartnerDAO.Phone,
                ContactPerson = PartnerDAO.ContactPerson,
                Address = PartnerDAO.Address,
            }).FirstOrDefaultAsync();
            return Partner;
        }

        public async Task<bool> Create(Partner Partner)
        {
            PartnerDAO PartnerDAO = new PartnerDAO();
            
            PartnerDAO.Id = Partner.Id;
            PartnerDAO.Name = Partner.Name;
            PartnerDAO.Phone = Partner.Phone;
            PartnerDAO.ContactPerson = Partner.ContactPerson;
            PartnerDAO.Address = Partner.Address;
            
            await DataContext.Partner.AddAsync(PartnerDAO);
            await DataContext.SaveChangesAsync();
            Partner.Id = PartnerDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(Partner Partner)
        {
            PartnerDAO PartnerDAO = DataContext.Partner.Where(x => x.Id == Partner.Id).FirstOrDefault();
            
            PartnerDAO.Id = Partner.Id;
            PartnerDAO.Name = Partner.Name;
            PartnerDAO.Phone = Partner.Phone;
            PartnerDAO.ContactPerson = Partner.ContactPerson;
            PartnerDAO.Address = Partner.Address;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Partner Partner)
        {
            PartnerDAO PartnerDAO = await DataContext.Partner.Where(x => x.Id == Partner.Id).FirstOrDefaultAsync();
            DataContext.Partner.Remove(PartnerDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
