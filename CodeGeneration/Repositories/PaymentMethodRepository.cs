
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
    public interface IPaymentMethodRepository
    {
        Task<int> Count(PaymentMethodFilter PaymentMethodFilter);
        Task<List<PaymentMethod>> List(PaymentMethodFilter PaymentMethodFilter);
        Task<PaymentMethod> Get(long Id);
        Task<bool> Create(PaymentMethod PaymentMethod);
        Task<bool> Update(PaymentMethod PaymentMethod);
        Task<bool> Delete(PaymentMethod PaymentMethod);
        
    }
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public PaymentMethodRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<PaymentMethodDAO> DynamicFilter(IQueryable<PaymentMethodDAO> query, PaymentMethodFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<PaymentMethodDAO> DynamicOrder(IQueryable<PaymentMethodDAO> query,  PaymentMethodFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case PaymentMethodOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case PaymentMethodOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case PaymentMethodOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case PaymentMethodOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case PaymentMethodOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case PaymentMethodOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case PaymentMethodOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case PaymentMethodOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<PaymentMethod>> DynamicSelect(IQueryable<PaymentMethodDAO> query, PaymentMethodFilter filter)
        {
            List <PaymentMethod> PaymentMethods = await query.Select(q => new PaymentMethod()
            {
                
                Id = filter.Selects.Contains(PaymentMethodSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(PaymentMethodSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(PaymentMethodSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(PaymentMethodSelect.Description) ? q.Description : default(string),
            }).ToListAsync();
            return PaymentMethods;
        }

        public async Task<int> Count(PaymentMethodFilter filter)
        {
            IQueryable <PaymentMethodDAO> PaymentMethodDAOs = DataContext.PaymentMethod;
            PaymentMethodDAOs = DynamicFilter(PaymentMethodDAOs, filter);
            return await PaymentMethodDAOs.CountAsync();
        }

        public async Task<List<PaymentMethod>> List(PaymentMethodFilter filter)
        {
            if (filter == null) return new List<PaymentMethod>();
            IQueryable<PaymentMethodDAO> PaymentMethodDAOs = DataContext.PaymentMethod;
            PaymentMethodDAOs = DynamicFilter(PaymentMethodDAOs, filter);
            PaymentMethodDAOs = DynamicOrder(PaymentMethodDAOs, filter);
            var PaymentMethods = await DynamicSelect(PaymentMethodDAOs, filter);
            return PaymentMethods;
        }

        
        public async Task<PaymentMethod> Get(long Id)
        {
            PaymentMethod PaymentMethod = await DataContext.PaymentMethod.Where(x => x.Id == Id).Select(PaymentMethodDAO => new PaymentMethod()
            {
                 
                Id = PaymentMethodDAO.Id,
                Code = PaymentMethodDAO.Code,
                Name = PaymentMethodDAO.Name,
                Description = PaymentMethodDAO.Description,
            }).FirstOrDefaultAsync();
            return PaymentMethod;
        }

        public async Task<bool> Create(PaymentMethod PaymentMethod)
        {
            PaymentMethodDAO PaymentMethodDAO = new PaymentMethodDAO();
            
            PaymentMethodDAO.Id = PaymentMethod.Id;
            PaymentMethodDAO.Code = PaymentMethod.Code;
            PaymentMethodDAO.Name = PaymentMethod.Name;
            PaymentMethodDAO.Description = PaymentMethod.Description;
            
            await DataContext.PaymentMethod.AddAsync(PaymentMethodDAO);
            await DataContext.SaveChangesAsync();
            PaymentMethod.Id = PaymentMethodDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(PaymentMethod PaymentMethod)
        {
            PaymentMethodDAO PaymentMethodDAO = DataContext.PaymentMethod.Where(x => x.Id == PaymentMethod.Id).FirstOrDefault();
            
            PaymentMethodDAO.Id = PaymentMethod.Id;
            PaymentMethodDAO.Code = PaymentMethod.Code;
            PaymentMethodDAO.Name = PaymentMethod.Name;
            PaymentMethodDAO.Description = PaymentMethod.Description;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(PaymentMethod PaymentMethod)
        {
            PaymentMethodDAO PaymentMethodDAO = await DataContext.PaymentMethod.Where(x => x.Id == PaymentMethod.Id).FirstOrDefaultAsync();
            DataContext.PaymentMethod.Remove(PaymentMethodDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
