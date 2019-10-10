
using WeGift.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WeGift.Controllers.user.user_master
{
    public class UserMaster_UserDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<UserMaster_WarehouseDTO> Warehouses { get; set; }
        public UserMaster_UserDTO() {}
        public UserMaster_UserDTO(User User)
        {
            
            this.Id = User.Id;
            this.Username = User.Username;
            this.Password = User.Password;
            this.Warehouses = User.Warehouses?.Select(x => new UserMaster_WarehouseDTO(x)).ToList();

        }
    }

    public class UserMaster_UserFilterDTO : FilterDTO
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Username { get; set; }
        public StringFilter Password { get; set; }
    }
}
