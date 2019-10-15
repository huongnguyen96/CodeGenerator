
using WeGift.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WeGift.Controllers.warehouse.warehouse_detail
{
    public class WarehouseDetail_UserDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public WarehouseDetail_UserDTO() {}
        public WarehouseDetail_UserDTO(User User)
        {
            
            this.Id = User.Id;
            this.Username = User.Username;
            this.Password = User.Password;
        }
    }

    public class WarehouseDetail_UserFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
