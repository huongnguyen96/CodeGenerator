

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MUser;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.user.user_master
{
    public class UserMasterRoute : Root
    {
        public const string FE = "/user/user-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
    }

    public class UserMasterController : ApiController
    {
        
        
        private IUserService UserService;

        public UserMasterController(
            
            IUserService UserService
        )
        {
            
            this.UserService = UserService;
        }


        [Route(UserMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] UserMaster_UserFilterDTO UserMaster_UserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            UserFilter UserFilter = ConvertFilterDTOToFilterEntity(UserMaster_UserFilterDTO);

            return await UserService.Count(UserFilter);
        }

        [Route(UserMasterRoute.List), HttpPost]
        public async Task<List<UserMaster_UserDTO>> List([FromBody] UserMaster_UserFilterDTO UserMaster_UserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            UserFilter UserFilter = ConvertFilterDTOToFilterEntity(UserMaster_UserFilterDTO);

            List<User> Users = await UserService.List(UserFilter);

            return Users.Select(c => new UserMaster_UserDTO(c)).ToList();
        }

        [Route(UserMasterRoute.Get), HttpPost]
        public async Task<UserMaster_UserDTO> Get([FromBody]UserMaster_UserDTO UserMaster_UserDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            User User = await UserService.Get(UserMaster_UserDTO.Id);
            return new UserMaster_UserDTO(User);
        }


        public UserFilter ConvertFilterDTOToFilterEntity(UserMaster_UserFilterDTO UserMaster_UserFilterDTO)
        {
            UserFilter UserFilter = new UserFilter();
            
            UserFilter.Id = new LongFilter{ Equal = UserMaster_UserFilterDTO.Id };
            UserFilter.Username = new StringFilter{ StartsWith = UserMaster_UserFilterDTO.Username };
            UserFilter.Password = new StringFilter{ StartsWith = UserMaster_UserFilterDTO.Password };
            return UserFilter;
        }
        
        
    }
}
