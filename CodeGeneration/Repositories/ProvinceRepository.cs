
using Common;
using ERP.Entities;
using CodeGeneration.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Repositories
{
    public interface IProvinceRepository
    {
        Task<int> Count(ProvinceFilter ProvinceFilter);
        Task<List<Province>> List(ProvinceFilter ProvinceFilter);
        Task<Province> Get(Guid Id);
        Task<bool> Create(Province Province);
        Task<bool> Update(Province Province);
        Task<bool> Delete(Guid Id);
        
    }
    public class ProvinceRepository : IProvinceRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public ProvinceRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
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
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<ProvinceDAO> DynamicOrder(IQueryable<ProvinceDAO> query,  ProvinceFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ProvinceOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ProvinceOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        default:
                            query = query.OrderByDescending(q => q.CX);
                            break;
                    }
                    break;
                default:
                    query = query.OrderBy(q => q.CX);
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Province>> DynamicSelect(IQueryable<ProvinceDAO> query, ProvinceFilter filter)
        {
            List <Province> Provinces = await query.Select(q => new Province()
            {
                
                Id = filter.Selects.Contains(ProvinceSelect.Id) ? q.Id : default(Guid),
                Name = filter.Selects.Contains(ProvinceSelect.Name) ? q.Name : default(string),
                BusinessGroupId = filter.Selects.Contains(ProvinceSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return Provinces;
        }

        public async Task<int> Count(ProvinceFilter filter)
        {
            IQueryable <ProvinceDAO> ProvinceDAOs = ERPContext.Province;
            ProvinceDAOs = DynamicFilter(ProvinceDAOs, filter);
            return await ProvinceDAOs.CountAsync();
        }

        public async Task<List<Province>> List(ProvinceFilter filter)
        {
            if (filter == null) return new List<Province>();
            IQueryable<ProvinceDAO> ProvinceDAOs = ERPContext.Province;
            ProvinceDAOs = DynamicFilter(ProvinceDAOs, filter);
            ProvinceDAOs = DynamicOrder(ProvinceDAOs, filter);
            var Provinces = await DynamicSelect(ProvinceDAOs, filter);
            return Provinces;
        }

        public async Task<Province> Get(Guid Id)
        {
            Province Province = await ERPContext.Province.Where(l => l.Id == Id).Select(ProvinceDAO => new Province()
            {
                 
                Id = ProvinceDAO.Id,
                Name = ProvinceDAO.Name,
                BusinessGroupId = ProvinceDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return Province;
        }

        public async Task<bool> Create(Province Province)
        {
            ProvinceDAO ProvinceDAO = new ProvinceDAO();
            
            ProvinceDAO.Id = Province.Id;
            ProvinceDAO.Name = Province.Name;
            ProvinceDAO.BusinessGroupId = Province.BusinessGroupId;
            ProvinceDAO.Disabled = false;
            
            await ERPContext.Province.AddAsync(ProvinceDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Province Province)
        {
            ProvinceDAO ProvinceDAO = ERPContext.Province.Where(b => b.Id == Province.Id).FirstOrDefault();
            
            ProvinceDAO.Id = Province.Id;
            ProvinceDAO.Name = Province.Name;
            ProvinceDAO.BusinessGroupId = Province.BusinessGroupId;
            ProvinceDAO.Disabled = false;
            ERPContext.Province.Update(ProvinceDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            ProvinceDAO ProvinceDAO = await ERPContext.Province.Where(x => x.Id == Id).FirstOrDefaultAsync();
            ProvinceDAO.Disabled = true;
            ERPContext.Province.Update(ProvinceDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
