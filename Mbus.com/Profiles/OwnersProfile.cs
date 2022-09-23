using AutoMapper;
using Mbus.com.Entities;
using Mbus.com.Models;

namespace Mbus.com.Profiles
{
    public class OwnersProfile: Profile
    {
        public OwnersProfile()
        {
            CreateMap<Owner, OwnerDTO>().ReverseMap();
            CreateMap<OwnerCreationDTO,Owner>();
            CreateMap<Owner, OwnerTokenDTO>();
        }
    }
}
