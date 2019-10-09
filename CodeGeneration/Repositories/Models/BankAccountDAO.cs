using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class BankAccountDAO
    {
        public Guid Id { get; set; }
        public Guid BankId { get; set; }
        public Guid SetOfBookId { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ChartOfAccountId { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual BankDAO Bank { get; set; }
        public virtual ChartOfAccountDAO ChartOfAccount { get; set; }
        public virtual SetOfBookDAO SetOfBook { get; set; }
    }
}
