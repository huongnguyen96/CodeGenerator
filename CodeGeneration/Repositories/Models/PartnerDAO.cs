using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class PartnerDAO
    {
        public PartnerDAO()
        {
            Items = new HashSet<ItemDAO>();
            Warehouses = new HashSet<WarehouseDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }

        public virtual ICollection<ItemDAO> Items { get; set; }
        public virtual ICollection<WarehouseDAO> Warehouses { get; set; }
    }
}
