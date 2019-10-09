
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
    public interface IPaymentMethodRepository
    {
        Task<int> Count(PaymentMethodFilter PaymentMethodFilter);
        Task<List<PaymentMethod>> List(PaymentMethodFilter PaymentMethodFilter);
        Task<PaymentMethod> Get(Guid Id);
        Task<bool> Create(PaymentMethod PaymentMethod);
        Task<bool> Update(PaymentMethod PaymentMethod);
        Task<bool> Delete(Guid Id);
        
    }
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public PaymentMethodRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<PaymentMethodDAO> DynamicFilter(IQueryable<PaymentMethodDAO> query, PaymentMethodFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.SetOfBookId != null)
                query = query.Where(q => q.SetOfBookId, filter.SetOfBookId);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<PaymentMethodDAO> DynamicOrder(IQueryable<PaymentMethodDAO> query,  PaymentMethodFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case PaymentMethodOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case PaymentMethodOrder.Name:
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
                        
                        case PaymentMethodOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case PaymentMethodOrder.Name:
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

        private async Task<List<PaymentMethod>> DynamicSelect(IQueryable<PaymentMethodDAO> query, PaymentMethodFilter filter)
        {
            List <PaymentMethod> PaymentMethods = await query.Select(q => new PaymentMethod()
            {
                
                Id = filter.Selects.Contains(PaymentMethodSelect.Id) ? q.Id : default(Guid),
                Code = filter.Selects.Contains(PaymentMethodSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(PaymentMethodSelect.Name) ? q.Name : default(string),
                SetOfBookId = filter.Selects.Contains(PaymentMethodSelect.SetOfBook) ? q.SetOfBookId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(PaymentMethodSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return PaymentMethods;
        }

        public async Task<int> Count(PaymentMethodFilter filter)
        {
            IQueryable <PaymentMethodDAO> PaymentMethodDAOs = ERPContext.PaymentMethod;
            PaymentMethodDAOs = DynamicFilter(PaymentMethodDAOs, filter);
            return await PaymentMethodDAOs.CountAsync();
        }

        public async Task<List<PaymentMethod>> List(PaymentMethodFilter filter)
        {
            if (filter == null) return new List<PaymentMethod>();
            IQueryable<PaymentMethodDAO> PaymentMethodDAOs = ERPContext.PaymentMethod;
            PaymentMethodDAOs = DynamicFilter(PaymentMethodDAOs, filter);
            PaymentMethodDAOs = DynamicOrder(PaymentMethodDAOs, filter);
            var PaymentMethods = await DynamicSelect(PaymentMethodDAOs, filter);
            return PaymentMethods;
        }

        public async Task<PaymentMethod> Get(Guid Id)
        {
            PaymentMethod PaymentMethod = await ERPContext.PaymentMethod.Where(l => l.Id == Id).Select(PaymentMethodDAO => new PaymentMethod()
            {
                 
                Id = PaymentMethodDAO.Id,
                Code = PaymentMethodDAO.Code,
                Name = PaymentMethodDAO.Name,
                SetOfBookId = PaymentMethodDAO.SetOfBookId,
                BusinessGroupId = PaymentMethodDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return PaymentMethod;
        }

        public async Task<bool> Create(PaymentMethod PaymentMethod)
        {
            PaymentMethodDAO PaymentMethodDAO = new PaymentMethodDAO();
            
            PaymentMethodDAO.Id = PaymentMethod.Id;
            PaymentMethodDAO.Code = PaymentMethod.Code;
            PaymentMethodDAO.Name = PaymentMethod.Name;
            PaymentMethodDAO.SetOfBookId = PaymentMethod.SetOfBookId;
            PaymentMethodDAO.BusinessGroupId = PaymentMethod.BusinessGroupId;
            PaymentMethodDAO.Disabled = false;
            
            await ERPContext.PaymentMethod.AddAsync(PaymentMethodDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(PaymentMethod PaymentMethod)
        {
            PaymentMethodDAO PaymentMethodDAO = ERPContext.PaymentMethod.Where(b => b.Id == PaymentMethod.Id).FirstOrDefault();
            
            PaymentMethodDAO.Id = PaymentMethod.Id;
            PaymentMethodDAO.Code = PaymentMethod.Code;
            PaymentMethodDAO.Name = PaymentMethod.Name;
            PaymentMethodDAO.SetOfBookId = PaymentMethod.SetOfBookId;
            PaymentMethodDAO.BusinessGroupId = PaymentMethod.BusinessGroupId;
            PaymentMethodDAO.Disabled = false;
            ERPContext.PaymentMethod.Update(PaymentMethodDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            PaymentMethodDAO PaymentMethodDAO = await ERPContext.PaymentMethod.Where(x => x.Id == Id).FirstOrDefaultAsync();
            PaymentMethodDAO.Disabled = true;
            ERPContext.PaymentMethod.Update(PaymentMethodDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
