using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class COATemplateDAO
    {
        public COATemplateDAO()
        {
            COATemplateDetails = new HashSet<COATemplateDetailDAO>();
        }

        public Guid Id { get; set; }
        public Guid BusinessGroupId { get; set; }
        public long CX { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public virtual BusinessGroupDAO BusinessGroup { get; set; }
        public virtual ICollection<COATemplateDetailDAO> COATemplateDetails { get; set; }
    }
}
