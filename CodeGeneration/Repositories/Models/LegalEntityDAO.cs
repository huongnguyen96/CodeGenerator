using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class LegalEntityDAO
    {
        public LegalEntityDAO()
        {
            APPSPermissions = new HashSet<APPSPermissionDAO>();
            CustomerDetails = new HashSet<CustomerDetailDAO>();
            CustomerGroupings = new HashSet<CustomerGroupingDAO>();
            Divisions = new HashSet<DivisionDAO>();
            EmployeeDetails = new HashSet<EmployeeDetailDAO>();
            ItemDetails = new HashSet<ItemDetailDAO>();
            ItemGroupings = new HashSet<ItemGroupingDAO>();
            Positions = new HashSet<PositionDAO>();
            SupplierDetails = new HashSet<SupplierDetailDAO>();
            SupplierGroupings = new HashSet<SupplierGroupingDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid SetOfBookId { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual SetOfBookDAO SetOfBook { get; set; }
        public virtual ICollection<APPSPermissionDAO> APPSPermissions { get; set; }
        public virtual ICollection<CustomerDetailDAO> CustomerDetails { get; set; }
        public virtual ICollection<CustomerGroupingDAO> CustomerGroupings { get; set; }
        public virtual ICollection<DivisionDAO> Divisions { get; set; }
        public virtual ICollection<EmployeeDetailDAO> EmployeeDetails { get; set; }
        public virtual ICollection<ItemDetailDAO> ItemDetails { get; set; }
        public virtual ICollection<ItemGroupingDAO> ItemGroupings { get; set; }
        public virtual ICollection<PositionDAO> Positions { get; set; }
        public virtual ICollection<SupplierDetailDAO> SupplierDetails { get; set; }
        public virtual ICollection<SupplierGroupingDAO> SupplierGroupings { get; set; }
    }
}
