
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class EmployeeDetail : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public Guid EmployeeId { get; set; }
		public Guid LegalEntityId { get; set; }
		public string BankAccountName { get; set; }
		public string BankAccountNumber { get; set; }
		public Guid? BankId { get; set; }
		public Guid? ProvinceId { get; set; }
		public string BankBranch { get; set; }
		public string BankAddress { get; set; }
		public Guid? EffectiveDate { get; set; }
		public Guid? EndDate { get; set; }
		public Guid? JoinDate { get; set; }
		
    }

    public class EmployeeDetailFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter EmployeeId { get; set; }
		public GuidFilter LegalEntityId { get; set; }
		public StringFilter BankAccountName { get; set; }
		public StringFilter BankAccountNumber { get; set; }
		public GuidFilter BankId { get; set; }
		public GuidFilter ProvinceId { get; set; }
		public StringFilter BankBranch { get; set; }
		public StringFilter BankAddress { get; set; }
		public GuidFilter EffectiveDate { get; set; }
		public GuidFilter EndDate { get; set; }
		public GuidFilter JoinDate { get; set; }
		
        public EmployeeDetailOrder OrderBy {get; set;}
        public EmployeeDetailSelect Selects {get; set;}
    }

    public enum EmployeeDetailOrder
    {
        
        Disabled,
        BankAccountName,
        BankAccountNumber,
        BankBranch,
        BankAddress,
        EffectiveDate,
        EndDate,
        JoinDate,
    }

    public enum EmployeeDetailSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        Employee = E._3,
        LegalEntity = E._4,
        BankAccountName = E._5,
        BankAccountNumber = E._6,
        Bank = E._7,
        Province = E._8,
        BankBranch = E._9,
        BankAddress = E._10,
        EffectiveDate = E._11,
        EndDate = E._12,
        JoinDate = E._13,
    }
}
