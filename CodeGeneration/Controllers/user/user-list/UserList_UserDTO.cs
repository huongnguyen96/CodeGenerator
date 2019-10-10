
using WeGift.Entities;
using Common;
using System;
using System.Collections.Generic;

namespace WeGift.Controllers.user.user_list
{
    public class UserList_UserDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Warehouse> Warehouses { get; set; }
        public UserList_UserDTO() {}
        public UserList_UserDTO(User User)
        {
            
            this.Id = User.Id;
            this.Username = User.Username;
            this.Password = User.Password;
            this.Warehouses = User.Warehouses?.Select(x => new UserList_WarehouseDTO(x)).ToList();

        }
    }

    public class UserList_UserFilterDTO : FilterDTO
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Username { get; set; }
        public StringFilter Password { get; set; }
    }
}
