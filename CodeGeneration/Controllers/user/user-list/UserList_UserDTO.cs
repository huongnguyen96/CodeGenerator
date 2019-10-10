
using WeGift.Entities;
using Common;
using System;
using System.Collections.Generic;

namespace WeGift.Controllers.user.userList
{
    public class UserList_UserDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserList_UserDTO() {}
        public UserList_UserDTO(User User)
        {
            
            this.Id = User.Id;
            this.Username = User.Username;
            this.Password = User.Password;
        }
    }

    public class UserList_UserFilterDTO : FilterDTO
    {
    }
}
