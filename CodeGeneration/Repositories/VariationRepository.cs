
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
    public interface IVariationRepository
    {
        Task<int> Count(VariationFilter VariationFilter);
        Task<List<Variation>> List(VariationFilter VariationFilter);
        Task<Variation> Get(long Id);
        Task<bool> Create(Variation Variation);
        Task<bool> Update(Variation Variation);
        Task<bool> Delete(Variation Variation);
        
    }
    public class VariationRepository : IVariationRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public VariationRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<VariationDAO> DynamicFilter(IQueryable<VariationDAO> query, VariationFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.VariationGroupingId != null)
                query = query.Where(q => q.VariationGroupingId, filter.VariationGroupingId);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<VariationDAO> DynamicOrder(IQueryable<VariationDAO> query,  VariationFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case VariationOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case VariationOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case VariationOrder.VariationGrouping:
                            query = query.OrderBy(q => q.VariationGrouping.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case VariationOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case VariationOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case VariationOrder.VariationGrouping:
                            query = query.OrderByDescending(q => q.VariationGrouping.Id);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Variation>> DynamicSelect(IQueryable<VariationDAO> query, VariationFilter filter)
        {
            List <Variation> Variations = await query.Select(q => new Variation()
            {
                
                Id = filter.Selects.Contains(VariationSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(VariationSelect.Name) ? q.Name : default(string),
                VariationGroupingId = filter.Selects.Contains(VariationSelect.VariationGrouping) ? q.VariationGroupingId : default(long),
                VariationGrouping = filter.Selects.Contains(VariationSelect.VariationGrouping) && q.VariationGrouping != null ? new VariationGrouping
                {
                    
                    Id = q.VariationGrouping.Id,
                    Name = q.VariationGrouping.Name,
                    ItemId = q.VariationGrouping.ItemId,
                } : null,
            }).ToListAsync();
            return Variations;
        }

        public async Task<int> Count(VariationFilter filter)
        {
            IQueryable <VariationDAO> VariationDAOs = DataContext.Variation;
            VariationDAOs = DynamicFilter(VariationDAOs, filter);
            return await VariationDAOs.CountAsync();
        }

        public async Task<List<Variation>> List(VariationFilter filter)
        {
            if (filter == null) return new List<Variation>();
            IQueryable<VariationDAO> VariationDAOs = DataContext.Variation;
            VariationDAOs = DynamicFilter(VariationDAOs, filter);
            VariationDAOs = DynamicOrder(VariationDAOs, filter);
            var Variations = await DynamicSelect(VariationDAOs, filter);
            return Variations;
        }

        
        public async Task<Variation> Get(long Id)
        {
            Variation Variation = await DataContext.Variation.Where(x => x.Id == Id).Select(VariationDAO => new Variation()
            {
                 
                Id = VariationDAO.Id,
                Name = VariationDAO.Name,
                VariationGroupingId = VariationDAO.VariationGroupingId,
                VariationGrouping = VariationDAO.VariationGrouping == null ? null : new VariationGrouping
                {
                    
                    Id = VariationDAO.VariationGrouping.Id,
                    Name = VariationDAO.VariationGrouping.Name,
                    ItemId = VariationDAO.VariationGrouping.ItemId,
                },
            }).FirstOrDefaultAsync();
            return Variation;
        }

        public async Task<bool> Create(Variation Variation)
        {
            VariationDAO VariationDAO = new VariationDAO();
            
            VariationDAO.Id = Variation.Id;
            VariationDAO.Name = Variation.Name;
            VariationDAO.VariationGroupingId = Variation.VariationGroupingId;
            
            await DataContext.Variation.AddAsync(VariationDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        
        public async Task<bool> Update(Variation Variation)
        {
            VariationDAO VariationDAO = DataContext.Variation.Where(x => x.Id == Variation.Id).FirstOrDefault();
            
            VariationDAO.Id = Variation.Id;
            VariationDAO.Name = Variation.Name;
            VariationDAO.VariationGroupingId = Variation.VariationGroupingId;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Variation Variation)
        {
            VariationDAO VariationDAO = await DataContext.Variation.Where(x => x.Id == Variation.Id).FirstOrDefaultAsync();
            DataContext.Variation.Remove(VariationDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
