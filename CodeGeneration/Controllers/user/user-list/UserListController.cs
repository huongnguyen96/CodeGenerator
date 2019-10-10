
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeGift.Services.MUserService;
using Microsoft.AspNetCore.Mvc;
using WeGift.Entities;

namespace WeGift.Controllers.user.userList
{
    public class UserListRoute : Root
    {
        public const string FE = "user/user-list";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
    }

    public class UserListController : ApiController
    {
        private IUserService UserService;

        public UserListController(
            IUserService UserService
        )
        {
            this.UserService = UserService;
        }

        [Route(UserListRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] UserList_UserFilterDTO UserList_UserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            UserFilter UserFilter = ConvertFilterDTOtoFilterBO(UserList_UserFilterDTO);

            return await UserService.Count(UserFilter);
        }

        [Route(UserListRoute.List), HttpPost]
        public async Task<List<UserList_UserDTO>> List([FromBody] UserList_UserFilterDTO UserList_UserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            UserFilter UserFilter = ConvertFilterDTOtoFilterEntity(UserList_UserFilterDTO);

            List<User> Users = await UserService.List(CustomerFilter);

            return Users.Select(c => new UserList_UserDTO(c)).ToList();
        }

        [Route(UserListRoute.Get), HttpPost]
        public async Task<UserList_UserDTO> Get([FromBody]UserList_UserDTO UserList_UserDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            User User = await UserService.Get(UserList_UserDTO.Id);
            return new UserList_UserDTO(Customer);
        }


        public UserFilter ConvertFilterDTOtoFilterEntity(UserList_UserFilterDTO UserFilter_UserDTO)
        {
            UserFilter UserFilter = new UserFilter();
            return UserFilter;
        }
    }
}
