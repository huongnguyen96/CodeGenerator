using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class AssetOrganizationDAO
    {
        public AssetOrganizationDAO()
        {
            Asset_AssetOrganizations = new HashSet<Asset_AssetOrganizationDAO>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid DivisionId { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual DivisionDAO Division { get; set; }
        public virtual ICollection<Asset_AssetOrganizationDAO> Asset_AssetOrganizations { get; set; }
    }
}
