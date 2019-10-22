
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
    public interface IDistrictRepository
    {
        Task<int> Count(DistrictFilter DistrictFilter);
        Task<List<District>> List(DistrictFilter DistrictFilter);
        Task<District> Get(long Id);
        Task<bool> Create(District District);
        Task<bool> Update(District District);
        Task<bool> Delete(District District);
        
    }
    public class DistrictRepository : IDistrictRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public DistrictRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<DistrictDAO> DynamicFilter(IQueryable<DistrictDAO> query, DistrictFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.OrderNumber != null)
                query = query.Where(q => q.OrderNumber, filter.OrderNumber);
            if (filter.ProvinceId != null)
                query = query.Where(q => q.ProvinceId, filter.ProvinceId);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<DistrictDAO> DynamicOrder(IQueryable<DistrictDAO> query,  DistrictFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case DistrictOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case DistrictOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case DistrictOrder.OrderNumber:
                            query = query.OrderBy(q => q.OrderNumber);
                            break;
                        case DistrictOrder.Province:
                            query = query.OrderBy(q => q.Province.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case DistrictOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case DistrictOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case DistrictOrder.OrderNumber:
                            query = query.OrderByDescending(q => q.OrderNumber);
                            break;
                        case DistrictOrder.Province:
                            query = query.OrderByDescending(q => q.Province.Id);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<District>> DynamicSelect(IQueryable<DistrictDAO> query, DistrictFilter filter)
        {
            List <District> Districts = await query.Select(q => new District()
            {
                
                Id = filter.Selects.Contains(DistrictSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(DistrictSelect.Name) ? q.Name : default(string),
                OrderNumber = filter.Selects.Contains(DistrictSelect.OrderNumber) ? q.OrderNumber : default(long),
                ProvinceId = filter.Selects.Contains(DistrictSelect.Province) ? q.ProvinceId : default(long),
                Province = filter.Selects.Contains(DistrictSelect.Province) && q.Province != null ? new Province
                {
                    
                    Id = q.Province.Id,
                    Name = q.Province.Name,
                    OrderNumber = q.Province.OrderNumber,
                } : null,
            }).ToListAsync();
            return Districts;
        }

        public async Task<int> Count(DistrictFilter filter)
        {
            IQueryable <DistrictDAO> DistrictDAOs = DataContext.District;
            DistrictDAOs = DynamicFilter(DistrictDAOs, filter);
            return await DistrictDAOs.CountAsync();
        }

        public async Task<List<District>> List(DistrictFilter filter)
        {
            if (filter == null) return new List<District>();
            IQueryable<DistrictDAO> DistrictDAOs = DataContext.District;
            DistrictDAOs = DynamicFilter(DistrictDAOs, filter);
            DistrictDAOs = DynamicOrder(DistrictDAOs, filter);
            var Districts = await DynamicSelect(DistrictDAOs, filter);
            return Districts;
        }

        
        public async Task<District> Get(long Id)
        {
            District District = await DataContext.District.Where(x => x.Id == Id).Select(DistrictDAO => new District()
            {
                 
                Id = DistrictDAO.Id,
                Name = DistrictDAO.Name,
                OrderNumber = DistrictDAO.OrderNumber,
                ProvinceId = DistrictDAO.ProvinceId,
                Province = DistrictDAO.Province == null ? null : new Province
                {
                    
                    Id = DistrictDAO.Province.Id,
                    Name = DistrictDAO.Province.Name,
                    OrderNumber = DistrictDAO.Province.OrderNumber,
                },
            }).FirstOrDefaultAsync();
            return District;
        }

        public async Task<bool> Create(District District)
        {
            DistrictDAO DistrictDAO = new DistrictDAO();
            
            DistrictDAO.Id = District.Id;
            DistrictDAO.Name = District.Name;
            DistrictDAO.OrderNumber = District.OrderNumber;
            DistrictDAO.ProvinceId = District.ProvinceId;
            
            await DataContext.District.AddAsync(DistrictDAO);
            await DataContext.SaveChangesAsync();
            District.Id = DistrictDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(District District)
        {
            DistrictDAO DistrictDAO = DataContext.District.Where(x => x.Id == District.Id).FirstOrDefault();
            
            DistrictDAO.Id = District.Id;
            DistrictDAO.Name = District.Name;
            DistrictDAO.OrderNumber = District.OrderNumber;
            DistrictDAO.ProvinceId = District.ProvinceId;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(District District)
        {
            DistrictDAO DistrictDAO = await DataContext.District.Where(x => x.Id == District.Id).FirstOrDefaultAsync();
            DataContext.District.Remove(DistrictDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
