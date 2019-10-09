using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class PaymentMethodDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid SetOfBookId { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual SetOfBookDAO SetOfBook { get; set; }
    }
}
