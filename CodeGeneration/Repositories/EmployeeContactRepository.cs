
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
    public interface IEmployeeContactRepository
    {
        Task<int> Count(EmployeeContactFilter EmployeeContactFilter);
        Task<List<EmployeeContact>> List(EmployeeContactFilter EmployeeContactFilter);
        Task<EmployeeContact> Get(Guid Id);
        Task<bool> Create(EmployeeContact EmployeeContact);
        Task<bool> Update(EmployeeContact EmployeeContact);
        Task<bool> Delete(Guid Id);
        
    }
    public class EmployeeContactRepository : IEmployeeContactRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public EmployeeContactRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<EmployeeContactDAO> DynamicFilter(IQueryable<EmployeeContactDAO> query, EmployeeContactFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.EmployeeDetailId != null)
                query = query.Where(q => q.EmployeeDetailId, filter.EmployeeDetailId);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
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
            return query;
        }
        private IQueryable<EmployeeContactDAO> DynamicOrder(IQueryable<EmployeeContactDAO> query,  EmployeeContactFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case EmployeeContactOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case EmployeeContactOrder.Email:
                            query = query.OrderBy(q => q.Email);
                            break;
                        case EmployeeContactOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                        case EmployeeContactOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        case EmployeeContactOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case EmployeeContactOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case EmployeeContactOrder.Email:
                            query = query.OrderByDescending(q => q.Email);
                            break;
                        case EmployeeContactOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                        case EmployeeContactOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        case EmployeeContactOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
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

        private async Task<List<EmployeeContact>> DynamicSelect(IQueryable<EmployeeContactDAO> query, EmployeeContactFilter filter)
        {
            List <EmployeeContact> EmployeeContacts = await query.Select(q => new EmployeeContact()
            {
                
                Id = filter.Selects.Contains(EmployeeContactSelect.Id) ? q.Id : default(Guid),
                EmployeeDetailId = filter.Selects.Contains(EmployeeContactSelect.EmployeeDetail) ? q.EmployeeDetailId : default(Guid),
                Name = filter.Selects.Contains(EmployeeContactSelect.Name) ? q.Name : default(string),
                Email = filter.Selects.Contains(EmployeeContactSelect.Email) ? q.Email : default(string),
                Phone = filter.Selects.Contains(EmployeeContactSelect.Phone) ? q.Phone : default(string),
                Address = filter.Selects.Contains(EmployeeContactSelect.Address) ? q.Address : default(string),
                Description = filter.Selects.Contains(EmployeeContactSelect.Description) ? q.Description : default(string),
                BusinessGroupId = filter.Selects.Contains(EmployeeContactSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return EmployeeContacts;
        }

        public async Task<int> Count(EmployeeContactFilter filter)
        {
            IQueryable <EmployeeContactDAO> EmployeeContactDAOs = ERPContext.EmployeeContact;
            EmployeeContactDAOs = DynamicFilter(EmployeeContactDAOs, filter);
            return await EmployeeContactDAOs.CountAsync();
        }

        public async Task<List<EmployeeContact>> List(EmployeeContactFilter filter)
        {
            if (filter == null) return new List<EmployeeContact>();
            IQueryable<EmployeeContactDAO> EmployeeContactDAOs = ERPContext.EmployeeContact;
            EmployeeContactDAOs = DynamicFilter(EmployeeContactDAOs, filter);
            EmployeeContactDAOs = DynamicOrder(EmployeeContactDAOs, filter);
            var EmployeeContacts = await DynamicSelect(EmployeeContactDAOs, filter);
            return EmployeeContacts;
        }

        public async Task<EmployeeContact> Get(Guid Id)
        {
            EmployeeContact EmployeeContact = await ERPContext.EmployeeContact.Where(l => l.Id == Id).Select(EmployeeContactDAO => new EmployeeContact()
            {
                 
                Id = EmployeeContactDAO.Id,
                EmployeeDetailId = EmployeeContactDAO.EmployeeDetailId,
                Name = EmployeeContactDAO.Name,
                Email = EmployeeContactDAO.Email,
                Phone = EmployeeContactDAO.Phone,
                Address = EmployeeContactDAO.Address,
                Description = EmployeeContactDAO.Description,
                BusinessGroupId = EmployeeContactDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return EmployeeContact;
        }

        public async Task<bool> Create(EmployeeContact EmployeeContact)
        {
            EmployeeContactDAO EmployeeContactDAO = new EmployeeContactDAO();
            
            EmployeeContactDAO.Id = EmployeeContact.Id;
            EmployeeContactDAO.EmployeeDetailId = EmployeeContact.EmployeeDetailId;
            EmployeeContactDAO.Name = EmployeeContact.Name;
            EmployeeContactDAO.Email = EmployeeContact.Email;
            EmployeeContactDAO.Phone = EmployeeContact.Phone;
            EmployeeContactDAO.Address = EmployeeContact.Address;
            EmployeeContactDAO.Description = EmployeeContact.Description;
            EmployeeContactDAO.BusinessGroupId = EmployeeContact.BusinessGroupId;
            EmployeeContactDAO.Disabled = false;
            
            await ERPContext.EmployeeContact.AddAsync(EmployeeContactDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(EmployeeContact EmployeeContact)
        {
            EmployeeContactDAO EmployeeContactDAO = ERPContext.EmployeeContact.Where(b => b.Id == EmployeeContact.Id).FirstOrDefault();
            
            EmployeeContactDAO.Id = EmployeeContact.Id;
            EmployeeContactDAO.EmployeeDetailId = EmployeeContact.EmployeeDetailId;
            EmployeeContactDAO.Name = EmployeeContact.Name;
            EmployeeContactDAO.Email = EmployeeContact.Email;
            EmployeeContactDAO.Phone = EmployeeContact.Phone;
            EmployeeContactDAO.Address = EmployeeContact.Address;
            EmployeeContactDAO.Description = EmployeeContact.Description;
            EmployeeContactDAO.BusinessGroupId = EmployeeContact.BusinessGroupId;
            EmployeeContactDAO.Disabled = false;
            ERPContext.EmployeeContact.Update(EmployeeContactDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            EmployeeContactDAO EmployeeContactDAO = await ERPContext.EmployeeContact.Where(x => x.Id == Id).FirstOrDefaultAsync();
            EmployeeContactDAO.Disabled = true;
            ERPContext.EmployeeContact.Update(EmployeeContactDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
