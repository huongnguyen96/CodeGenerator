
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class CustomerBankAccount : DataEntity
    {
        public Guid Id { get; set; }
		public Guid CustomerDetailId { get; set; }
		public string Name { get; set; }
		public Guid BankId { get; set; }
		public string AccountNumber { get; set; }
		public string AccountName { get; set; }
		public string Branch { get; set; }
		public Guid ProvinceId { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class CustomerBankAccountFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter CustomerDetailId { get; set; }
		public StringFilter Name { get; set; }
		public GuidFilter BankId { get; set; }
		public StringFilter AccountNumber { get; set; }
		public StringFilter AccountName { get; set; }
		public StringFilter Branch { get; set; }
		public GuidFilter ProvinceId { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public CustomerBankAccountOrder OrderBy {get; set;}
        public CustomerBankAccountSelect Selects {get; set;}
    }

    public enum CustomerBankAccountOrder
    {
        
        Name,
        AccountNumber,
        AccountName,
        Branch,
    }

    public enum CustomerBankAccountSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        CustomerDetail = E._2,
        Name = E._3,
        Bank = E._4,
        AccountNumber = E._5,
        AccountName = E._6,
        Branch = E._7,
        Province = E._8,
        BusinessGroup = E._9,
    }
}
