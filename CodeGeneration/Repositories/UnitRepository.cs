
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
    public interface IUnitRepository
    {
        Task<int> Count(UnitFilter UnitFilter);
        Task<List<Unit>> List(UnitFilter UnitFilter);
        Task<Unit> Get(long Id);
        Task<bool> Create(Unit Unit);
        Task<bool> Update(Unit Unit);
        Task<bool> Delete(Unit Unit);
        
    }
    public class UnitRepository : IUnitRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public UnitRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<UnitDAO> DynamicFilter(IQueryable<UnitDAO> query, UnitFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.FirstVariationId != null)
                query = query.Where(q => q.FirstVariationId, filter.FirstVariationId);
            if (filter.SecondVariationId != null)
                query = query.Where(q => q.SecondVariationId, filter.SecondVariationId);
            if (filter.ThirdVariationId != null)
                query = query.Where(q => q.ThirdVariationId, filter.ThirdVariationId);
            if (filter.SKU != null)
                query = query.Where(q => q.SKU, filter.SKU);
            if (filter.Price != null)
                query = query.Where(q => q.Price, filter.Price);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<UnitDAO> DynamicOrder(IQueryable<UnitDAO> query,  UnitFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case UnitOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case UnitOrder.FirstVariation:
                            query = query.OrderBy(q => q.FirstVariation.Id);
                            break;
                        case UnitOrder.SecondVariation:
                            query = query.OrderBy(q => q.SecondVariation.Id);
                            break;
                        case UnitOrder.ThirdVariation:
                            query = query.OrderBy(q => q.ThirdVariation.Id);
                            break;
                        case UnitOrder.SKU:
                            query = query.OrderBy(q => q.SKU);
                            break;
                        case UnitOrder.Price:
                            query = query.OrderBy(q => q.Price);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case UnitOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case UnitOrder.FirstVariation:
                            query = query.OrderByDescending(q => q.FirstVariation.Id);
                            break;
                        case UnitOrder.SecondVariation:
                            query = query.OrderByDescending(q => q.SecondVariation.Id);
                            break;
                        case UnitOrder.ThirdVariation:
                            query = query.OrderByDescending(q => q.ThirdVariation.Id);
                            break;
                        case UnitOrder.SKU:
                            query = query.OrderByDescending(q => q.SKU);
                            break;
                        case UnitOrder.Price:
                            query = query.OrderByDescending(q => q.Price);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Unit>> DynamicSelect(IQueryable<UnitDAO> query, UnitFilter filter)
        {
            List <Unit> Units = await query.Select(q => new Unit()
            {
                
                Id = filter.Selects.Contains(UnitSelect.Id) ? q.Id : default(long),
                FirstVariationId = filter.Selects.Contains(UnitSelect.FirstVariation) ? q.FirstVariationId : default(long),
                SecondVariationId = filter.Selects.Contains(UnitSelect.SecondVariation) ? q.SecondVariationId : default(long?),
                ThirdVariationId = filter.Selects.Contains(UnitSelect.ThirdVariation) ? q.ThirdVariationId : default(long?),
                SKU = filter.Selects.Contains(UnitSelect.SKU) ? q.SKU : default(string),
                Price = filter.Selects.Contains(UnitSelect.Price) ? q.Price : default(long),
                FirstVariation = filter.Selects.Contains(UnitSelect.FirstVariation) && q.FirstVariation != null ? new Variation
                {
                    
                    Id = q.FirstVariation.Id,
                    Name = q.FirstVariation.Name,
                    VariationGroupingId = q.FirstVariation.VariationGroupingId,
                } : null,
                SecondVariation = filter.Selects.Contains(UnitSelect.SecondVariation) && q.SecondVariation != null ? new Variation
                {
                    
                    Id = q.SecondVariation.Id,
                    Name = q.SecondVariation.Name,
                    VariationGroupingId = q.SecondVariation.VariationGroupingId,
                } : null,
                ThirdVariation = filter.Selects.Contains(UnitSelect.ThirdVariation) && q.ThirdVariation != null ? new Variation
                {
                    
                    Id = q.ThirdVariation.Id,
                    Name = q.ThirdVariation.Name,
                    VariationGroupingId = q.ThirdVariation.VariationGroupingId,
                } : null,
            }).ToListAsync();
            return Units;
        }

        public async Task<int> Count(UnitFilter filter)
        {
            IQueryable <UnitDAO> UnitDAOs = DataContext.Unit;
            UnitDAOs = DynamicFilter(UnitDAOs, filter);
            return await UnitDAOs.CountAsync();
        }

        public async Task<List<Unit>> List(UnitFilter filter)
        {
            if (filter == null) return new List<Unit>();
            IQueryable<UnitDAO> UnitDAOs = DataContext.Unit;
            UnitDAOs = DynamicFilter(UnitDAOs, filter);
            UnitDAOs = DynamicOrder(UnitDAOs, filter);
            var Units = await DynamicSelect(UnitDAOs, filter);
            return Units;
        }

        
        public async Task<Unit> Get(long Id)
        {
            Unit Unit = await DataContext.Unit.Where(x => x.Id == Id).Select(UnitDAO => new Unit()
            {
                 
                Id = UnitDAO.Id,
                FirstVariationId = UnitDAO.FirstVariationId,
                SecondVariationId = UnitDAO.SecondVariationId,
                ThirdVariationId = UnitDAO.ThirdVariationId,
                SKU = UnitDAO.SKU,
                Price = UnitDAO.Price,
                FirstVariation = UnitDAO.FirstVariation == null ? null : new Variation
                {
                    
                    Id = UnitDAO.FirstVariation.Id,
                    Name = UnitDAO.FirstVariation.Name,
                    VariationGroupingId = UnitDAO.FirstVariation.VariationGroupingId,
                },
                SecondVariation = UnitDAO.SecondVariation == null ? null : new Variation
                {
                    
                    Id = UnitDAO.SecondVariation.Id,
                    Name = UnitDAO.SecondVariation.Name,
                    VariationGroupingId = UnitDAO.SecondVariation.VariationGroupingId,
                },
                ThirdVariation = UnitDAO.ThirdVariation == null ? null : new Variation
                {
                    
                    Id = UnitDAO.ThirdVariation.Id,
                    Name = UnitDAO.ThirdVariation.Name,
                    VariationGroupingId = UnitDAO.ThirdVariation.VariationGroupingId,
                },
            }).FirstOrDefaultAsync();
            return Unit;
        }

        public async Task<bool> Create(Unit Unit)
        {
            UnitDAO UnitDAO = new UnitDAO();
            
            UnitDAO.Id = Unit.Id;
            UnitDAO.FirstVariationId = Unit.FirstVariationId;
            UnitDAO.SecondVariationId = Unit.SecondVariationId;
            UnitDAO.ThirdVariationId = Unit.ThirdVariationId;
            UnitDAO.SKU = Unit.SKU;
            UnitDAO.Price = Unit.Price;
            
            await DataContext.Unit.AddAsync(UnitDAO);
            await DataContext.SaveChangesAsync();
            Unit.Id = UnitDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(Unit Unit)
        {
            UnitDAO UnitDAO = DataContext.Unit.Where(x => x.Id == Unit.Id).FirstOrDefault();
            
            UnitDAO.Id = Unit.Id;
            UnitDAO.FirstVariationId = Unit.FirstVariationId;
            UnitDAO.SecondVariationId = Unit.SecondVariationId;
            UnitDAO.ThirdVariationId = Unit.ThirdVariationId;
            UnitDAO.SKU = Unit.SKU;
            UnitDAO.Price = Unit.Price;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Unit Unit)
        {
            UnitDAO UnitDAO = await DataContext.Unit.Where(x => x.Id == Unit.Id).FirstOrDefaultAsync();
            DataContext.Unit.Remove(UnitDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
