
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MCustomer
{
    public interface ICustomerService : IServiceScoped
    {
        Task<int> Count(CustomerFilter CustomerFilter);
        Task<List<Customer>> List(CustomerFilter CustomerFilter);
        Task<Customer> Get(long Id);
        Task<Customer> Create(Customer Customer);
        Task<Customer> Update(Customer Customer);
        Task<Customer> Delete(Customer Customer);
    }

    public class CustomerService : ICustomerService
    {
        public IUOW UOW;
        public ICustomerValidator CustomerValidator;

        public CustomerService(
            IUOW UOW, 
            ICustomerValidator CustomerValidator
        )
        {
            this.UOW = UOW;
            this.CustomerValidator = CustomerValidator;
        }
        public async Task<int> Count(CustomerFilter CustomerFilter)
        {
            int result = await UOW.CustomerRepository.Count(CustomerFilter);
            return result;
        }

        public async Task<List<Customer>> List(CustomerFilter CustomerFilter)
        {
            List<Customer> Customers = await UOW.CustomerRepository.List(CustomerFilter);
            return Customers;
        }

        public async Task<Customer> Get(long Id)
        {
            Customer Customer = await UOW.CustomerRepository.Get(Id);
            if (Customer == null)
                return null;
            return Customer;
        }

        public async Task<Customer> Create(Customer Customer)
        {
            if (!await CustomerValidator.Create(Customer))
                return Customer;

            try
            {
               
                await UOW.Begin();
                await UOW.CustomerRepository.Create(Customer);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(Customer, "", nameof(CustomerService));
                return await UOW.CustomerRepository.Get(Customer.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(CustomerService));
                throw new MessageException(ex);
            }
        }

        public async Task<Customer> Update(Customer Customer)
        {
            if (!await CustomerValidator.Update(Customer))
                return Customer;
            try
            {
                var oldData = await UOW.CustomerRepository.Get(Customer.Id);

                await UOW.Begin();
                await UOW.CustomerRepository.Update(Customer);
                await UOW.Commit();

                var newData = await UOW.CustomerRepository.Get(Customer.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(CustomerService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(CustomerService));
                throw new MessageException(ex);
            }
        }

        public async Task<Customer> Delete(Customer Customer)
        {
            if (!await CustomerValidator.Delete(Customer))
                return Customer;

            try
            {
                await UOW.Begin();
                await UOW.CustomerRepository.Delete(Customer);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", Customer, nameof(CustomerService));
                return Customer;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(CustomerService));
                throw new MessageException(ex);
            }
        }
    }
}
