using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class AssetDAO
    {
        public AssetDAO()
        {
            Asset_AssetOrganizations = new HashSet<Asset_AssetOrganizationDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid TypeId { get; set; }
        public Guid StatusId { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual BusinessGroupDAO BusinessGroup { get; set; }
        public virtual EnumMasterDataDAO Status { get; set; }
        public virtual EnumMasterDataDAO Type { get; set; }
        public virtual ICollection<Asset_AssetOrganizationDAO> Asset_AssetOrganizations { get; set; }
    }
}
