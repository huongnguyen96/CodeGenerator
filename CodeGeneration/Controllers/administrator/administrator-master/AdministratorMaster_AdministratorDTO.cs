
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.administrator.administrator_master
{
    public class AdministratorMaster_AdministratorDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public AdministratorMaster_AdministratorDTO() {}
        public AdministratorMaster_AdministratorDTO(Administrator Administrator)
        {
            
            this.Id = Administrator.Id;
            this.Username = Administrator.Username;
            this.DisplayName = Administrator.DisplayName;
        }
    }

    public class AdministratorMaster_AdministratorFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
    }
}
