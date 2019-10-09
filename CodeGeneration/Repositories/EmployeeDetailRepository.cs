
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
    public interface IEmployeeDetailRepository
    {
        Task<int> Count(EmployeeDetailFilter EmployeeDetailFilter);
        Task<List<EmployeeDetail>> List(EmployeeDetailFilter EmployeeDetailFilter);
        Task<EmployeeDetail> Get(Guid Id);
        Task<bool> Create(EmployeeDetail EmployeeDetail);
        Task<bool> Update(EmployeeDetail EmployeeDetail);
        Task<bool> Delete(Guid Id);
        
    }
    public class EmployeeDetailRepository : IEmployeeDetailRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public EmployeeDetailRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<EmployeeDetailDAO> DynamicFilter(IQueryable<EmployeeDetailDAO> query, EmployeeDetailFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.EmployeeId != null)
                query = query.Where(q => q.EmployeeId, filter.EmployeeId);
            if (filter.LegalEntityId != null)
                query = query.Where(q => q.LegalEntityId, filter.LegalEntityId);
            if (filter.BankAccountName != null)
                query = query.Where(q => q.BankAccountName, filter.BankAccountName);
            if (filter.BankAccountNumber != null)
                query = query.Where(q => q.BankAccountNumber, filter.BankAccountNumber);
            if (filter.BankId.HasValue)
                query = query.Where(q => q.BankId.HasValue && q.BankId.Value == filter.BankId.Value);
            if (filter.BankId != null)
                query = query.Where(q => q.BankId, filter.BankId);
            if (filter.ProvinceId.HasValue)
                query = query.Where(q => q.ProvinceId.HasValue && q.ProvinceId.Value == filter.ProvinceId.Value);
            if (filter.ProvinceId != null)
                query = query.Where(q => q.ProvinceId, filter.ProvinceId);
            if (filter.BankBranch != null)
                query = query.Where(q => q.BankBranch, filter.BankBranch);
            if (filter.BankAddress != null)
                query = query.Where(q => q.BankAddress, filter.BankAddress);
            if (filter.EffectiveDate.HasValue)
                query = query.Where(q => q.EffectiveDate.HasValue && q.EffectiveDate.Value == filter.EffectiveDate.Value);
            if (filter.EffectiveDate != null)
                query = query.Where(q => q.EffectiveDate, filter.EffectiveDate);
            if (filter.EndDate.HasValue)
                query = query.Where(q => q.EndDate.HasValue && q.EndDate.Value == filter.EndDate.Value);
            if (filter.EndDate != null)
                query = query.Where(q => q.EndDate, filter.EndDate);
            if (filter.JoinDate.HasValue)
                query = query.Where(q => q.JoinDate.HasValue && q.JoinDate.Value == filter.JoinDate.Value);
            if (filter.JoinDate != null)
                query = query.Where(q => q.JoinDate, filter.JoinDate);
            return query;
        }
        private IQueryable<EmployeeDetailDAO> DynamicOrder(IQueryable<EmployeeDetailDAO> query,  EmployeeDetailFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case EmployeeDetailOrder.BankAccountName:
                            query = query.OrderBy(q => q.BankAccountName);
                            break;
                        case EmployeeDetailOrder.BankAccountNumber:
                            query = query.OrderBy(q => q.BankAccountNumber);
                            break;
                        case EmployeeDetailOrder.BankId:
                            query = query.OrderBy(q => q.BankId);
                            break;
                        case EmployeeDetailOrder.ProvinceId:
                            query = query.OrderBy(q => q.ProvinceId);
                            break;
                        case EmployeeDetailOrder.BankBranch:
                            query = query.OrderBy(q => q.BankBranch);
                            break;
                        case EmployeeDetailOrder.BankAddress:
                            query = query.OrderBy(q => q.BankAddress);
                            break;
                        case EmployeeDetailOrder.EffectiveDate:
                            query = query.OrderBy(q => q.EffectiveDate);
                            break;
                        case EmployeeDetailOrder.EndDate:
                            query = query.OrderBy(q => q.EndDate);
                            break;
                        case EmployeeDetailOrder.JoinDate:
                            query = query.OrderBy(q => q.JoinDate);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case EmployeeDetailOrder.BankAccountName:
                            query = query.OrderByDescending(q => q.BankAccountName);
                            break;
                        case EmployeeDetailOrder.BankAccountNumber:
                            query = query.OrderByDescending(q => q.BankAccountNumber);
                            break;
                        case EmployeeDetailOrder.BankId:
                            query = query.OrderByDescending(q => q.BankId);
                            break;
                        case EmployeeDetailOrder.ProvinceId:
                            query = query.OrderByDescending(q => q.ProvinceId);
                            break;
                        case EmployeeDetailOrder.BankBranch:
                            query = query.OrderByDescending(q => q.BankBranch);
                            break;
                        case EmployeeDetailOrder.BankAddress:
                            query = query.OrderByDescending(q => q.BankAddress);
                            break;
                        case EmployeeDetailOrder.EffectiveDate:
                            query = query.OrderByDescending(q => q.EffectiveDate);
                            break;
                        case EmployeeDetailOrder.EndDate:
                            query = query.OrderByDescending(q => q.EndDate);
                            break;
                        case EmployeeDetailOrder.JoinDate:
                            query = query.OrderByDescending(q => q.JoinDate);
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

        private async Task<List<EmployeeDetail>> DynamicSelect(IQueryable<EmployeeDetailDAO> query, EmployeeDetailFilter filter)
        {
            List <EmployeeDetail> EmployeeDetails = await query.Select(q => new EmployeeDetail()
            {
                
                Id = filter.Selects.Contains(EmployeeDetailSelect.Id) ? q.Id : default(Guid),
                EmployeeId = filter.Selects.Contains(EmployeeDetailSelect.Employee) ? q.EmployeeId : default(Guid),
                LegalEntityId = filter.Selects.Contains(EmployeeDetailSelect.LegalEntity) ? q.LegalEntityId : default(Guid),
                BankAccountName = filter.Selects.Contains(EmployeeDetailSelect.BankAccountName) ? q.BankAccountName : default(string),
                BankAccountNumber = filter.Selects.Contains(EmployeeDetailSelect.BankAccountNumber) ? q.BankAccountNumber : default(string),
                BankId = filter.Selects.Contains(EmployeeDetailSelect.Bank) ? q.BankId : default(Guid?),
                ProvinceId = filter.Selects.Contains(EmployeeDetailSelect.Province) ? q.ProvinceId : default(Guid?),
                BankBranch = filter.Selects.Contains(EmployeeDetailSelect.BankBranch) ? q.BankBranch : default(string),
                BankAddress = filter.Selects.Contains(EmployeeDetailSelect.BankAddress) ? q.BankAddress : default(string),
                EffectiveDate = filter.Selects.Contains(EmployeeDetailSelect.EffectiveDate) ? q.EffectiveDate : default(Guid?),
                EndDate = filter.Selects.Contains(EmployeeDetailSelect.EndDate) ? q.EndDate : default(Guid?),
                JoinDate = filter.Selects.Contains(EmployeeDetailSelect.JoinDate) ? q.JoinDate : default(Guid?),
            }).ToListAsync();
            return EmployeeDetails;
        }

        public async Task<int> Count(EmployeeDetailFilter filter)
        {
            IQueryable <EmployeeDetailDAO> EmployeeDetailDAOs = ERPContext.EmployeeDetail;
            EmployeeDetailDAOs = DynamicFilter(EmployeeDetailDAOs, filter);
            return await EmployeeDetailDAOs.CountAsync();
        }

        public async Task<List<EmployeeDetail>> List(EmployeeDetailFilter filter)
        {
            if (filter == null) return new List<EmployeeDetail>();
            IQueryable<EmployeeDetailDAO> EmployeeDetailDAOs = ERPContext.EmployeeDetail;
            EmployeeDetailDAOs = DynamicFilter(EmployeeDetailDAOs, filter);
            EmployeeDetailDAOs = DynamicOrder(EmployeeDetailDAOs, filter);
            var EmployeeDetails = await DynamicSelect(EmployeeDetailDAOs, filter);
            return EmployeeDetails;
        }

        public async Task<EmployeeDetail> Get(Guid Id)
        {
            EmployeeDetail EmployeeDetail = await ERPContext.EmployeeDetail.Where(l => l.Id == Id).Select(EmployeeDetailDAO => new EmployeeDetail()
            {
                 
                Id = EmployeeDetailDAO.Id,
                EmployeeId = EmployeeDetailDAO.EmployeeId,
                LegalEntityId = EmployeeDetailDAO.LegalEntityId,
                BankAccountName = EmployeeDetailDAO.BankAccountName,
                BankAccountNumber = EmployeeDetailDAO.BankAccountNumber,
                BankId = EmployeeDetailDAO.BankId,
                ProvinceId = EmployeeDetailDAO.ProvinceId,
                BankBranch = EmployeeDetailDAO.BankBranch,
                BankAddress = EmployeeDetailDAO.BankAddress,
                EffectiveDate = EmployeeDetailDAO.EffectiveDate,
                EndDate = EmployeeDetailDAO.EndDate,
                JoinDate = EmployeeDetailDAO.JoinDate,
            }).FirstOrDefaultAsync();
            return EmployeeDetail;
        }

        public async Task<bool> Create(EmployeeDetail EmployeeDetail)
        {
            EmployeeDetailDAO EmployeeDetailDAO = new EmployeeDetailDAO();
            
            EmployeeDetailDAO.Id = EmployeeDetail.Id;
            EmployeeDetailDAO.EmployeeId = EmployeeDetail.EmployeeId;
            EmployeeDetailDAO.LegalEntityId = EmployeeDetail.LegalEntityId;
            EmployeeDetailDAO.BankAccountName = EmployeeDetail.BankAccountName;
            EmployeeDetailDAO.BankAccountNumber = EmployeeDetail.BankAccountNumber;
            EmployeeDetailDAO.BankId = EmployeeDetail.BankId;
            EmployeeDetailDAO.ProvinceId = EmployeeDetail.ProvinceId;
            EmployeeDetailDAO.BankBranch = EmployeeDetail.BankBranch;
            EmployeeDetailDAO.BankAddress = EmployeeDetail.BankAddress;
            EmployeeDetailDAO.EffectiveDate = EmployeeDetail.EffectiveDate;
            EmployeeDetailDAO.EndDate = EmployeeDetail.EndDate;
            EmployeeDetailDAO.JoinDate = EmployeeDetail.JoinDate;
            EmployeeDetailDAO.Disabled = false;
            
            await ERPContext.EmployeeDetail.AddAsync(EmployeeDetailDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(EmployeeDetail EmployeeDetail)
        {
            EmployeeDetailDAO EmployeeDetailDAO = ERPContext.EmployeeDetail.Where(b => b.Id == EmployeeDetail.Id).FirstOrDefault();
            
            EmployeeDetailDAO.Id = EmployeeDetail.Id;
            EmployeeDetailDAO.EmployeeId = EmployeeDetail.EmployeeId;
            EmployeeDetailDAO.LegalEntityId = EmployeeDetail.LegalEntityId;
            EmployeeDetailDAO.BankAccountName = EmployeeDetail.BankAccountName;
            EmployeeDetailDAO.BankAccountNumber = EmployeeDetail.BankAccountNumber;
            EmployeeDetailDAO.BankId = EmployeeDetail.BankId;
            EmployeeDetailDAO.ProvinceId = EmployeeDetail.ProvinceId;
            EmployeeDetailDAO.BankBranch = EmployeeDetail.BankBranch;
            EmployeeDetailDAO.BankAddress = EmployeeDetail.BankAddress;
            EmployeeDetailDAO.EffectiveDate = EmployeeDetail.EffectiveDate;
            EmployeeDetailDAO.EndDate = EmployeeDetail.EndDate;
            EmployeeDetailDAO.JoinDate = EmployeeDetail.JoinDate;
            EmployeeDetailDAO.Disabled = false;
            ERPContext.EmployeeDetail.Update(EmployeeDetailDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            EmployeeDetailDAO EmployeeDetailDAO = await ERPContext.EmployeeDetail.Where(x => x.Id == Id).FirstOrDefaultAsync();
            EmployeeDetailDAO.Disabled = true;
            ERPContext.EmployeeDetail.Update(EmployeeDetailDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
