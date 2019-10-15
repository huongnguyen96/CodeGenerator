
using WeGift.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WeGift.Controllers.user.user_detail
{
    public class UserDetail_UserDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<UserDetail_WarehouseDTO> Warehouses { get; set; }
        public UserDetail_UserDTO() {}
        public UserDetail_UserDTO(User User)
        {
            
            this.Id = User.Id;
            this.Username = User.Username;
            this.Password = User.Password;
            this.Warehouses = User.Warehouses?.Select(x => new UserDetail_WarehouseDTO(x)).ToList();

        }
    }

    public class UserDetail_UserFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
