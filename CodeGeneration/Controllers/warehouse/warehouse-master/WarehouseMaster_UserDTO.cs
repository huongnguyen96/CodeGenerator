
using WeGift.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WeGift.Controllers.warehouse.warehouse_master
{
    public class WarehouseMaster_UserDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public WarehouseMaster_UserDTO() {}
        public WarehouseMaster_UserDTO(User User)
        {
            
            this.Id = User.Id;
            this.Username = User.Username;
            this.Password = User.Password;
        }
    }

    public class WarehouseMaster_UserFilterDTO : FilterDTO
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Username { get; set; }
        public StringFilter Password { get; set; }
    }
}
