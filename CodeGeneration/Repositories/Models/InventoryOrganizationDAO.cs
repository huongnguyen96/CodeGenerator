using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class InventoryOrganizationDAO
    {
        public InventoryOrganizationDAO()
        {
            InventoryOrganizationAddresses = new HashSet<InventoryOrganizationAddressDAO>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public Guid DivisionId { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual DivisionDAO Division { get; set; }
        public virtual ICollection<InventoryOrganizationAddressDAO> InventoryOrganizationAddresses { get; set; }
    }
}
