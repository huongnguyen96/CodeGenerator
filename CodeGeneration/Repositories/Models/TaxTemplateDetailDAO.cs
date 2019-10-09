using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class TaxTemplateDetailDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public Guid TaxTemplateId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid UnitOfMeasureId { get; set; }
        public decimal Rate { get; set; }
        public string Description { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual TaxTemplateDAO TaxTemplate { get; set; }
        public virtual UnitOfMeasureDAO UnitOfMeasure { get; set; }
    }
}
