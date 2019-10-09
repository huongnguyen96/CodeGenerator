using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class FiscalYearDAO
    {
        public FiscalYearDAO()
        {
            AccountingPeriods = new HashSet<AccountingPeriodDAO>();
        }

        public Guid Id { get; set; }
        public Guid SetOfBookId { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InventoryValuationMethod { get; set; }
        public Guid StatusId { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual SetOfBookDAO SetOfBook { get; set; }
        public virtual EnumMasterDataDAO Status { get; set; }
        public virtual ICollection<AccountingPeriodDAO> AccountingPeriods { get; set; }
    }
}
