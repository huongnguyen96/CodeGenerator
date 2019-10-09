using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class Asset_AssetOrganizationDAO
    {
        public Guid AssetOrganizationId { get; set; }
        public Guid AssetId { get; set; }
        public long CX { get; set; }
        public Guid BusinessGroupId { get; set; }
        public Guid? OwnerId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public virtual AssetDAO Asset { get; set; }
        public virtual AssetOrganizationDAO AssetOrganization { get; set; }
    }
}
