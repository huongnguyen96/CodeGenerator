using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class TaxDAO
    {
        public TaxDAO()
        {
            InverseParent = new HashSet<TaxDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid SetOfBookId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal? Rate { get; set; }
        public string Description { get; set; }
        public Guid? UnitOfMeasureId { get; set; }
        public string Type { get; set; }
        public Guid BusinessGroupId { get; set; }
        public Guid? ParentId { get; set; }

        public virtual TaxDAO Parent { get; set; }
        public virtual SetOfBookDAO SetOfBook { get; set; }
        public virtual UnitOfMeasureDAO UnitOfMeasure { get; set; }
        public virtual ICollection<TaxDAO> InverseParent { get; set; }
    }
}
