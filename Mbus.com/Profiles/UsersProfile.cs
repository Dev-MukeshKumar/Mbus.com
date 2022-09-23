using AutoMapper;
using Mbus.com.Entities;
using Mbus.com.Models;

namespace Mbus.com.Profiles
{
    public class UsersProfile: Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserTokenDTO>();
            CreateMap<UserCreationDTO, User>();
            CreateMap<UserUpdationDTO, User>();
        }
    }
}
