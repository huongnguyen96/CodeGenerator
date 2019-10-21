
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.administrator.administrator_detail
{
    public class AdministratorDetail_AdministratorDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public AdministratorDetail_AdministratorDTO() {}
        public AdministratorDetail_AdministratorDTO(Administrator Administrator)
        {
            
            this.Id = Administrator.Id;
            this.Username = Administrator.Username;
            this.DisplayName = Administrator.DisplayName;
        }
    }

    public class AdministratorDetail_AdministratorFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
    }
}
