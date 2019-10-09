using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class EmployeeDetailDAO
    {
        public EmployeeDetailDAO()
        {
            EmployeeContacts = new HashSet<EmployeeContactDAO>();
            EmployeePositions = new HashSet<EmployeePositionDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid LegalEntityId { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNumber { get; set; }
        public Guid? BankId { get; set; }
        public Guid? ProvinceId { get; set; }
        public string BankBranch { get; set; }
        public string BankAddress { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? JoinDate { get; set; }

        public virtual BankDAO Bank { get; set; }
        public virtual EmployeeDAO Employee { get; set; }
        public virtual LegalEntityDAO LegalEntity { get; set; }
        public virtual ProvinceDAO Province { get; set; }
        public virtual ICollection<EmployeeContactDAO> EmployeeContacts { get; set; }
        public virtual ICollection<EmployeePositionDAO> EmployeePositions { get; set; }
    }
}
