
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
    public interface IWardRepository
    {
        Task<int> Count(WardFilter WardFilter);
        Task<List<Ward>> List(WardFilter WardFilter);
        Task<Ward> Get(long Id);
        Task<bool> Create(Ward Ward);
        Task<bool> Update(Ward Ward);
        Task<bool> Delete(Ward Ward);
        
    }
    public class WardRepository : IWardRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public WardRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<WardDAO> DynamicFilter(IQueryable<WardDAO> query, WardFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.OrderNumber != null)
                query = query.Where(q => q.OrderNumber, filter.OrderNumber);
            if (filter.DistrictId != null)
                query = query.Where(q => q.DistrictId, filter.DistrictId);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<WardDAO> DynamicOrder(IQueryable<WardDAO> query,  WardFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case WardOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case WardOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case WardOrder.OrderNumber:
                            query = query.OrderBy(q => q.OrderNumber);
                            break;
                        case WardOrder.District:
                            query = query.OrderBy(q => q.District.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case WardOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case WardOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case WardOrder.OrderNumber:
                            query = query.OrderByDescending(q => q.OrderNumber);
                            break;
                        case WardOrder.District:
                            query = query.OrderByDescending(q => q.District.Id);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Ward>> DynamicSelect(IQueryable<WardDAO> query, WardFilter filter)
        {
            List <Ward> Wards = await query.Select(q => new Ward()
            {
                
                Id = filter.Selects.Contains(WardSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(WardSelect.Name) ? q.Name : default(string),
                OrderNumber = filter.Selects.Contains(WardSelect.OrderNumber) ? q.OrderNumber : default(long),
                DistrictId = filter.Selects.Contains(WardSelect.District) ? q.DistrictId : default(long),
                District = filter.Selects.Contains(WardSelect.District) && q.District != null ? new District
                {
                    
                    Id = q.District.Id,
                    Name = q.District.Name,
                    OrderNumber = q.District.OrderNumber,
                    ProvinceId = q.District.ProvinceId,
                } : null,
            }).ToListAsync();
            return Wards;
        }

        public async Task<int> Count(WardFilter filter)
        {
            IQueryable <WardDAO> WardDAOs = DataContext.Ward;
            WardDAOs = DynamicFilter(WardDAOs, filter);
            return await WardDAOs.CountAsync();
        }

        public async Task<List<Ward>> List(WardFilter filter)
        {
            if (filter == null) return new List<Ward>();
            IQueryable<WardDAO> WardDAOs = DataContext.Ward;
            WardDAOs = DynamicFilter(WardDAOs, filter);
            WardDAOs = DynamicOrder(WardDAOs, filter);
            var Wards = await DynamicSelect(WardDAOs, filter);
            return Wards;
        }

        
        public async Task<Ward> Get(long Id)
        {
            Ward Ward = await DataContext.Ward.Where(x => x.Id == Id).Select(WardDAO => new Ward()
            {
                 
                Id = WardDAO.Id,
                Name = WardDAO.Name,
                OrderNumber = WardDAO.OrderNumber,
                DistrictId = WardDAO.DistrictId,
                District = WardDAO.District == null ? null : new District
                {
                    
                    Id = WardDAO.District.Id,
                    Name = WardDAO.District.Name,
                    OrderNumber = WardDAO.District.OrderNumber,
                    ProvinceId = WardDAO.District.ProvinceId,
                },
            }).FirstOrDefaultAsync();
            return Ward;
        }

        public async Task<bool> Create(Ward Ward)
        {
            WardDAO WardDAO = new WardDAO();
            
            WardDAO.Id = Ward.Id;
            WardDAO.Name = Ward.Name;
            WardDAO.OrderNumber = Ward.OrderNumber;
            WardDAO.DistrictId = Ward.DistrictId;
            
            await DataContext.Ward.AddAsync(WardDAO);
            await DataContext.SaveChangesAsync();
            Ward.Id = WardDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(Ward Ward)
        {
            WardDAO WardDAO = DataContext.Ward.Where(x => x.Id == Ward.Id).FirstOrDefault();
            
            WardDAO.Id = Ward.Id;
            WardDAO.Name = Ward.Name;
            WardDAO.OrderNumber = Ward.OrderNumber;
            WardDAO.DistrictId = Ward.DistrictId;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Ward Ward)
        {
            WardDAO WardDAO = await DataContext.Ward.Where(x => x.Id == Ward.Id).FirstOrDefaultAsync();
            DataContext.Ward.Remove(WardDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
