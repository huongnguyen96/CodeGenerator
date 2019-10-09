
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
    public interface IVoucherRepository
    {
        Task<int> Count(VoucherFilter VoucherFilter);
        Task<List<Voucher>> List(VoucherFilter VoucherFilter);
        Task<Voucher> Get(Guid Id);
        Task<bool> Create(Voucher Voucher);
        Task<bool> Update(Voucher Voucher);
        Task<bool> Delete(Guid Id);
        
    }
    public class VoucherRepository : IVoucherRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public VoucherRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<VoucherDAO> DynamicFilter(IQueryable<VoucherDAO> query, VoucherFilter filter)
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
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.DebitAccountId.HasValue)
                query = query.Where(q => q.DebitAccountId.HasValue && q.DebitAccountId.Value == filter.DebitAccountId.Value);
            if (filter.DebitAccountId != null)
                query = query.Where(q => q.DebitAccountId, filter.DebitAccountId);
            if (filter.CreditAccountId.HasValue)
                query = query.Where(q => q.CreditAccountId.HasValue && q.CreditAccountId.Value == filter.CreditAccountId.Value);
            if (filter.CreditAccountId != null)
                query = query.Where(q => q.CreditAccountId, filter.CreditAccountId);
            if (filter.VoucherTypeId.HasValue)
                query = query.Where(q => q.VoucherTypeId.HasValue && q.VoucherTypeId.Value == filter.VoucherTypeId.Value);
            if (filter.VoucherTypeId != null)
                query = query.Where(q => q.VoucherTypeId, filter.VoucherTypeId);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<VoucherDAO> DynamicOrder(IQueryable<VoucherDAO> query,  VoucherFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case VoucherOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case VoucherOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case VoucherOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case VoucherOrder.DebitAccountId:
                            query = query.OrderBy(q => q.DebitAccountId);
                            break;
                        case VoucherOrder.CreditAccountId:
                            query = query.OrderBy(q => q.CreditAccountId);
                            break;
                        case VoucherOrder.VoucherTypeId:
                            query = query.OrderBy(q => q.VoucherTypeId);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case VoucherOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case VoucherOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case VoucherOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case VoucherOrder.DebitAccountId:
                            query = query.OrderByDescending(q => q.DebitAccountId);
                            break;
                        case VoucherOrder.CreditAccountId:
                            query = query.OrderByDescending(q => q.CreditAccountId);
                            break;
                        case VoucherOrder.VoucherTypeId:
                            query = query.OrderByDescending(q => q.VoucherTypeId);
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

        private async Task<List<Voucher>> DynamicSelect(IQueryable<VoucherDAO> query, VoucherFilter filter)
        {
            List <Voucher> Vouchers = await query.Select(q => new Voucher()
            {
                
                Id = filter.Selects.Contains(VoucherSelect.Id) ? q.Id : default(Guid),
                SetOfBookId = filter.Selects.Contains(VoucherSelect.SetOfBook) ? q.SetOfBookId : default(Guid),
                Code = filter.Selects.Contains(VoucherSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(VoucherSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(VoucherSelect.Description) ? q.Description : default(string),
                DebitAccountId = filter.Selects.Contains(VoucherSelect.DebitAccount) ? q.DebitAccountId : default(Guid?),
                CreditAccountId = filter.Selects.Contains(VoucherSelect.CreditAccount) ? q.CreditAccountId : default(Guid?),
                VoucherTypeId = filter.Selects.Contains(VoucherSelect.VoucherType) ? q.VoucherTypeId : default(Guid?),
                BusinessGroupId = filter.Selects.Contains(VoucherSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return Vouchers;
        }

        public async Task<int> Count(VoucherFilter filter)
        {
            IQueryable <VoucherDAO> VoucherDAOs = ERPContext.Voucher;
            VoucherDAOs = DynamicFilter(VoucherDAOs, filter);
            return await VoucherDAOs.CountAsync();
        }

        public async Task<List<Voucher>> List(VoucherFilter filter)
        {
            if (filter == null) return new List<Voucher>();
            IQueryable<VoucherDAO> VoucherDAOs = ERPContext.Voucher;
            VoucherDAOs = DynamicFilter(VoucherDAOs, filter);
            VoucherDAOs = DynamicOrder(VoucherDAOs, filter);
            var Vouchers = await DynamicSelect(VoucherDAOs, filter);
            return Vouchers;
        }

        public async Task<Voucher> Get(Guid Id)
        {
            Voucher Voucher = await ERPContext.Voucher.Where(l => l.Id == Id).Select(VoucherDAO => new Voucher()
            {
                 
                Id = VoucherDAO.Id,
                SetOfBookId = VoucherDAO.SetOfBookId,
                Code = VoucherDAO.Code,
                Name = VoucherDAO.Name,
                Description = VoucherDAO.Description,
                DebitAccountId = VoucherDAO.DebitAccountId,
                CreditAccountId = VoucherDAO.CreditAccountId,
                VoucherTypeId = VoucherDAO.VoucherTypeId,
                BusinessGroupId = VoucherDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return Voucher;
        }

        public async Task<bool> Create(Voucher Voucher)
        {
            VoucherDAO VoucherDAO = new VoucherDAO();
            
            VoucherDAO.Id = Voucher.Id;
            VoucherDAO.SetOfBookId = Voucher.SetOfBookId;
            VoucherDAO.Code = Voucher.Code;
            VoucherDAO.Name = Voucher.Name;
            VoucherDAO.Description = Voucher.Description;
            VoucherDAO.DebitAccountId = Voucher.DebitAccountId;
            VoucherDAO.CreditAccountId = Voucher.CreditAccountId;
            VoucherDAO.VoucherTypeId = Voucher.VoucherTypeId;
            VoucherDAO.BusinessGroupId = Voucher.BusinessGroupId;
            VoucherDAO.Disabled = false;
            
            await ERPContext.Voucher.AddAsync(VoucherDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Voucher Voucher)
        {
            VoucherDAO VoucherDAO = ERPContext.Voucher.Where(b => b.Id == Voucher.Id).FirstOrDefault();
            
            VoucherDAO.Id = Voucher.Id;
            VoucherDAO.SetOfBookId = Voucher.SetOfBookId;
            VoucherDAO.Code = Voucher.Code;
            VoucherDAO.Name = Voucher.Name;
            VoucherDAO.Description = Voucher.Description;
            VoucherDAO.DebitAccountId = Voucher.DebitAccountId;
            VoucherDAO.CreditAccountId = Voucher.CreditAccountId;
            VoucherDAO.VoucherTypeId = Voucher.VoucherTypeId;
            VoucherDAO.BusinessGroupId = Voucher.BusinessGroupId;
            VoucherDAO.Disabled = false;
            ERPContext.Voucher.Update(VoucherDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            VoucherDAO VoucherDAO = await ERPContext.Voucher.Where(x => x.Id == Id).FirstOrDefaultAsync();
            VoucherDAO.Disabled = true;
            ERPContext.Voucher.Update(VoucherDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
