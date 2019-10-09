
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
    public interface IFeatureRepository
    {
        Task<int> Count(FeatureFilter FeatureFilter);
        Task<List<Feature>> List(FeatureFilter FeatureFilter);
        Task<Feature> Get(Guid Id);
        Task<bool> Create(Feature Feature);
        Task<bool> Update(Feature Feature);
        Task<bool> Delete(Guid Id);
        
    }
    public class FeatureRepository : IFeatureRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public FeatureRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<FeatureDAO> DynamicFilter(IQueryable<FeatureDAO> query, FeatureFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            return query;
        }
        private IQueryable<FeatureDAO> DynamicOrder(IQueryable<FeatureDAO> query,  FeatureFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case FeatureOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case FeatureOrder.Name:
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
                        
                        case FeatureOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case FeatureOrder.Name:
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

        private async Task<List<Feature>> DynamicSelect(IQueryable<FeatureDAO> query, FeatureFilter filter)
        {
            List <Feature> Features = await query.Select(q => new Feature()
            {
                
                Id = filter.Selects.Contains(FeatureSelect.Id) ? q.Id : default(Guid),
                Code = filter.Selects.Contains(FeatureSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(FeatureSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return Features;
        }

        public async Task<int> Count(FeatureFilter filter)
        {
            IQueryable <FeatureDAO> FeatureDAOs = ERPContext.Feature;
            FeatureDAOs = DynamicFilter(FeatureDAOs, filter);
            return await FeatureDAOs.CountAsync();
        }

        public async Task<List<Feature>> List(FeatureFilter filter)
        {
            if (filter == null) return new List<Feature>();
            IQueryable<FeatureDAO> FeatureDAOs = ERPContext.Feature;
            FeatureDAOs = DynamicFilter(FeatureDAOs, filter);
            FeatureDAOs = DynamicOrder(FeatureDAOs, filter);
            var Features = await DynamicSelect(FeatureDAOs, filter);
            return Features;
        }

        public async Task<Feature> Get(Guid Id)
        {
            Feature Feature = await ERPContext.Feature.Where(l => l.Id == Id).Select(FeatureDAO => new Feature()
            {
                 
                Id = FeatureDAO.Id,
                Code = FeatureDAO.Code,
                Name = FeatureDAO.Name,
            }).FirstOrDefaultAsync();
            return Feature;
        }

        public async Task<bool> Create(Feature Feature)
        {
            FeatureDAO FeatureDAO = new FeatureDAO();
            
            FeatureDAO.Id = Feature.Id;
            FeatureDAO.Code = Feature.Code;
            FeatureDAO.Name = Feature.Name;
            FeatureDAO.Disabled = false;
            
            await ERPContext.Feature.AddAsync(FeatureDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Feature Feature)
        {
            FeatureDAO FeatureDAO = ERPContext.Feature.Where(b => b.Id == Feature.Id).FirstOrDefault();
            
            FeatureDAO.Id = Feature.Id;
            FeatureDAO.Code = Feature.Code;
            FeatureDAO.Name = Feature.Name;
            FeatureDAO.Disabled = false;
            ERPContext.Feature.Update(FeatureDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            FeatureDAO FeatureDAO = await ERPContext.Feature.Where(x => x.Id == Id).FirstOrDefaultAsync();
            FeatureDAO.Disabled = true;
            ERPContext.Feature.Update(FeatureDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
