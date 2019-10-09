
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
    public interface ISupplierDetailRepository
    {
        Task<int> Count(SupplierDetailFilter SupplierDetailFilter);
        Task<List<SupplierDetail>> List(SupplierDetailFilter SupplierDetailFilter);
        Task<SupplierDetail> Get(Guid Id);
        Task<bool> Create(SupplierDetail SupplierDetail);
        Task<bool> Update(SupplierDetail SupplierDetail);
        Task<bool> Delete(Guid Id);
        
    }
    public class SupplierDetailRepository : ISupplierDetailRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public SupplierDetailRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<SupplierDetailDAO> DynamicFilter(IQueryable<SupplierDetailDAO> query, SupplierDetailFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.LegalEntityId != null)
                query = query.Where(q => q.LegalEntityId, filter.LegalEntityId);
            if (filter.SupplierId != null)
                query = query.Where(q => q.SupplierId, filter.SupplierId);
            if (filter.StaffInChargeId.HasValue)
                query = query.Where(q => q.StaffInChargeId.HasValue && q.StaffInChargeId.Value == filter.StaffInChargeId.Value);
            if (filter.StaffInChargeId != null)
                query = query.Where(q => q.StaffInChargeId, filter.StaffInChargeId);
            if (filter.PaymentTermId.HasValue)
                query = query.Where(q => q.PaymentTermId.HasValue && q.PaymentTermId.Value == filter.PaymentTermId.Value);
            if (filter.PaymentTermId != null)
                query = query.Where(q => q.PaymentTermId, filter.PaymentTermId);
            if (filter.DebtLoad.HasValue)
                query = query.Where(q => q.DebtLoad.HasValue && q.DebtLoad.Value == filter.DebtLoad.Value);
            if (filter.DebtLoad != null)
                query = query.Where(q => q.DebtLoad, filter.DebtLoad);
            if (filter.DueInDays.HasValue)
                query = query.Where(q => q.DueInDays.HasValue && q.DueInDays.Value == filter.DueInDays.Value);
            if (filter.DueInDays != null)
                query = query.Where(q => q.DueInDays, filter.DueInDays);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<SupplierDetailDAO> DynamicOrder(IQueryable<SupplierDetailDAO> query,  SupplierDetailFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case SupplierDetailOrder.StaffInChargeId:
                            query = query.OrderBy(q => q.StaffInChargeId);
                            break;
                        case SupplierDetailOrder.PaymentTermId:
                            query = query.OrderBy(q => q.PaymentTermId);
                            break;
                        case SupplierDetailOrder.DebtLoad:
                            query = query.OrderBy(q => q.DebtLoad);
                            break;
                        case SupplierDetailOrder.DueInDays:
                            query = query.OrderBy(q => q.DueInDays);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case SupplierDetailOrder.StaffInChargeId:
                            query = query.OrderByDescending(q => q.StaffInChargeId);
                            break;
                        case SupplierDetailOrder.PaymentTermId:
                            query = query.OrderByDescending(q => q.PaymentTermId);
                            break;
                        case SupplierDetailOrder.DebtLoad:
                            query = query.OrderByDescending(q => q.DebtLoad);
                            break;
                        case SupplierDetailOrder.DueInDays:
                            query = query.OrderByDescending(q => q.DueInDays);
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

        private async Task<List<SupplierDetail>> DynamicSelect(IQueryable<SupplierDetailDAO> query, SupplierDetailFilter filter)
        {
            List <SupplierDetail> SupplierDetails = await query.Select(q => new SupplierDetail()
            {
                
                Id = filter.Selects.Contains(SupplierDetailSelect.Id) ? q.Id : default(Guid),
                LegalEntityId = filter.Selects.Contains(SupplierDetailSelect.LegalEntity) ? q.LegalEntityId : default(Guid),
                SupplierId = filter.Selects.Contains(SupplierDetailSelect.Supplier) ? q.SupplierId : default(Guid),
                StaffInChargeId = filter.Selects.Contains(SupplierDetailSelect.StaffInCharge) ? q.StaffInChargeId : default(Guid?),
                PaymentTermId = filter.Selects.Contains(SupplierDetailSelect.PaymentTerm) ? q.PaymentTermId : default(Guid?),
                DebtLoad = filter.Selects.Contains(SupplierDetailSelect.DebtLoad) ? q.DebtLoad : default(Guid?),
                DueInDays = filter.Selects.Contains(SupplierDetailSelect.DueInDays) ? q.DueInDays : default(Guid?),
                BusinessGroupId = filter.Selects.Contains(SupplierDetailSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return SupplierDetails;
        }

        public async Task<int> Count(SupplierDetailFilter filter)
        {
            IQueryable <SupplierDetailDAO> SupplierDetailDAOs = ERPContext.SupplierDetail;
            SupplierDetailDAOs = DynamicFilter(SupplierDetailDAOs, filter);
            return await SupplierDetailDAOs.CountAsync();
        }

        public async Task<List<SupplierDetail>> List(SupplierDetailFilter filter)
        {
            if (filter == null) return new List<SupplierDetail>();
            IQueryable<SupplierDetailDAO> SupplierDetailDAOs = ERPContext.SupplierDetail;
            SupplierDetailDAOs = DynamicFilter(SupplierDetailDAOs, filter);
            SupplierDetailDAOs = DynamicOrder(SupplierDetailDAOs, filter);
            var SupplierDetails = await DynamicSelect(SupplierDetailDAOs, filter);
            return SupplierDetails;
        }

        public async Task<SupplierDetail> Get(Guid Id)
        {
            SupplierDetail SupplierDetail = await ERPContext.SupplierDetail.Where(l => l.Id == Id).Select(SupplierDetailDAO => new SupplierDetail()
            {
                 
                Id = SupplierDetailDAO.Id,
                LegalEntityId = SupplierDetailDAO.LegalEntityId,
                SupplierId = SupplierDetailDAO.SupplierId,
                StaffInChargeId = SupplierDetailDAO.StaffInChargeId,
                PaymentTermId = SupplierDetailDAO.PaymentTermId,
                DebtLoad = SupplierDetailDAO.DebtLoad,
                DueInDays = SupplierDetailDAO.DueInDays,
                BusinessGroupId = SupplierDetailDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return SupplierDetail;
        }

        public async Task<bool> Create(SupplierDetail SupplierDetail)
        {
            SupplierDetailDAO SupplierDetailDAO = new SupplierDetailDAO();
            
            SupplierDetailDAO.Id = SupplierDetail.Id;
            SupplierDetailDAO.LegalEntityId = SupplierDetail.LegalEntityId;
            SupplierDetailDAO.SupplierId = SupplierDetail.SupplierId;
            SupplierDetailDAO.StaffInChargeId = SupplierDetail.StaffInChargeId;
            SupplierDetailDAO.PaymentTermId = SupplierDetail.PaymentTermId;
            SupplierDetailDAO.DebtLoad = SupplierDetail.DebtLoad;
            SupplierDetailDAO.DueInDays = SupplierDetail.DueInDays;
            SupplierDetailDAO.BusinessGroupId = SupplierDetail.BusinessGroupId;
            SupplierDetailDAO.Disabled = false;
            
            await ERPContext.SupplierDetail.AddAsync(SupplierDetailDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(SupplierDetail SupplierDetail)
        {
            SupplierDetailDAO SupplierDetailDAO = ERPContext.SupplierDetail.Where(b => b.Id == SupplierDetail.Id).FirstOrDefault();
            
            SupplierDetailDAO.Id = SupplierDetail.Id;
            SupplierDetailDAO.LegalEntityId = SupplierDetail.LegalEntityId;
            SupplierDetailDAO.SupplierId = SupplierDetail.SupplierId;
            SupplierDetailDAO.StaffInChargeId = SupplierDetail.StaffInChargeId;
            SupplierDetailDAO.PaymentTermId = SupplierDetail.PaymentTermId;
            SupplierDetailDAO.DebtLoad = SupplierDetail.DebtLoad;
            SupplierDetailDAO.DueInDays = SupplierDetail.DueInDays;
            SupplierDetailDAO.BusinessGroupId = SupplierDetail.BusinessGroupId;
            SupplierDetailDAO.Disabled = false;
            ERPContext.SupplierDetail.Update(SupplierDetailDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            SupplierDetailDAO SupplierDetailDAO = await ERPContext.SupplierDetail.Where(x => x.Id == Id).FirstOrDefaultAsync();
            SupplierDetailDAO.Disabled = true;
            ERPContext.SupplierDetail.Update(SupplierDetailDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
