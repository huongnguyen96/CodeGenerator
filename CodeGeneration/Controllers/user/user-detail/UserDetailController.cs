

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WeGift.Services.MUser;
using Microsoft.AspNetCore.Mvc;
using WeGift.Entities;



namespace WeGift.Controllers.user.user_detail
{
    public class UserDetailRoute : Root
    {
        public const string FE = "/user/user-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class UserDetailController : ApiController
    {
        
        
        private IUserService UserService;

        public UserDetailController(
            
            IUserService UserService
        )
        {
            
            this.UserService = UserService;
        }


        [Route(UserDetailRoute.Get), HttpPost]
        public async Task<UserDetail_UserDTO> Get([FromBody]UserDetail_UserDTO UserDetail_UserDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            User User = await UserService.Get(UserDetail_UserDTO.Id);
            return new UserDetail_UserDTO(User);
        }


        [Route(UserDetailRoute.Create), HttpPost]
        public async Task<ActionResult<UserDetail_UserDTO>> Create([FromBody] UserDetail_UserDTO UserDetail_UserDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            User User = ConvertDTOToEntity(UserDetail_UserDTO);

            User = await UserService.Create(User);
            UserDetail_UserDTO = new UserDetail_UserDTO(User);
            if (User.IsValidated)
                return UserDetail_UserDTO;
            else
                return BadRequest(UserDetail_UserDTO);        
        }

        [Route(UserDetailRoute.Update), HttpPost]
        public async Task<ActionResult<UserDetail_UserDTO>> Update([FromBody] UserDetail_UserDTO UserDetail_UserDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            User User = ConvertDTOToEntity(UserDetail_UserDTO);

            User = await UserService.Update(User);
            UserDetail_UserDTO = new UserDetail_UserDTO(User);
            if (User.IsValidated)
                return UserDetail_UserDTO;
            else
                return BadRequest(UserDetail_UserDTO);        
        }

        [Route(UserDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<UserDetail_UserDTO>> Delete([FromBody] UserDetail_UserDTO UserDetail_UserDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            User User = ConvertDTOToEntity(UserDetail_UserDTO);

            User = await UserService.Delete(User);
            UserDetail_UserDTO = new UserDetail_UserDTO(User);
            if (User.IsValidated)
                return UserDetail_UserDTO;
            else
                return BadRequest(UserDetail_UserDTO);        
        }

        public User ConvertDTOToEntity(UserDetail_UserDTO UserDetail_UserDTO)
        {
            User User = new User();
            
            User.Id = UserDetail_UserDTO.Id;
            User.Username = UserDetail_UserDTO.Username;
            User.Password = UserDetail_UserDTO.Password;
            return User;
        }
        
        
    }
}
