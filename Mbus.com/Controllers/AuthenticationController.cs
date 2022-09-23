using AutoMapper;
using Mbus.com.Models;
using Mbus.com.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Mbus.com.Controllers
{
    [ApiController]
    [Route("authenticate")]
    public class AuthenticationController: ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticateService authenticateService, IMapper mapper, ILogger<AuthenticationController> logger)
        {
            _authenticateService = authenticateService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("user")]
        public async Task<IActionResult> UserAuthentication([FromBody] UserLoginDTO userData)
        {
            var user = await _authenticateService.AuthenticateUser(userData);

            if (user == null)
            {
                _logger.LogError($"Trying to bypass authentication with not registered data.");
                return BadRequest(new { message = "email or password is incorrect" });
            }

            var userToReturn = _mapper.Map<UserTokenDTO>(user);
            return Ok(userToReturn);
        }

        [HttpPost("owner")]
        public async Task<IActionResult> OwnerAuthentication([FromBody] OwnerLoginDTO ownerData)
        {
            var owner = await _authenticateService.AuthenticateOwner(ownerData);

            if (owner == null)
                return BadRequest(new { message = "email or password is incorrect" });

            var ownerToReturn = _mapper.Map<OwnerTokenDTO>(owner);
            return Ok(ownerToReturn);
        }
    }
}
