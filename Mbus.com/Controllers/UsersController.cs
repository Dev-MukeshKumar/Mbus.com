using AutoMapper;
using Mbus.com.Entities;
using Mbus.com.Helpers.Parameters;
using Mbus.com.Models;
using Mbus.com.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mbus.com.Controllers
{
    [ApiController]
    [Authorize(Roles = "user")]
    [Route("[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public UsersController(IUserServices userServices, IMapper mapper){
            _userServices = userServices;
            _mapper = mapper;
        }

        [HttpGet("{UserId}",Name ="GetUser")]
        public async Task<ActionResult<UserDTO>> GetUser(Guid UserId)
        {

            if(UserId == null || UserId == Guid.Empty)
                throw new ArgumentNullException(nameof(UserId));

            var user = await _userServices.GetUserById(UserId);

            if(user == null)
                return NoContent();

            //var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userToReturn = _mapper.Map<UserDTO>(user);

            return Ok(userToReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationDTO userDetails)
        {
            if(userDetails == null || userDetails.Name == null || userDetails.Password == null || userDetails.Email == null)
            {
                return BadRequest("Enter the name, email, and password of the user");
            }
            var user = _mapper.Map<Entities.User>(userDetails);

            var response = await _userServices.RegisterUser(user);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var userResource = _mapper.Map<UserDTO>(response.User);
            return CreatedAtRoute("GetUser",new {UserId = userResource.Id},userResource);
        }

        [HttpGet("{UserId}/buses", Name = "GetAllBus")]
        public IActionResult GetAllBus([FromQuery] BusResourceParamter resourceParamter)
        {
            if (resourceParamter == null)
                return BadRequest(nameof(resourceParamter) + " was null.");

            var buses = _userServices.GetAllBus(resourceParamter);
            var busesToReturn = _mapper.Map<IEnumerable<BusToReturnDTO>>(buses);

            return Ok(busesToReturn);
        }

        [HttpGet("{UserId}/buses/{BusId}", Name = "GetBusByUser")]
        public async Task<IActionResult> GetBus(Guid UserId, Guid BusId)
        {
            if (UserId == null || UserId == Guid.Empty)
                return BadRequest("Enter user id.");

            if (BusId == null || BusId == Guid.Empty)
                return BadRequest("Enter bus id.");

            var bus = await _userServices.GetBusById(BusId);
            var busToReturn = _mapper.Map<BusToReturnDTO>(bus);

            return Ok(busToReturn);
        }

        [HttpGet("{UserId}/tickets/{TicketId}", Name ="GetTicket")]
        public async Task<IActionResult> GetTicket(Guid UserId,Guid TicketId)
        {
            if (UserId == null || UserId == Guid.Empty || TicketId == null || TicketId == Guid.Empty)
                return BadRequest("Enter required ids.");

            var response = await _userServices.confirmBooking(TicketId);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var ticketToReturn = _mapper.Map<UserTicketToReturnDTO>(response.Ticket);

            return Ok(ticketToReturn);
        }

        [HttpGet("{UserId}/tickets",Name = "GetAllTickets")]
        public IActionResult GetAllTickets([FromQuery] TicketResourceParameter resourceParameter, Guid UserId)
        {
            if(UserId == Guid.Empty || UserId == null)
                return BadRequest("Enter valid user id.");

            if(resourceParameter == null || resourceParameter.BusId == null || resourceParameter.BusId == Guid.Empty || resourceParameter.TravelDate == DateTime.MinValue)
                return BadRequest("Enter valid query details.");

            var tickets = _userServices.GetAllTicket(UserId, resourceParameter);
            var ticketsToReturn = _mapper.Map<IEnumerable<UserTicketToReturnDTO>>(tickets);

            return Ok(ticketsToReturn);
        }

        [HttpPost("{UserId}/tickets")]
        public async Task<IActionResult> BookTicket(Guid UserId, [FromBody] TicketCreationDTO TicketDetails)
        {
            if (TicketDetails.BusId == null || TicketDetails.BusId == Guid.Empty || TicketDetails.TicketCount == 0 || TicketDetails.TravelDate == null)
                return BadRequest("Give valid ticket booking details.");

            if (UserId == null || UserId == Guid.Empty)
                return BadRequest("Enter a valid user id.");

            var ticket = _mapper.Map<Ticket>(TicketDetails);
            var response = await _userServices.BookTicket(UserId,ticket);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            var ticketResource = _mapper.Map<Ticket, UserTicketToReturnDTO>(response.Ticket);
            return CreatedAtRoute("GetTicket", new { UserId = UserId, TicketId = ticketResource.Id }, ticketResource);
        }

        [HttpDelete("{UserId}/tickets/{Ticketid}", Name = "DeleteTicket")]
        public async Task<IActionResult> DeleteTicket(Guid UserId, Guid TicketId)
        {
            if (UserId == Guid.Empty)
                return BadRequest("Enter a valid user id.");

            if (TicketId == Guid.Empty)
                return BadRequest("Enter a valid ticket id.");

            var response = await _userServices.CancelBooking(UserId,TicketId);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var ticketToReturn = _mapper.Map<UserTicketToReturnDTO>(response.Ticket);
            return Ok(ticketToReturn);
        }

        [HttpDelete("{UserId}", Name = "DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid UserId)
        {
            if (UserId == Guid.Empty)
                return BadRequest("Enter valid user id.");

            var response = await _userServices.DeleteUser(UserId);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var userToReturn = _mapper.Map<UserDTO>(response.User);
            return Ok(userToReturn);
        }

        [HttpPut("{UserId}",Name="UpdateUser")]
        public async Task<IActionResult> PartialUpdateUserData(Guid UserId, [FromBody] UserUpdationDTO userDetails)
        {
            if (UserId == Guid.Empty)
                return BadRequest("Enter valid user id.");

            if (userDetails == null)
                return BadRequest("Enter valid user updation details.");

            var user = await _userServices.GetUserById(UserId);

            if (user == null)
                return BadRequest("User does not exists.");

            var updatedUser = await _userServices.UpdateUser(user, userDetails);

            if (!updatedUser.Success)
                return BadRequest(updatedUser.Message);

            var userToReturn = _mapper.Map<UserDTO>(user);

            return Ok(userToReturn);
        }
    }
}
