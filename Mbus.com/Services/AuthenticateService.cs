using AutoMapper;
using Mbus.com.Entities;
using Mbus.com.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mbus.com.Services
{
    public class AuthenticateService: IAuthenticateService
    {
        private readonly IUserServices _userServices;
        private readonly IOwnerServices _ownerServices;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        public AuthenticateService(IUserServices userServices, IOptions<AppSettings> appSettings, IOwnerServices ownerServices, IMapper mapper)
        {
            _userServices = userServices;
            _appSettings = appSettings.Value;
            _ownerServices = ownerServices;
            _mapper = mapper;
        }

        public async Task<UserTokenDTO> AuthenticateUser(UserLoginDTO userDetails)
        {
            var user = await _userServices.GetUserByEmail(userDetails.Email, userDetails.Password);

            if(user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var expirationTime = double.Parse(_appSettings.ExpirationInMinutes);
            var secret = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, "user"),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(expirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userTokenDTO = _mapper.Map<UserTokenDTO>(user);

            userTokenDTO.Token = tokenHandler.WriteToken(token); 

            return userTokenDTO;
        }

        public async Task<OwnerTokenDTO> AuthenticateOwner(OwnerLoginDTO ownerDetails)
        {
            var owner = await _ownerServices.GetOwnerByEmail(ownerDetails.Email, ownerDetails.Password);

            if (owner == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var expirationTime = double.Parse(_appSettings.ExpirationInMinutes);
            var secret = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, owner.Email),
                    new Claim(ClaimTypes.Name, owner.Name),
                    new Claim(ClaimTypes.Role, "owner"),
                    new Claim(ClaimTypes.UserData, owner.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(expirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var ownerTokenDTO = _mapper.Map<OwnerTokenDTO>(owner);

            ownerTokenDTO.Token = tokenHandler.WriteToken(token);

            return ownerTokenDTO;
        }
    }
}
