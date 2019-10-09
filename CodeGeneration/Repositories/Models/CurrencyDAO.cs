using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class CurrencyDAO
    {
        public CurrencyDAO()
        {
            SetOfBooks = new HashSet<SetOfBookDAO>();
        }

        public Guid Id { get; set; }
        public Guid BusinessGroupId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Disabled { get; set; }
        public long CX { get; set; }
        public int Sequence { get; set; }
        public string Description { get; set; }

        public virtual BusinessGroupDAO BusinessGroup { get; set; }
        public virtual ICollection<SetOfBookDAO> SetOfBooks { get; set; }
    }
}
