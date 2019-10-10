namespace CodeGeneration.Repositories.Models
{
    public partial class WarehouseDAO
    {
        public long Id { get; set; }
        public long ManagerId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual UserDAO Manager { get; set; }
    }
}
