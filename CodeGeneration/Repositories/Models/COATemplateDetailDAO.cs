using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class COATemplateDetailDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public Guid COATemplateId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual COATemplateDAO COATemplate { get; set; }
    }
}
