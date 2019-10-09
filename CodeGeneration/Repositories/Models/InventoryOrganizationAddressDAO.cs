using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class InventoryOrganizationAddressDAO
    {
        public InventoryOrganizationAddressDAO()
        {
            Employee_InventoryOrganizationAddresses = new HashSet<Employee_InventoryOrganizationAddressDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Guid InventoryOrganizationId { get; set; }

        public virtual InventoryOrganizationDAO InventoryOrganization { get; set; }
        public virtual ICollection<Employee_InventoryOrganizationAddressDAO> Employee_InventoryOrganizationAddresses { get; set; }
    }
}
