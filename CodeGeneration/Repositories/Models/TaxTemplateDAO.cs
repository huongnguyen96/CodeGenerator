using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class TaxTemplateDAO
    {
        public TaxTemplateDAO()
        {
            TaxTemplateDetails = new HashSet<TaxTemplateDetailDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual BusinessGroupDAO BusinessGroup { get; set; }
        public virtual ICollection<TaxTemplateDetailDAO> TaxTemplateDetails { get; set; }
    }
}
