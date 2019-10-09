using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class APPSPermissionDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public Guid UserId { get; set; }
        public Guid? BusinessGroupId { get; set; }
        public Guid? SetOfBookId { get; set; }
        public Guid? LegalEntityId { get; set; }
        public Guid? DivisionId { get; set; }

        public virtual BusinessGroupDAO BusinessGroup { get; set; }
        public virtual DivisionDAO Division { get; set; }
        public virtual LegalEntityDAO LegalEntity { get; set; }
        public virtual SetOfBookDAO SetOfBook { get; set; }
        public virtual UserDAO User { get; set; }
    }
}
