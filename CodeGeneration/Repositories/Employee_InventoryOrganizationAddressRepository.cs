
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
    public interface IEmployee_InventoryOrganizationAddressRepository
    {
        Task<int> Count(Employee_InventoryOrganizationAddressFilter Employee_InventoryOrganizationAddressFilter);
        Task<List<Employee_InventoryOrganizationAddress>> List(Employee_InventoryOrganizationAddressFilter Employee_InventoryOrganizationAddressFilter);
        Task<Employee_InventoryOrganizationAddress> Get(Guid Id);
        Task<bool> Create(Employee_InventoryOrganizationAddress Employee_InventoryOrganizationAddress);
        Task<bool> Update(Employee_InventoryOrganizationAddress Employee_InventoryOrganizationAddress);
        Task<bool> Delete(Guid Id);
        
    }
    public class Employee_InventoryOrganizationAddressRepository : IEmployee_InventoryOrganizationAddressRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public Employee_InventoryOrganizationAddressRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<Employee_InventoryOrganizationAddressDAO> DynamicFilter(IQueryable<Employee_InventoryOrganizationAddressDAO> query, Employee_InventoryOrganizationAddressFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.InventoryOrganizationAddressId != null)
                query = query.Where(q => q.InventoryOrganizationAddressId, filter.InventoryOrganizationAddressId);
            if (filter.EmployeeId != null)
                query = query.Where(q => q.EmployeeId, filter.EmployeeId);
            return query;
        }
        private IQueryable<Employee_InventoryOrganizationAddressDAO> DynamicOrder(IQueryable<Employee_InventoryOrganizationAddressDAO> query,  Employee_InventoryOrganizationAddressFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
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

        private async Task<List<Employee_InventoryOrganizationAddress>> DynamicSelect(IQueryable<Employee_InventoryOrganizationAddressDAO> query, Employee_InventoryOrganizationAddressFilter filter)
        {
            List <Employee_InventoryOrganizationAddress> Employee_InventoryOrganizationAddresss = await query.Select(q => new Employee_InventoryOrganizationAddress()
            {
                
                InventoryOrganizationAddressId = filter.Selects.Contains(Employee_InventoryOrganizationAddressSelect.InventoryOrganizationAddress) ? q.InventoryOrganizationAddressId : default(Guid),
                EmployeeId = filter.Selects.Contains(Employee_InventoryOrganizationAddressSelect.Employee) ? q.EmployeeId : default(Guid),
            }).ToListAsync();
            return Employee_InventoryOrganizationAddresss;
        }

        public async Task<int> Count(Employee_InventoryOrganizationAddressFilter filter)
        {
            IQueryable <Employee_InventoryOrganizationAddressDAO> Employee_InventoryOrganizationAddressDAOs = ERPContext.Employee_InventoryOrganizationAddress;
            Employee_InventoryOrganizationAddressDAOs = DynamicFilter(Employee_InventoryOrganizationAddressDAOs, filter);
            return await Employee_InventoryOrganizationAddressDAOs.CountAsync();
        }

        public async Task<List<Employee_InventoryOrganizationAddress>> List(Employee_InventoryOrganizationAddressFilter filter)
        {
            if (filter == null) return new List<Employee_InventoryOrganizationAddress>();
            IQueryable<Employee_InventoryOrganizationAddressDAO> Employee_InventoryOrganizationAddressDAOs = ERPContext.Employee_InventoryOrganizationAddress;
            Employee_InventoryOrganizationAddressDAOs = DynamicFilter(Employee_InventoryOrganizationAddressDAOs, filter);
            Employee_InventoryOrganizationAddressDAOs = DynamicOrder(Employee_InventoryOrganizationAddressDAOs, filter);
            var Employee_InventoryOrganizationAddresss = await DynamicSelect(Employee_InventoryOrganizationAddressDAOs, filter);
            return Employee_InventoryOrganizationAddresss;
        }

        public async Task<Employee_InventoryOrganizationAddress> Get(Guid Id)
        {
            Employee_InventoryOrganizationAddress Employee_InventoryOrganizationAddress = await ERPContext.Employee_InventoryOrganizationAddress.Where(l => l.Id == Id).Select(Employee_InventoryOrganizationAddressDAO => new Employee_InventoryOrganizationAddress()
            {
                 
                InventoryOrganizationAddressId = Employee_InventoryOrganizationAddressDAO.InventoryOrganizationAddressId,
                EmployeeId = Employee_InventoryOrganizationAddressDAO.EmployeeId,
            }).FirstOrDefaultAsync();
            return Employee_InventoryOrganizationAddress;
        }

        public async Task<bool> Create(Employee_InventoryOrganizationAddress Employee_InventoryOrganizationAddress)
        {
            Employee_InventoryOrganizationAddressDAO Employee_InventoryOrganizationAddressDAO = new Employee_InventoryOrganizationAddressDAO();
            
            Employee_InventoryOrganizationAddressDAO.InventoryOrganizationAddressId = Employee_InventoryOrganizationAddress.InventoryOrganizationAddressId;
            Employee_InventoryOrganizationAddressDAO.EmployeeId = Employee_InventoryOrganizationAddress.EmployeeId;
            Employee_InventoryOrganizationAddressDAO.Disabled = false;
            
            await ERPContext.Employee_InventoryOrganizationAddress.AddAsync(Employee_InventoryOrganizationAddressDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Employee_InventoryOrganizationAddress Employee_InventoryOrganizationAddress)
        {
            Employee_InventoryOrganizationAddressDAO Employee_InventoryOrganizationAddressDAO = ERPContext.Employee_InventoryOrganizationAddress.Where(b => b.Id == Employee_InventoryOrganizationAddress.Id).FirstOrDefault();
            
            Employee_InventoryOrganizationAddressDAO.InventoryOrganizationAddressId = Employee_InventoryOrganizationAddress.InventoryOrganizationAddressId;
            Employee_InventoryOrganizationAddressDAO.EmployeeId = Employee_InventoryOrganizationAddress.EmployeeId;
            Employee_InventoryOrganizationAddressDAO.Disabled = false;
            ERPContext.Employee_InventoryOrganizationAddress.Update(Employee_InventoryOrganizationAddressDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            Employee_InventoryOrganizationAddressDAO Employee_InventoryOrganizationAddressDAO = await ERPContext.Employee_InventoryOrganizationAddress.Where(x => x.Id == Id).FirstOrDefaultAsync();
            Employee_InventoryOrganizationAddressDAO.Disabled = true;
            ERPContext.Employee_InventoryOrganizationAddress.Update(Employee_InventoryOrganizationAddressDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
