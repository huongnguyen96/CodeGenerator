using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class Employee_InventoryOrganizationAddressDAO
    {
        public Guid InventoryOrganizationAddressId { get; set; }
        public Guid EmployeeId { get; set; }
        public long CX { get; set; }

        public virtual EmployeeDAO Employee { get; set; }
        public virtual InventoryOrganizationAddressDAO InventoryOrganizationAddress { get; set; }
    }
}
