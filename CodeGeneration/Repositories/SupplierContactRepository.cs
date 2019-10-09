
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
    public interface ISupplierContactRepository
    {
        Task<int> Count(SupplierContactFilter SupplierContactFilter);
        Task<List<SupplierContact>> List(SupplierContactFilter SupplierContactFilter);
        Task<SupplierContact> Get(Guid Id);
        Task<bool> Create(SupplierContact SupplierContact);
        Task<bool> Update(SupplierContact SupplierContact);
        Task<bool> Delete(Guid Id);
        
    }
    public class SupplierContactRepository : ISupplierContactRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public SupplierContactRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<SupplierContactDAO> DynamicFilter(IQueryable<SupplierContactDAO> query, SupplierContactFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.SupplierDetailId != null)
                query = query.Where(q => q.SupplierDetailId, filter.SupplierDetailId);
            if (filter.FullName != null)
                query = query.Where(q => q.FullName, filter.FullName);
            if (filter.Email != null)
                query = query.Where(q => q.Email, filter.Email);
            if (filter.Phone != null)
                query = query.Where(q => q.Phone, filter.Phone);
            if (filter.Address != null)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.ProvinceId.HasValue)
                query = query.Where(q => q.ProvinceId.HasValue && q.ProvinceId.Value == filter.ProvinceId.Value);
            if (filter.ProvinceId != null)
                query = query.Where(q => q.ProvinceId, filter.ProvinceId);
            return query;
        }
        private IQueryable<SupplierContactDAO> DynamicOrder(IQueryable<SupplierContactDAO> query,  SupplierContactFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case SupplierContactOrder.FullName:
                            query = query.OrderBy(q => q.FullName);
                            break;
                        case SupplierContactOrder.Email:
                            query = query.OrderBy(q => q.Email);
                            break;
                        case SupplierContactOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                        case SupplierContactOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        case SupplierContactOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case SupplierContactOrder.ProvinceId:
                            query = query.OrderBy(q => q.ProvinceId);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case SupplierContactOrder.FullName:
                            query = query.OrderByDescending(q => q.FullName);
                            break;
                        case SupplierContactOrder.Email:
                            query = query.OrderByDescending(q => q.Email);
                            break;
                        case SupplierContactOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                        case SupplierContactOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        case SupplierContactOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case SupplierContactOrder.ProvinceId:
                            query = query.OrderByDescending(q => q.ProvinceId);
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

        private async Task<List<SupplierContact>> DynamicSelect(IQueryable<SupplierContactDAO> query, SupplierContactFilter filter)
        {
            List <SupplierContact> SupplierContacts = await query.Select(q => new SupplierContact()
            {
                
                Id = filter.Selects.Contains(SupplierContactSelect.Id) ? q.Id : default(Guid),
                SupplierDetailId = filter.Selects.Contains(SupplierContactSelect.SupplierDetail) ? q.SupplierDetailId : default(Guid),
                FullName = filter.Selects.Contains(SupplierContactSelect.FullName) ? q.FullName : default(string),
                Email = filter.Selects.Contains(SupplierContactSelect.Email) ? q.Email : default(string),
                Phone = filter.Selects.Contains(SupplierContactSelect.Phone) ? q.Phone : default(string),
                Address = filter.Selects.Contains(SupplierContactSelect.Address) ? q.Address : default(string),
                Description = filter.Selects.Contains(SupplierContactSelect.Description) ? q.Description : default(string),
                BusinessGroupId = filter.Selects.Contains(SupplierContactSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                ProvinceId = filter.Selects.Contains(SupplierContactSelect.Province) ? q.ProvinceId : default(Guid?),
            }).ToListAsync();
            return SupplierContacts;
        }

        public async Task<int> Count(SupplierContactFilter filter)
        {
            IQueryable <SupplierContactDAO> SupplierContactDAOs = ERPContext.SupplierContact;
            SupplierContactDAOs = DynamicFilter(SupplierContactDAOs, filter);
            return await SupplierContactDAOs.CountAsync();
        }

        public async Task<List<SupplierContact>> List(SupplierContactFilter filter)
        {
            if (filter == null) return new List<SupplierContact>();
            IQueryable<SupplierContactDAO> SupplierContactDAOs = ERPContext.SupplierContact;
            SupplierContactDAOs = DynamicFilter(SupplierContactDAOs, filter);
            SupplierContactDAOs = DynamicOrder(SupplierContactDAOs, filter);
            var SupplierContacts = await DynamicSelect(SupplierContactDAOs, filter);
            return SupplierContacts;
        }

        public async Task<SupplierContact> Get(Guid Id)
        {
            SupplierContact SupplierContact = await ERPContext.SupplierContact.Where(l => l.Id == Id).Select(SupplierContactDAO => new SupplierContact()
            {
                 
                Id = SupplierContactDAO.Id,
                SupplierDetailId = SupplierContactDAO.SupplierDetailId,
                FullName = SupplierContactDAO.FullName,
                Email = SupplierContactDAO.Email,
                Phone = SupplierContactDAO.Phone,
                Address = SupplierContactDAO.Address,
                Description = SupplierContactDAO.Description,
                BusinessGroupId = SupplierContactDAO.BusinessGroupId,
                ProvinceId = SupplierContactDAO.ProvinceId,
            }).FirstOrDefaultAsync();
            return SupplierContact;
        }

        public async Task<bool> Create(SupplierContact SupplierContact)
        {
            SupplierContactDAO SupplierContactDAO = new SupplierContactDAO();
            
            SupplierContactDAO.Id = SupplierContact.Id;
            SupplierContactDAO.SupplierDetailId = SupplierContact.SupplierDetailId;
            SupplierContactDAO.FullName = SupplierContact.FullName;
            SupplierContactDAO.Email = SupplierContact.Email;
            SupplierContactDAO.Phone = SupplierContact.Phone;
            SupplierContactDAO.Address = SupplierContact.Address;
            SupplierContactDAO.Description = SupplierContact.Description;
            SupplierContactDAO.BusinessGroupId = SupplierContact.BusinessGroupId;
            SupplierContactDAO.ProvinceId = SupplierContact.ProvinceId;
            SupplierContactDAO.Disabled = false;
            
            await ERPContext.SupplierContact.AddAsync(SupplierContactDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(SupplierContact SupplierContact)
        {
            SupplierContactDAO SupplierContactDAO = ERPContext.SupplierContact.Where(b => b.Id == SupplierContact.Id).FirstOrDefault();
            
            SupplierContactDAO.Id = SupplierContact.Id;
            SupplierContactDAO.SupplierDetailId = SupplierContact.SupplierDetailId;
            SupplierContactDAO.FullName = SupplierContact.FullName;
            SupplierContactDAO.Email = SupplierContact.Email;
            SupplierContactDAO.Phone = SupplierContact.Phone;
            SupplierContactDAO.Address = SupplierContact.Address;
            SupplierContactDAO.Description = SupplierContact.Description;
            SupplierContactDAO.BusinessGroupId = SupplierContact.BusinessGroupId;
            SupplierContactDAO.ProvinceId = SupplierContact.ProvinceId;
            SupplierContactDAO.Disabled = false;
            ERPContext.SupplierContact.Update(SupplierContactDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            SupplierContactDAO SupplierContactDAO = await ERPContext.SupplierContact.Where(x => x.Id == Id).FirstOrDefaultAsync();
            SupplierContactDAO.Disabled = true;
            ERPContext.SupplierContact.Update(SupplierContactDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
