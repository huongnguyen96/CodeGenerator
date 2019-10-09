
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
    public interface IPaymentTermRepository
    {
        Task<int> Count(PaymentTermFilter PaymentTermFilter);
        Task<List<PaymentTerm>> List(PaymentTermFilter PaymentTermFilter);
        Task<PaymentTerm> Get(Guid Id);
        Task<bool> Create(PaymentTerm PaymentTerm);
        Task<bool> Update(PaymentTerm PaymentTerm);
        Task<bool> Delete(Guid Id);
        
    }
    public class PaymentTermRepository : IPaymentTermRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public PaymentTermRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<PaymentTermDAO> DynamicFilter(IQueryable<PaymentTermDAO> query, PaymentTermFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.SetOfBookId != null)
                query = query.Where(q => q.SetOfBookId, filter.SetOfBookId);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.DueInDays.HasValue)
                query = query.Where(q => q.DueInDays.HasValue && q.DueInDays.Value == filter.DueInDays.Value);
            if (filter.DueInDays != null)
                query = query.Where(q => q.DueInDays, filter.DueInDays);
            if (filter.DiscountPeriod.HasValue)
                query = query.Where(q => q.DiscountPeriod.HasValue && q.DiscountPeriod.Value == filter.DiscountPeriod.Value);
            if (filter.DiscountPeriod != null)
                query = query.Where(q => q.DiscountPeriod, filter.DiscountPeriod);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.DiscountRate.HasValue)
                query = query.Where(q => q.DiscountRate.HasValue && q.DiscountRate.Value == filter.DiscountRate.Value);
            if (filter.DiscountRate != null)
                query = query.Where(q => q.DiscountRate, filter.DiscountRate);
            if (filter.Sequence != null)
                query = query.Where(q => q.Sequence, filter.Sequence);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<PaymentTermDAO> DynamicOrder(IQueryable<PaymentTermDAO> query,  PaymentTermFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case PaymentTermOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case PaymentTermOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case PaymentTermOrder.DueInDays:
                            query = query.OrderBy(q => q.DueInDays);
                            break;
                        case PaymentTermOrder.DiscountPeriod:
                            query = query.OrderBy(q => q.DiscountPeriod);
                            break;
                        case PaymentTermOrder.DiscountRate:
                            query = query.OrderBy(q => q.DiscountRate);
                            break;
                        case PaymentTermOrder.Sequence:
                            query = query.OrderBy(q => q.Sequence);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case PaymentTermOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case PaymentTermOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case PaymentTermOrder.DueInDays:
                            query = query.OrderByDescending(q => q.DueInDays);
                            break;
                        case PaymentTermOrder.DiscountPeriod:
                            query = query.OrderByDescending(q => q.DiscountPeriod);
                            break;
                        case PaymentTermOrder.DiscountRate:
                            query = query.OrderByDescending(q => q.DiscountRate);
                            break;
                        case PaymentTermOrder.Sequence:
                            query = query.OrderByDescending(q => q.Sequence);
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

        private async Task<List<PaymentTerm>> DynamicSelect(IQueryable<PaymentTermDAO> query, PaymentTermFilter filter)
        {
            List <PaymentTerm> PaymentTerms = await query.Select(q => new PaymentTerm()
            {
                
                Id = filter.Selects.Contains(PaymentTermSelect.Id) ? q.Id : default(Guid),
                SetOfBookId = filter.Selects.Contains(PaymentTermSelect.SetOfBook) ? q.SetOfBookId : default(Guid),
                Code = filter.Selects.Contains(PaymentTermSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(PaymentTermSelect.Name) ? q.Name : default(string),
                DueInDays = filter.Selects.Contains(PaymentTermSelect.DueInDays) ? q.DueInDays : default(Guid?),
                DiscountPeriod = filter.Selects.Contains(PaymentTermSelect.DiscountPeriod) ? q.DiscountPeriod : default(Guid?),
                DiscountRate = filter.Selects.Contains(PaymentTermSelect.DiscountRate) ? q.DiscountRate : default(Guid?),
                Sequence = filter.Selects.Contains(PaymentTermSelect.Sequence) ? q.Sequence : default(int),
                BusinessGroupId = filter.Selects.Contains(PaymentTermSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return PaymentTerms;
        }

        public async Task<int> Count(PaymentTermFilter filter)
        {
            IQueryable <PaymentTermDAO> PaymentTermDAOs = ERPContext.PaymentTerm;
            PaymentTermDAOs = DynamicFilter(PaymentTermDAOs, filter);
            return await PaymentTermDAOs.CountAsync();
        }

        public async Task<List<PaymentTerm>> List(PaymentTermFilter filter)
        {
            if (filter == null) return new List<PaymentTerm>();
            IQueryable<PaymentTermDAO> PaymentTermDAOs = ERPContext.PaymentTerm;
            PaymentTermDAOs = DynamicFilter(PaymentTermDAOs, filter);
            PaymentTermDAOs = DynamicOrder(PaymentTermDAOs, filter);
            var PaymentTerms = await DynamicSelect(PaymentTermDAOs, filter);
            return PaymentTerms;
        }

        public async Task<PaymentTerm> Get(Guid Id)
        {
            PaymentTerm PaymentTerm = await ERPContext.PaymentTerm.Where(l => l.Id == Id).Select(PaymentTermDAO => new PaymentTerm()
            {
                 
                Id = PaymentTermDAO.Id,
                SetOfBookId = PaymentTermDAO.SetOfBookId,
                Code = PaymentTermDAO.Code,
                Name = PaymentTermDAO.Name,
                DueInDays = PaymentTermDAO.DueInDays,
                DiscountPeriod = PaymentTermDAO.DiscountPeriod,
                DiscountRate = PaymentTermDAO.DiscountRate,
                Sequence = PaymentTermDAO.Sequence,
                BusinessGroupId = PaymentTermDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return PaymentTerm;
        }

        public async Task<bool> Create(PaymentTerm PaymentTerm)
        {
            PaymentTermDAO PaymentTermDAO = new PaymentTermDAO();
            
            PaymentTermDAO.Id = PaymentTerm.Id;
            PaymentTermDAO.SetOfBookId = PaymentTerm.SetOfBookId;
            PaymentTermDAO.Code = PaymentTerm.Code;
            PaymentTermDAO.Name = PaymentTerm.Name;
            PaymentTermDAO.DueInDays = PaymentTerm.DueInDays;
            PaymentTermDAO.DiscountPeriod = PaymentTerm.DiscountPeriod;
            PaymentTermDAO.DiscountRate = PaymentTerm.DiscountRate;
            PaymentTermDAO.Sequence = PaymentTerm.Sequence;
            PaymentTermDAO.BusinessGroupId = PaymentTerm.BusinessGroupId;
            PaymentTermDAO.Disabled = false;
            
            await ERPContext.PaymentTerm.AddAsync(PaymentTermDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(PaymentTerm PaymentTerm)
        {
            PaymentTermDAO PaymentTermDAO = ERPContext.PaymentTerm.Where(b => b.Id == PaymentTerm.Id).FirstOrDefault();
            
            PaymentTermDAO.Id = PaymentTerm.Id;
            PaymentTermDAO.SetOfBookId = PaymentTerm.SetOfBookId;
            PaymentTermDAO.Code = PaymentTerm.Code;
            PaymentTermDAO.Name = PaymentTerm.Name;
            PaymentTermDAO.DueInDays = PaymentTerm.DueInDays;
            PaymentTermDAO.DiscountPeriod = PaymentTerm.DiscountPeriod;
            PaymentTermDAO.DiscountRate = PaymentTerm.DiscountRate;
            PaymentTermDAO.Sequence = PaymentTerm.Sequence;
            PaymentTermDAO.BusinessGroupId = PaymentTerm.BusinessGroupId;
            PaymentTermDAO.Disabled = false;
            ERPContext.PaymentTerm.Update(PaymentTermDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            PaymentTermDAO PaymentTermDAO = await ERPContext.PaymentTerm.Where(x => x.Id == Id).FirstOrDefaultAsync();
            PaymentTermDAO.Disabled = true;
            ERPContext.PaymentTerm.Update(PaymentTermDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
