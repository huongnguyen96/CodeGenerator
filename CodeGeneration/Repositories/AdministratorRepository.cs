
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
    public interface IAdministratorRepository
    {
        Task<int> Count(AdministratorFilter AdministratorFilter);
        Task<List<Administrator>> List(AdministratorFilter AdministratorFilter);
        Task<Administrator> Get(long Id);
        Task<bool> Create(Administrator Administrator);
        Task<bool> Update(Administrator Administrator);
        Task<bool> Delete(Administrator Administrator);
        
    }
    public class AdministratorRepository : IAdministratorRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public AdministratorRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<AdministratorDAO> DynamicFilter(IQueryable<AdministratorDAO> query, AdministratorFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Username != null)
                query = query.Where(q => q.Username, filter.Username);
            if (filter.DisplayName != null)
                query = query.Where(q => q.DisplayName, filter.DisplayName);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<AdministratorDAO> DynamicOrder(IQueryable<AdministratorDAO> query,  AdministratorFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case AdministratorOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case AdministratorOrder.Username:
                            query = query.OrderBy(q => q.Username);
                            break;
                        case AdministratorOrder.DisplayName:
                            query = query.OrderBy(q => q.DisplayName);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case AdministratorOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case AdministratorOrder.Username:
                            query = query.OrderByDescending(q => q.Username);
                            break;
                        case AdministratorOrder.DisplayName:
                            query = query.OrderByDescending(q => q.DisplayName);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Administrator>> DynamicSelect(IQueryable<AdministratorDAO> query, AdministratorFilter filter)
        {
            List <Administrator> Administrators = await query.Select(q => new Administrator()
            {
                
                Id = filter.Selects.Contains(AdministratorSelect.Id) ? q.Id : default(long),
                Username = filter.Selects.Contains(AdministratorSelect.Username) ? q.Username : default(string),
                DisplayName = filter.Selects.Contains(AdministratorSelect.DisplayName) ? q.DisplayName : default(string),
            }).ToListAsync();
            return Administrators;
        }

        public async Task<int> Count(AdministratorFilter filter)
        {
            IQueryable <AdministratorDAO> AdministratorDAOs = DataContext.Administrator;
            AdministratorDAOs = DynamicFilter(AdministratorDAOs, filter);
            return await AdministratorDAOs.CountAsync();
        }

        public async Task<List<Administrator>> List(AdministratorFilter filter)
        {
            if (filter == null) return new List<Administrator>();
            IQueryable<AdministratorDAO> AdministratorDAOs = DataContext.Administrator;
            AdministratorDAOs = DynamicFilter(AdministratorDAOs, filter);
            AdministratorDAOs = DynamicOrder(AdministratorDAOs, filter);
            var Administrators = await DynamicSelect(AdministratorDAOs, filter);
            return Administrators;
        }

        
        public async Task<Administrator> Get(long Id)
        {
            Administrator Administrator = await DataContext.Administrator.Where(x => x.Id == Id).Select(AdministratorDAO => new Administrator()
            {
                 
                Id = AdministratorDAO.Id,
                Username = AdministratorDAO.Username,
                DisplayName = AdministratorDAO.DisplayName,
            }).FirstOrDefaultAsync();
            return Administrator;
        }

        public async Task<bool> Create(Administrator Administrator)
        {
            AdministratorDAO AdministratorDAO = new AdministratorDAO();
            
            AdministratorDAO.Id = Administrator.Id;
            AdministratorDAO.Username = Administrator.Username;
            AdministratorDAO.DisplayName = Administrator.DisplayName;
            
            await DataContext.Administrator.AddAsync(AdministratorDAO);
            await DataContext.SaveChangesAsync();
            Administrator.Id = AdministratorDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(Administrator Administrator)
        {
            AdministratorDAO AdministratorDAO = DataContext.Administrator.Where(x => x.Id == Administrator.Id).FirstOrDefault();
            
            AdministratorDAO.Id = Administrator.Id;
            AdministratorDAO.Username = Administrator.Username;
            AdministratorDAO.DisplayName = Administrator.DisplayName;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Administrator Administrator)
        {
            AdministratorDAO AdministratorDAO = await DataContext.Administrator.Where(x => x.Id == Administrator.Id).FirstOrDefaultAsync();
            DataContext.Administrator.Remove(AdministratorDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
