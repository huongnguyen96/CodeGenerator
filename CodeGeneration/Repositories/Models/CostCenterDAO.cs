using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class CostCenterDAO
    {
        public Guid Id { get; set; }
        public bool Disabled { get; set; }
        public long CX { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? ChartOfAccountId { get; set; }
        public Guid SetOfBookId { get; set; }
        public Guid BusinessGroupId { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string Description { get; set; }

        public virtual ChartOfAccountDAO ChartOfAccount { get; set; }
        public virtual SetOfBookDAO SetOfBook { get; set; }
    }
}
