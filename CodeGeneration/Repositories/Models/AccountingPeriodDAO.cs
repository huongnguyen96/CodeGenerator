using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class AccountingPeriodDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid FiscalYearId { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public Guid StatusId { get; set; }
        public string Description { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual FiscalYearDAO FiscalYear { get; set; }
        public virtual EnumMasterDataDAO Status { get; set; }
    }
}
