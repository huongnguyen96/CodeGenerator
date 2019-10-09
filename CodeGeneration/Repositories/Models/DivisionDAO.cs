using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class DivisionDAO
    {
        public DivisionDAO()
        {
            APPSPermissions = new HashSet<APPSPermissionDAO>();
            AssetOrganizations = new HashSet<AssetOrganizationDAO>();
            HROrganizations = new HashSet<HROrganizationDAO>();
            InventoryOrganizations = new HashSet<InventoryOrganizationDAO>();
            ProjectOrganizations = new HashSet<ProjectOrganizationDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid LegalEntityId { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public Guid BusinessGroupId { get; set; }
        public string Description { get; set; }

        public virtual LegalEntityDAO LegalEntity { get; set; }
        public virtual ICollection<APPSPermissionDAO> APPSPermissions { get; set; }
        public virtual ICollection<AssetOrganizationDAO> AssetOrganizations { get; set; }
        public virtual ICollection<HROrganizationDAO> HROrganizations { get; set; }
        public virtual ICollection<InventoryOrganizationDAO> InventoryOrganizations { get; set; }
        public virtual ICollection<ProjectOrganizationDAO> ProjectOrganizations { get; set; }
    }
}
