
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
    public interface IDiscountRepository
    {
        Task<int> Count(DiscountFilter DiscountFilter);
        Task<List<Discount>> List(DiscountFilter DiscountFilter);
        Task<Discount> Get(long Id);
        Task<bool> Create(Discount Discount);
        Task<bool> Update(Discount Discount);
        Task<bool> Delete(Discount Discount);
        
    }
    public class DiscountRepository : IDiscountRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public DiscountRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<DiscountDAO> DynamicFilter(IQueryable<DiscountDAO> query, DiscountFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Start != null)
                query = query.Where(q => q.Start, filter.Start);
            if (filter.End != null)
                query = query.Where(q => q.End, filter.End);
            if (filter.Type != null)
                query = query.Where(q => q.Type, filter.Type);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<DiscountDAO> DynamicOrder(IQueryable<DiscountDAO> query,  DiscountFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case DiscountOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case DiscountOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case DiscountOrder.Start:
                            query = query.OrderBy(q => q.Start);
                            break;
                        case DiscountOrder.End:
                            query = query.OrderBy(q => q.End);
                            break;
                        case DiscountOrder.Type:
                            query = query.OrderBy(q => q.Type);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case DiscountOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case DiscountOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case DiscountOrder.Start:
                            query = query.OrderByDescending(q => q.Start);
                            break;
                        case DiscountOrder.End:
                            query = query.OrderByDescending(q => q.End);
                            break;
                        case DiscountOrder.Type:
                            query = query.OrderByDescending(q => q.Type);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Discount>> DynamicSelect(IQueryable<DiscountDAO> query, DiscountFilter filter)
        {
            List <Discount> Discounts = await query.Select(q => new Discount()
            {
                
                Id = filter.Selects.Contains(DiscountSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(DiscountSelect.Name) ? q.Name : default(string),
                Start = filter.Selects.Contains(DiscountSelect.Start) ? q.Start : default(DateTime),
                End = filter.Selects.Contains(DiscountSelect.End) ? q.End : default(DateTime),
                Type = filter.Selects.Contains(DiscountSelect.Type) ? q.Type : default(string),
            }).ToListAsync();
            return Discounts;
        }

        public async Task<int> Count(DiscountFilter filter)
        {
            IQueryable <DiscountDAO> DiscountDAOs = DataContext.Discount;
            DiscountDAOs = DynamicFilter(DiscountDAOs, filter);
            return await DiscountDAOs.CountAsync();
        }

        public async Task<List<Discount>> List(DiscountFilter filter)
        {
            if (filter == null) return new List<Discount>();
            IQueryable<DiscountDAO> DiscountDAOs = DataContext.Discount;
            DiscountDAOs = DynamicFilter(DiscountDAOs, filter);
            DiscountDAOs = DynamicOrder(DiscountDAOs, filter);
            var Discounts = await DynamicSelect(DiscountDAOs, filter);
            return Discounts;
        }

        
        public async Task<Discount> Get(long Id)
        {
            Discount Discount = await DataContext.Discount.Where(x => x.Id == Id).Select(DiscountDAO => new Discount()
            {
                 
                Id = DiscountDAO.Id,
                Name = DiscountDAO.Name,
                Start = DiscountDAO.Start,
                End = DiscountDAO.End,
                Type = DiscountDAO.Type,
            }).FirstOrDefaultAsync();
            return Discount;
        }

        public async Task<bool> Create(Discount Discount)
        {
            DiscountDAO DiscountDAO = new DiscountDAO();
            
            DiscountDAO.Id = Discount.Id;
            DiscountDAO.Name = Discount.Name;
            DiscountDAO.Start = Discount.Start;
            DiscountDAO.End = Discount.End;
            DiscountDAO.Type = Discount.Type;
            
            await DataContext.Discount.AddAsync(DiscountDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        
        public async Task<bool> Update(Discount Discount)
        {
            DiscountDAO DiscountDAO = DataContext.Discount.Where(x => x.Id == Discount.Id).FirstOrDefault();
            
            DiscountDAO.Id = Discount.Id;
            DiscountDAO.Name = Discount.Name;
            DiscountDAO.Start = Discount.Start;
            DiscountDAO.End = Discount.End;
            DiscountDAO.Type = Discount.Type;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Discount Discount)
        {
            DiscountDAO DiscountDAO = await DataContext.Discount.Where(x => x.Id == Discount.Id).FirstOrDefaultAsync();
            DataContext.Discount.Remove(DiscountDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
