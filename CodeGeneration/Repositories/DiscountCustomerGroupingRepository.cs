
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
    public interface IDiscountCustomerGroupingRepository
    {
        Task<int> Count(DiscountCustomerGroupingFilter DiscountCustomerGroupingFilter);
        Task<List<DiscountCustomerGrouping>> List(DiscountCustomerGroupingFilter DiscountCustomerGroupingFilter);
        Task<DiscountCustomerGrouping> Get(long Id);
        Task<bool> Create(DiscountCustomerGrouping DiscountCustomerGrouping);
        Task<bool> Update(DiscountCustomerGrouping DiscountCustomerGrouping);
        Task<bool> Delete(DiscountCustomerGrouping DiscountCustomerGrouping);
        
    }
    public class DiscountCustomerGroupingRepository : IDiscountCustomerGroupingRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public DiscountCustomerGroupingRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<DiscountCustomerGroupingDAO> DynamicFilter(IQueryable<DiscountCustomerGroupingDAO> query, DiscountCustomerGroupingFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.DiscountId != null)
                query = query.Where(q => q.DiscountId, filter.DiscountId);
            if (filter.CustomerGroupingCode != null)
                query = query.Where(q => q.CustomerGroupingCode, filter.CustomerGroupingCode);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<DiscountCustomerGroupingDAO> DynamicOrder(IQueryable<DiscountCustomerGroupingDAO> query,  DiscountCustomerGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case DiscountCustomerGroupingOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case DiscountCustomerGroupingOrder.Discount:
                            query = query.OrderBy(q => q.Discount.Id);
                            break;
                        case DiscountCustomerGroupingOrder.CustomerGroupingCode:
                            query = query.OrderBy(q => q.CustomerGroupingCode);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case DiscountCustomerGroupingOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case DiscountCustomerGroupingOrder.Discount:
                            query = query.OrderByDescending(q => q.Discount.Id);
                            break;
                        case DiscountCustomerGroupingOrder.CustomerGroupingCode:
                            query = query.OrderByDescending(q => q.CustomerGroupingCode);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<DiscountCustomerGrouping>> DynamicSelect(IQueryable<DiscountCustomerGroupingDAO> query, DiscountCustomerGroupingFilter filter)
        {
            List <DiscountCustomerGrouping> DiscountCustomerGroupings = await query.Select(q => new DiscountCustomerGrouping()
            {
                
                Id = filter.Selects.Contains(DiscountCustomerGroupingSelect.Id) ? q.Id : default(long),
                DiscountId = filter.Selects.Contains(DiscountCustomerGroupingSelect.Discount) ? q.DiscountId : default(long),
                CustomerGroupingCode = filter.Selects.Contains(DiscountCustomerGroupingSelect.CustomerGroupingCode) ? q.CustomerGroupingCode : default(string),
                Discount = filter.Selects.Contains(DiscountCustomerGroupingSelect.Discount) && q.Discount != null ? new Discount
                {
                    
                    Id = q.Discount.Id,
                    Name = q.Discount.Name,
                    Start = q.Discount.Start,
                    End = q.Discount.End,
                    Type = q.Discount.Type,
                } : null,
            }).ToListAsync();
            return DiscountCustomerGroupings;
        }

        public async Task<int> Count(DiscountCustomerGroupingFilter filter)
        {
            IQueryable <DiscountCustomerGroupingDAO> DiscountCustomerGroupingDAOs = DataContext.DiscountCustomerGrouping;
            DiscountCustomerGroupingDAOs = DynamicFilter(DiscountCustomerGroupingDAOs, filter);
            return await DiscountCustomerGroupingDAOs.CountAsync();
        }

        public async Task<List<DiscountCustomerGrouping>> List(DiscountCustomerGroupingFilter filter)
        {
            if (filter == null) return new List<DiscountCustomerGrouping>();
            IQueryable<DiscountCustomerGroupingDAO> DiscountCustomerGroupingDAOs = DataContext.DiscountCustomerGrouping;
            DiscountCustomerGroupingDAOs = DynamicFilter(DiscountCustomerGroupingDAOs, filter);
            DiscountCustomerGroupingDAOs = DynamicOrder(DiscountCustomerGroupingDAOs, filter);
            var DiscountCustomerGroupings = await DynamicSelect(DiscountCustomerGroupingDAOs, filter);
            return DiscountCustomerGroupings;
        }

        
        public async Task<DiscountCustomerGrouping> Get(long Id)
        {
            DiscountCustomerGrouping DiscountCustomerGrouping = await DataContext.DiscountCustomerGrouping.Where(x => x.Id == Id).Select(DiscountCustomerGroupingDAO => new DiscountCustomerGrouping()
            {
                 
                Id = DiscountCustomerGroupingDAO.Id,
                DiscountId = DiscountCustomerGroupingDAO.DiscountId,
                CustomerGroupingCode = DiscountCustomerGroupingDAO.CustomerGroupingCode,
                Discount = DiscountCustomerGroupingDAO.Discount == null ? null : new Discount
                {
                    
                    Id = DiscountCustomerGroupingDAO.Discount.Id,
                    Name = DiscountCustomerGroupingDAO.Discount.Name,
                    Start = DiscountCustomerGroupingDAO.Discount.Start,
                    End = DiscountCustomerGroupingDAO.Discount.End,
                    Type = DiscountCustomerGroupingDAO.Discount.Type,
                },
            }).FirstOrDefaultAsync();
            return DiscountCustomerGrouping;
        }

        public async Task<bool> Create(DiscountCustomerGrouping DiscountCustomerGrouping)
        {
            DiscountCustomerGroupingDAO DiscountCustomerGroupingDAO = new DiscountCustomerGroupingDAO();
            
            DiscountCustomerGroupingDAO.Id = DiscountCustomerGrouping.Id;
            DiscountCustomerGroupingDAO.DiscountId = DiscountCustomerGrouping.DiscountId;
            DiscountCustomerGroupingDAO.CustomerGroupingCode = DiscountCustomerGrouping.CustomerGroupingCode;
            
            await DataContext.DiscountCustomerGrouping.AddAsync(DiscountCustomerGroupingDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        
        public async Task<bool> Update(DiscountCustomerGrouping DiscountCustomerGrouping)
        {
            DiscountCustomerGroupingDAO DiscountCustomerGroupingDAO = DataContext.DiscountCustomerGrouping.Where(x => x.Id == DiscountCustomerGrouping.Id).FirstOrDefault();
            
            DiscountCustomerGroupingDAO.Id = DiscountCustomerGrouping.Id;
            DiscountCustomerGroupingDAO.DiscountId = DiscountCustomerGrouping.DiscountId;
            DiscountCustomerGroupingDAO.CustomerGroupingCode = DiscountCustomerGrouping.CustomerGroupingCode;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(DiscountCustomerGrouping DiscountCustomerGrouping)
        {
            DiscountCustomerGroupingDAO DiscountCustomerGroupingDAO = await DataContext.DiscountCustomerGrouping.Where(x => x.Id == DiscountCustomerGrouping.Id).FirstOrDefaultAsync();
            DataContext.DiscountCustomerGrouping.Remove(DiscountCustomerGroupingDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
