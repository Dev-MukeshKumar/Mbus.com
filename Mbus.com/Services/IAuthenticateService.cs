using Mbus.com.Entities;
using Mbus.com.Models;
using System;
using System.Threading.Tasks;

namespace Mbus.com.Services
{
    public interface IAuthenticateService
    {
        Task<UserTokenDTO> AuthenticateUser(UserLoginDTO userDetails);
        Task<OwnerTokenDTO> AuthenticateOwner(OwnerLoginDTO ownerDetails);
    }
}
