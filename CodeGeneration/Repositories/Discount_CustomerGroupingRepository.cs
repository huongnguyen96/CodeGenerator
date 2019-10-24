
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
    public interface IDiscount_CustomerGroupingRepository
    {
        Task<int> Count(Discount_CustomerGroupingFilter Discount_CustomerGroupingFilter);
        Task<List<Discount_CustomerGrouping>> List(Discount_CustomerGroupingFilter Discount_CustomerGroupingFilter);
        Task<Discount_CustomerGrouping> Get(long DiscountId, long CustomerGroupingId);
        Task<bool> Create(Discount_CustomerGrouping Discount_CustomerGrouping);
        Task<bool> Update(Discount_CustomerGrouping Discount_CustomerGrouping);
        Task<bool> Delete(Discount_CustomerGrouping Discount_CustomerGrouping);
        
    }
    public class Discount_CustomerGroupingRepository : IDiscount_CustomerGroupingRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public Discount_CustomerGroupingRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<Discount_CustomerGroupingDAO> DynamicFilter(IQueryable<Discount_CustomerGroupingDAO> query, Discount_CustomerGroupingFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.DiscountId != null)
                query = query.Where(q => q.DiscountId, filter.DiscountId);
            if (filter.CustomerGroupingId != null)
                query = query.Where(q => q.CustomerGroupingId, filter.CustomerGroupingId);
            return query;
        }
        private IQueryable<Discount_CustomerGroupingDAO> DynamicOrder(IQueryable<Discount_CustomerGroupingDAO> query,  Discount_CustomerGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case Discount_CustomerGroupingOrder.Discount:
                            query = query.OrderBy(q => q.Discount.Id);
                            break;
                        case Discount_CustomerGroupingOrder.CustomerGrouping:
                            query = query.OrderBy(q => q.CustomerGrouping.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case Discount_CustomerGroupingOrder.Discount:
                            query = query.OrderByDescending(q => q.Discount.Id);
                            break;
                        case Discount_CustomerGroupingOrder.CustomerGrouping:
                            query = query.OrderByDescending(q => q.CustomerGrouping.Id);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Discount_CustomerGrouping>> DynamicSelect(IQueryable<Discount_CustomerGroupingDAO> query, Discount_CustomerGroupingFilter filter)
        {
            List <Discount_CustomerGrouping> Discount_CustomerGroupings = await query.Select(q => new Discount_CustomerGrouping()
            {
                
                DiscountId = filter.Selects.Contains(Discount_CustomerGroupingSelect.Discount) ? q.DiscountId : default(long),
                CustomerGroupingId = filter.Selects.Contains(Discount_CustomerGroupingSelect.CustomerGrouping) ? q.CustomerGroupingId : default(long),
                CustomerGrouping = filter.Selects.Contains(Discount_CustomerGroupingSelect.CustomerGrouping) && q.CustomerGrouping != null ? new CustomerGrouping
                {
                    
                    Id = q.CustomerGrouping.Id,
                    Name = q.CustomerGrouping.Name,
                } : null,
                Discount = filter.Selects.Contains(Discount_CustomerGroupingSelect.Discount) && q.Discount != null ? new Discount
                {
                    
                    Id = q.Discount.Id,
                    Name = q.Discount.Name,
                    Start = q.Discount.Start,
                    End = q.Discount.End,
                    Type = q.Discount.Type,
                } : null,
            }).ToListAsync();
            return Discount_CustomerGroupings;
        }

        public async Task<int> Count(Discount_CustomerGroupingFilter filter)
        {
            IQueryable <Discount_CustomerGroupingDAO> Discount_CustomerGroupingDAOs = DataContext.Discount_CustomerGrouping;
            Discount_CustomerGroupingDAOs = DynamicFilter(Discount_CustomerGroupingDAOs, filter);
            return await Discount_CustomerGroupingDAOs.CountAsync();
        }

        public async Task<List<Discount_CustomerGrouping>> List(Discount_CustomerGroupingFilter filter)
        {
            if (filter == null) return new List<Discount_CustomerGrouping>();
            IQueryable<Discount_CustomerGroupingDAO> Discount_CustomerGroupingDAOs = DataContext.Discount_CustomerGrouping;
            Discount_CustomerGroupingDAOs = DynamicFilter(Discount_CustomerGroupingDAOs, filter);
            Discount_CustomerGroupingDAOs = DynamicOrder(Discount_CustomerGroupingDAOs, filter);
            var Discount_CustomerGroupings = await DynamicSelect(Discount_CustomerGroupingDAOs, filter);
            return Discount_CustomerGroupings;
        }

        
        public async Task<Discount_CustomerGrouping> Get(long DiscountId, long CustomerGroupingId)
        {
            Discount_CustomerGrouping Discount_CustomerGrouping = await DataContext.Discount_CustomerGrouping.Where(x => x.DiscountId == DiscountId && x.CustomerGroupingId == CustomerGroupingId).Select(Discount_CustomerGroupingDAO => new Discount_CustomerGrouping()
            {
                 
                DiscountId = Discount_CustomerGroupingDAO.DiscountId,
                CustomerGroupingId = Discount_CustomerGroupingDAO.CustomerGroupingId,
                CustomerGrouping = Discount_CustomerGroupingDAO.CustomerGrouping == null ? null : new CustomerGrouping
                {
                    
                    Id = Discount_CustomerGroupingDAO.CustomerGrouping.Id,
                    Name = Discount_CustomerGroupingDAO.CustomerGrouping.Name,
                },
                Discount = Discount_CustomerGroupingDAO.Discount == null ? null : new Discount
                {
                    
                    Id = Discount_CustomerGroupingDAO.Discount.Id,
                    Name = Discount_CustomerGroupingDAO.Discount.Name,
                    Start = Discount_CustomerGroupingDAO.Discount.Start,
                    End = Discount_CustomerGroupingDAO.Discount.End,
                    Type = Discount_CustomerGroupingDAO.Discount.Type,
                },
            }).FirstOrDefaultAsync();
            return Discount_CustomerGrouping;
        }

        public async Task<bool> Create(Discount_CustomerGrouping Discount_CustomerGrouping)
        {
            Discount_CustomerGroupingDAO Discount_CustomerGroupingDAO = new Discount_CustomerGroupingDAO();
            
            Discount_CustomerGroupingDAO.DiscountId = Discount_CustomerGrouping.DiscountId;
            Discount_CustomerGroupingDAO.CustomerGroupingId = Discount_CustomerGrouping.CustomerGroupingId;
            
            await DataContext.Discount_CustomerGrouping.AddAsync(Discount_CustomerGroupingDAO);
            await DataContext.SaveChangesAsync();
            
            return true;
        }

        
        
        public async Task<bool> Update(Discount_CustomerGrouping Discount_CustomerGrouping)
        {
            Discount_CustomerGroupingDAO Discount_CustomerGroupingDAO = DataContext.Discount_CustomerGrouping.Where(x => x.DiscountId == Discount_CustomerGrouping.DiscountId && x.CustomerGroupingId == Discount_CustomerGrouping.CustomerGroupingId).FirstOrDefault();
            
            Discount_CustomerGroupingDAO.DiscountId = Discount_CustomerGrouping.DiscountId;
            Discount_CustomerGroupingDAO.CustomerGroupingId = Discount_CustomerGrouping.CustomerGroupingId;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Discount_CustomerGrouping Discount_CustomerGrouping)
        {
            Discount_CustomerGroupingDAO Discount_CustomerGroupingDAO = await DataContext.Discount_CustomerGrouping.Where(x => x.DiscountId == Discount_CustomerGrouping.DiscountId && x.CustomerGroupingId == Discount_CustomerGrouping.CustomerGroupingId).FirstOrDefaultAsync();
            DataContext.Discount_CustomerGrouping.Remove(Discount_CustomerGroupingDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
