using AutoMapper;
using Mbus.com.Entities;
using Mbus.com.Helpers.Parameters;
using Mbus.com.Models;
using Mbus.com.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mbus.com.Controllers
{
    [ApiController]
    [Authorize(Roles ="owner")]
    [Route("[controller]")]
    public class OwnersController: ControllerBase
    {
        private readonly IOwnerServices _ownerService;
        private readonly IMapper _mapper;

        public OwnersController(IOwnerServices ownerService, IMapper mapper)
        {
            _ownerService = ownerService;
            _mapper = mapper;
        }

        [HttpGet("{OwnerId}",Name ="GetOwner")]
        public async Task<IActionResult> GetOwner(Guid OwnerId)
        {
            if(OwnerId == Guid.Empty)
                return BadRequest("Enter a valid owner id.");
            
            var owner = await _ownerService.GetOwner(OwnerId);
            var ownerToReturn = _mapper.Map<OwnerDTO>(owner);

            return Ok(ownerToReturn);

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterOwner([FromBody] OwnerCreationDTO OwnerDetails)
        {
            if (OwnerDetails == null || string.IsNullOrWhiteSpace(OwnerDetails.Name) || string.IsNullOrWhiteSpace(OwnerDetails.Email) || string.IsNullOrWhiteSpace(OwnerDetails.Password))
            {
                return BadRequest("Provide owner details to register.");
            }

            var owner = _mapper.Map<Entities.Owner>(OwnerDetails);
            var response = await _ownerService.CreateOwner(owner);

            if (!response.Success) { 
                return BadRequest(response.Message);
            }

            var ownerResource = _mapper.Map<OwnerDTO>(response.Owner);

            return CreatedAtRoute("GetOwner", new { OwnerId = ownerResource.Id }, ownerResource);
        }

        [HttpPost("{OwnerId}/buses")]
        public async Task<IActionResult> AddBus(Guid OwnerId,[FromBody] BusCreationDTO BusDetails)
        {
            if (BusDetails == null || 
                string.IsNullOrWhiteSpace(BusDetails.Name) || 
                string.IsNullOrWhiteSpace(BusDetails.DepartureTime) ||
                string.IsNullOrWhiteSpace(BusDetails.From) ||
                string.IsNullOrWhiteSpace(BusDetails.To) ||
                BusDetails.TotalSeats == 0 ||
                BusDetails.TicketPrice == 0
                )
                return BadRequest("Provide proper bus details.");
            
            var owner = await _ownerService.GetOwner(OwnerId);
            if (owner == null)
                return BadRequest("Owner does not exists.");
            
            BusDetails.OwnerId = OwnerId;

            var bus = _mapper.Map<Bus>(BusDetails);
            var response = await _ownerService.AddBus(bus);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            var busResource = _mapper.Map<Bus, BusToReturnDTO>(response.Bus);
            return CreatedAtRoute("GetBus",new {OwnerId=OwnerId, BusId=busResource.Id},busResource);
        }

        [HttpGet("{OwnerId}/buses/{Busid}",Name ="GetBus")]
        public async Task<IActionResult> GetBusById(Guid BusId)
        {
            if (BusId == Guid.Empty)
                return BadRequest("Provide valid bus id.");

            var bus = await _ownerService.GetBus(BusId);
            var busToReturn = _mapper.Map<Bus,BusToReturnDTO>(bus);
            return Ok(busToReturn);
        }

        [HttpGet("{OwnerId}/buses",Name ="GetAllBuses")]
        public async Task<IActionResult> GetAllBus([FromQuery] BusResourceParamter resourceParamter, Guid OwnerId)
        {
            if(OwnerId == Guid.Empty || OwnerId == null)
                return BadRequest("Provide proper owner id.");

            var owner = await _ownerService.GetOwner(OwnerId);
            if (owner == null)
                return BadRequest("Invalid owner.");

            var buses = _ownerService.GetAllBus(OwnerId, resourceParamter);
            var busesToReturn = _mapper.Map<IEnumerable<BusToReturnDTO>>(buses);

            return Ok(busesToReturn);
        }

        [HttpGet("{OwnerId}/tickets", Name = "GetAllTicket")]
        public async Task<IActionResult> GetAllTickets([FromQuery] TicketResourceParameter resourceParameter, Guid OwnerId)
        {
            if (resourceParameter == null || 
                resourceParameter.BusId == null || 
                resourceParameter.TravelDate == null  ||
                resourceParameter.BusId == Guid.Empty || 
                resourceParameter.TravelDate == DateTime.MinValue 
                )
                return BadRequest("Provide atleast bus id and travel date.");

            var bus = await _ownerService.GetBus(resourceParameter.BusId);
            if (bus == null || bus.OwnerId != OwnerId)
                return BadRequest("Bus does not exists.");

            var tickets = _ownerService.GetAllTicket(resourceParameter);
            var ticketsToReturn = _mapper.Map<IEnumerable<OwnerTicketToReturnDTO>>(tickets);

            return Ok(ticketsToReturn);
        }

        [HttpGet("{OwnerId}/tickets_availability")]
        public async Task<IActionResult> CheckFreeTickets([FromQuery] TicketResourceParameter resourceParameter, Guid OwnerId)
        {
            if (resourceParameter == null ||
                resourceParameter.BusId == null ||
                resourceParameter.TravelDate == null ||
                resourceParameter.BusId == Guid.Empty ||
                resourceParameter.TravelDate == DateTime.MinValue
                )
                return BadRequest("Provide atleast bus id and travel date.");

            var bus = await _ownerService.GetBus(resourceParameter.BusId);
            if (bus == null || bus.OwnerId != OwnerId)
                return BadRequest("Bus does not exists.");

            var response = await _ownerService.CheckTicketAvailabilty(resourceParameter);
            return Ok(response.Message);
        }


        [HttpDelete("{OwnerId}", Name = "DeleteOwner")]
        public async Task<IActionResult> DeleteOwner(Guid OwnerId)
        {
            if (OwnerId == Guid.Empty)
                return BadRequest("Provide proper owner id.");

            var response = await _ownerService.DeleteOwner(OwnerId);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var ownerToReturn = _mapper.Map<OwnerDTO>(response.Owner);

            return Ok(ownerToReturn);

        }

        [HttpDelete("{OwnerId}/buses/{Busid}", Name = "DeleteBus")]
        public async Task<IActionResult> DeleteBus(Guid OwnerId,Guid BusId)
        {
            if (BusId == Guid.Empty || BusId == null)
                return BadRequest("Provide proper bus id.");

            var response = await _ownerService.DeleteBus(OwnerId,BusId);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var busToReturn = _mapper.Map<BusToReturnDTO>(response.Bus);
            return Ok(busToReturn);
        }

        [HttpPut("{OwnerId}")]
        public async Task<IActionResult> PartialUpdateOwnerData(Guid OwnerId, [FromBody] OwnerUpdationDTO ownerDetails)
        {
            if (OwnerId == Guid.Empty || OwnerId == null)
                return BadRequest("Enter valid owner id.");

            if (ownerDetails == null)
                return BadRequest("Enter valid owner updation details.");

            var owner = await _ownerService.GetOwner(OwnerId);
            if (owner == null)
                return BadRequest("Owner does not exists.");

            var updatedOwner = await _ownerService.UpdateOwner(owner, ownerDetails);

            var ownerToReturn = _mapper.Map<OwnerDTO>(updatedOwner.Owner);

            return Ok(ownerToReturn);
        }

        [HttpPut("{OwnerId}/buses/{BusId}")]
        public async Task<IActionResult> PartialUpdateBusData(Guid OwnerId,Guid BusId, [FromBody] BusUpdationDTO busDetails)
        {
            if (BusId == Guid.Empty || BusId == null)
                return BadRequest("Provide proper bus id.");

            if (busDetails == null)
                return BadRequest("Provide valid updation details.");

            var bus = await _ownerService.GetBus(BusId);
            if (bus == null || bus.OwnerId != OwnerId)
                return BadRequest("Bus does not exists.");

            var updatedBus = await _ownerService.UpdateBus(bus,busDetails);
            var busToReturn = _mapper.Map<BusToReturnDTO>(updatedBus.Bus);

            return Ok(busToReturn);
        }

    }
}
