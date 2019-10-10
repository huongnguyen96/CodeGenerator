using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class UserDAO
    {
        public UserDAO()
        {
            Warehouses = new HashSet<WarehouseDAO>();
        }

        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<WarehouseDAO> Warehouses { get; set; }
    }
}
