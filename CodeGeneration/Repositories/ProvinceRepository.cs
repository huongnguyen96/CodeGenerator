
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
    public interface IProvinceRepository
    {
        Task<int> Count(ProvinceFilter ProvinceFilter);
        Task<List<Province>> List(ProvinceFilter ProvinceFilter);
        Task<Province> Get(long Id);
        Task<bool> Create(Province Province);
        Task<bool> Update(Province Province);
        Task<bool> Delete(Province Province);
        
    }
    public class ProvinceRepository : IProvinceRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public ProvinceRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ProvinceDAO> DynamicFilter(IQueryable<ProvinceDAO> query, ProvinceFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.OrderNumber != null)
                query = query.Where(q => q.OrderNumber, filter.OrderNumber);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<ProvinceDAO> DynamicOrder(IQueryable<ProvinceDAO> query,  ProvinceFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ProvinceOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ProvinceOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case ProvinceOrder.OrderNumber:
                            query = query.OrderBy(q => q.OrderNumber);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ProvinceOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ProvinceOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case ProvinceOrder.OrderNumber:
                            query = query.OrderByDescending(q => q.OrderNumber);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Province>> DynamicSelect(IQueryable<ProvinceDAO> query, ProvinceFilter filter)
        {
            List <Province> Provinces = await query.Select(q => new Province()
            {
                
                Id = filter.Selects.Contains(ProvinceSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(ProvinceSelect.Name) ? q.Name : default(string),
                OrderNumber = filter.Selects.Contains(ProvinceSelect.OrderNumber) ? q.OrderNumber : default(long),
            }).ToListAsync();
            return Provinces;
        }

        public async Task<int> Count(ProvinceFilter filter)
        {
            IQueryable <ProvinceDAO> ProvinceDAOs = DataContext.Province;
            ProvinceDAOs = DynamicFilter(ProvinceDAOs, filter);
            return await ProvinceDAOs.CountAsync();
        }

        public async Task<List<Province>> List(ProvinceFilter filter)
        {
            if (filter == null) return new List<Province>();
            IQueryable<ProvinceDAO> ProvinceDAOs = DataContext.Province;
            ProvinceDAOs = DynamicFilter(ProvinceDAOs, filter);
            ProvinceDAOs = DynamicOrder(ProvinceDAOs, filter);
            var Provinces = await DynamicSelect(ProvinceDAOs, filter);
            return Provinces;
        }

        
        public async Task<Province> Get(long Id)
        {
            Province Province = await DataContext.Province.Where(x => x.Id == Id).Select(ProvinceDAO => new Province()
            {
                 
                Id = ProvinceDAO.Id,
                Name = ProvinceDAO.Name,
                OrderNumber = ProvinceDAO.OrderNumber,
            }).FirstOrDefaultAsync();
            return Province;
        }

        public async Task<bool> Create(Province Province)
        {
            ProvinceDAO ProvinceDAO = new ProvinceDAO();
            
            ProvinceDAO.Id = Province.Id;
            ProvinceDAO.Name = Province.Name;
            ProvinceDAO.OrderNumber = Province.OrderNumber;
            
            await DataContext.Province.AddAsync(ProvinceDAO);
            await DataContext.SaveChangesAsync();
            Province.Id = ProvinceDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(Province Province)
        {
            ProvinceDAO ProvinceDAO = DataContext.Province.Where(x => x.Id == Province.Id).FirstOrDefault();
            
            ProvinceDAO.Id = Province.Id;
            ProvinceDAO.Name = Province.Name;
            ProvinceDAO.OrderNumber = Province.OrderNumber;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Province Province)
        {
            ProvinceDAO ProvinceDAO = await DataContext.Province.Where(x => x.Id == Province.Id).FirstOrDefaultAsync();
            DataContext.Province.Remove(ProvinceDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
