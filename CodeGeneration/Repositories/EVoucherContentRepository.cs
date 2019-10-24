
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
    public interface IEVoucherContentRepository
    {
        Task<int> Count(EVoucherContentFilter EVoucherContentFilter);
        Task<List<EVoucherContent>> List(EVoucherContentFilter EVoucherContentFilter);
        Task<EVoucherContent> Get(long Id);
        Task<bool> Create(EVoucherContent EVoucherContent);
        Task<bool> Update(EVoucherContent EVoucherContent);
        Task<bool> Delete(EVoucherContent EVoucherContent);
        
    }
    public class EVoucherContentRepository : IEVoucherContentRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public EVoucherContentRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<EVoucherContentDAO> DynamicFilter(IQueryable<EVoucherContentDAO> query, EVoucherContentFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.EVourcherId != null)
                query = query.Where(q => q.EVourcherId, filter.EVourcherId);
            if (filter.UsedCode != null)
                query = query.Where(q => q.UsedCode, filter.UsedCode);
            if (filter.MerchantCode != null)
                query = query.Where(q => q.MerchantCode, filter.MerchantCode);
            if (filter.UsedDate != null)
                query = query.Where(q => q.UsedDate, filter.UsedDate);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<EVoucherContentDAO> DynamicOrder(IQueryable<EVoucherContentDAO> query,  EVoucherContentFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case EVoucherContentOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case EVoucherContentOrder.EVourcher:
                            query = query.OrderBy(q => q.EVourcher.Id);
                            break;
                        case EVoucherContentOrder.UsedCode:
                            query = query.OrderBy(q => q.UsedCode);
                            break;
                        case EVoucherContentOrder.MerchantCode:
                            query = query.OrderBy(q => q.MerchantCode);
                            break;
                        case EVoucherContentOrder.UsedDate:
                            query = query.OrderBy(q => q.UsedDate);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case EVoucherContentOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case EVoucherContentOrder.EVourcher:
                            query = query.OrderByDescending(q => q.EVourcher.Id);
                            break;
                        case EVoucherContentOrder.UsedCode:
                            query = query.OrderByDescending(q => q.UsedCode);
                            break;
                        case EVoucherContentOrder.MerchantCode:
                            query = query.OrderByDescending(q => q.MerchantCode);
                            break;
                        case EVoucherContentOrder.UsedDate:
                            query = query.OrderByDescending(q => q.UsedDate);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<EVoucherContent>> DynamicSelect(IQueryable<EVoucherContentDAO> query, EVoucherContentFilter filter)
        {
            List <EVoucherContent> EVoucherContents = await query.Select(q => new EVoucherContent()
            {
                
                Id = filter.Selects.Contains(EVoucherContentSelect.Id) ? q.Id : default(long),
                EVourcherId = filter.Selects.Contains(EVoucherContentSelect.EVourcher) ? q.EVourcherId : default(long),
                UsedCode = filter.Selects.Contains(EVoucherContentSelect.UsedCode) ? q.UsedCode : default(string),
                MerchantCode = filter.Selects.Contains(EVoucherContentSelect.MerchantCode) ? q.MerchantCode : default(string),
                UsedDate = filter.Selects.Contains(EVoucherContentSelect.UsedDate) ? q.UsedDate : default(DateTime?),
                EVourcher = filter.Selects.Contains(EVoucherContentSelect.EVourcher) && q.EVourcher != null ? new EVoucher
                {
                    
                    Id = q.EVourcher.Id,
                    CustomerId = q.EVourcher.CustomerId,
                    ProductId = q.EVourcher.ProductId,
                    Name = q.EVourcher.Name,
                    Start = q.EVourcher.Start,
                    End = q.EVourcher.End,
                    Quantity = q.EVourcher.Quantity,
                } : null,
            }).ToListAsync();
            return EVoucherContents;
        }

        public async Task<int> Count(EVoucherContentFilter filter)
        {
            IQueryable <EVoucherContentDAO> EVoucherContentDAOs = DataContext.EVoucherContent;
            EVoucherContentDAOs = DynamicFilter(EVoucherContentDAOs, filter);
            return await EVoucherContentDAOs.CountAsync();
        }

        public async Task<List<EVoucherContent>> List(EVoucherContentFilter filter)
        {
            if (filter == null) return new List<EVoucherContent>();
            IQueryable<EVoucherContentDAO> EVoucherContentDAOs = DataContext.EVoucherContent;
            EVoucherContentDAOs = DynamicFilter(EVoucherContentDAOs, filter);
            EVoucherContentDAOs = DynamicOrder(EVoucherContentDAOs, filter);
            var EVoucherContents = await DynamicSelect(EVoucherContentDAOs, filter);
            return EVoucherContents;
        }

        
        public async Task<EVoucherContent> Get(long Id)
        {
            EVoucherContent EVoucherContent = await DataContext.EVoucherContent.Where(x => x.Id == Id).Select(EVoucherContentDAO => new EVoucherContent()
            {
                 
                Id = EVoucherContentDAO.Id,
                EVourcherId = EVoucherContentDAO.EVourcherId,
                UsedCode = EVoucherContentDAO.UsedCode,
                MerchantCode = EVoucherContentDAO.MerchantCode,
                UsedDate = EVoucherContentDAO.UsedDate,
                EVourcher = EVoucherContentDAO.EVourcher == null ? null : new EVoucher
                {
                    
                    Id = EVoucherContentDAO.EVourcher.Id,
                    CustomerId = EVoucherContentDAO.EVourcher.CustomerId,
                    ProductId = EVoucherContentDAO.EVourcher.ProductId,
                    Name = EVoucherContentDAO.EVourcher.Name,
                    Start = EVoucherContentDAO.EVourcher.Start,
                    End = EVoucherContentDAO.EVourcher.End,
                    Quantity = EVoucherContentDAO.EVourcher.Quantity,
                },
            }).FirstOrDefaultAsync();
            return EVoucherContent;
        }

        public async Task<bool> Create(EVoucherContent EVoucherContent)
        {
            EVoucherContentDAO EVoucherContentDAO = new EVoucherContentDAO();
            
            EVoucherContentDAO.Id = EVoucherContent.Id;
            EVoucherContentDAO.EVourcherId = EVoucherContent.EVourcherId;
            EVoucherContentDAO.UsedCode = EVoucherContent.UsedCode;
            EVoucherContentDAO.MerchantCode = EVoucherContent.MerchantCode;
            EVoucherContentDAO.UsedDate = EVoucherContent.UsedDate;
            
            await DataContext.EVoucherContent.AddAsync(EVoucherContentDAO);
            await DataContext.SaveChangesAsync();
            EVoucherContent.Id = EVoucherContentDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(EVoucherContent EVoucherContent)
        {
            EVoucherContentDAO EVoucherContentDAO = DataContext.EVoucherContent.Where(x => x.Id == EVoucherContent.Id).FirstOrDefault();
            
            EVoucherContentDAO.Id = EVoucherContent.Id;
            EVoucherContentDAO.EVourcherId = EVoucherContent.EVourcherId;
            EVoucherContentDAO.UsedCode = EVoucherContent.UsedCode;
            EVoucherContentDAO.MerchantCode = EVoucherContent.MerchantCode;
            EVoucherContentDAO.UsedDate = EVoucherContent.UsedDate;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(EVoucherContent EVoucherContent)
        {
            EVoucherContentDAO EVoucherContentDAO = await DataContext.EVoucherContent.Where(x => x.Id == EVoucherContent.Id).FirstOrDefaultAsync();
            DataContext.EVoucherContent.Remove(EVoucherContentDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
