using Mbus.com.Data;
using Mbus.com.Entities;
using Mbus.com.Helpers.Parameters;
using Mbus.com.Helpers.Responses;
using Mbus.com.Helpers.Security.Hashing;
using Mbus.com.Models;
using Mbus.com.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mbus.com.Services
{
    public class OwnerServices : IOwnerServices
    {

        private readonly IOwnerRepository _ownerRepository;
        private readonly IBusRepository _busRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public OwnerServices(
            IOwnerRepository ownerRepository, 
            IUnitOfWork unitOfWork, 
            IPasswordHasher passwordHasher, 
            IBusRepository busRepository,
            ITicketRepository ticketRepository
            )
        {
            _ownerRepository = ownerRepository;
            _busRepository = busRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _ticketRepository = ticketRepository;
        }

        public async Task<OwnerResponse> CreateOwner(Owner owner)
        {

            var exisitngOwner = _ownerRepository.OwnerExists(owner.Email);

            if (exisitngOwner)
            {
                return new OwnerResponse(false, "Email already in use.", null);
            }

            owner.Password = _passwordHasher.HashPassword(owner.Password);

            await _ownerRepository.CreateOwner(owner);
            await _unitOfWork.SaveAsync();

            return new OwnerResponse(true, null, owner);
        }

        public async Task<Owner> GetOwner(Guid Id)
        {
            return await _ownerRepository.GetOwnerById(Id);
        }

        public async Task<OwnerResponse> DeleteOwner(Guid Id)
        {
            var exisitngOwner = await _ownerRepository.GetOwnerById(Id);
            if (exisitngOwner == null)
            {
                return new OwnerResponse(false, "Owner does not exists.", null);
            }

            await _ownerRepository.DeleteOwner(exisitngOwner);
            await _unitOfWork.SaveAsync();

            return new OwnerResponse(true, "Owner account removed.", exisitngOwner);
        }

        public async Task<OwnerResponse> UpdateOwner(Owner owner, OwnerUpdationDTO ownerDetails)
        {

            if (!string.IsNullOrWhiteSpace(ownerDetails.Name))
                owner.Name = ownerDetails.Name;

            if (!string.IsNullOrWhiteSpace(ownerDetails.Email))
                owner.Email = ownerDetails.Email;

            _ownerRepository.UpdateOwner(owner);
            await _unitOfWork.SaveAsync();

            return new OwnerResponse(true, "Updated owner data", owner);
        }

        public async Task<BusResponse> DeleteBus(Guid OwnerId, Guid BusId)
        {
            var exisitngBus = await _busRepository.GetBusById(BusId);

            if (exisitngBus == null || exisitngBus.OwnerId != OwnerId)
                return new BusResponse(false, "Bus does not exists.", null);

            await _busRepository.DeleteBus(exisitngBus);
            await _unitOfWork.SaveAsync();

            return new BusResponse(true, "Bus data removed.", exisitngBus);
        }

        public async Task<BusResponse> AddBus(Bus bus)
        {
            if(bus == null)
                throw new ArgumentNullException(nameof(bus));

            var exisitingBus = _busRepository.BusExists(bus.Name);

            if (exisitingBus)
                return new BusResponse(false, "Bus already exixts.", null);

            await _busRepository.CreateBus(bus);
            await _unitOfWork.SaveAsync();

            return new BusResponse(true, "Bus created successfully.", bus);
        }

        public async Task<Bus> GetBus(Guid BusId)
        {
            if (BusId == null || BusId == Guid.Empty)
                throw new ArgumentNullException(nameof(BusId));

            return await _busRepository.GetBusById(BusId);
        }

        public async Task<BusResponse> UpdateBus(Bus bus, BusUpdationDTO busDetails)
        {

            if (!string.IsNullOrWhiteSpace(busDetails.Name))
                bus.Name = busDetails.Name;

            if (!string.IsNullOrWhiteSpace(busDetails.From))
                bus.From = busDetails.From;

            if (!string.IsNullOrWhiteSpace(busDetails.To))
                bus.To = busDetails.To;

            if (!string.IsNullOrWhiteSpace(busDetails.DepartureTime))
                bus.DepartureTime = DateTime.Parse(busDetails.DepartureTime);

            if (busDetails.TotalSeats !=0 && busDetails.TotalSeats > 0)
                bus.TotalSeats = busDetails.TotalSeats;

            if (busDetails.TicketPrice != 0 && busDetails.TicketPrice > 0)
                bus.TicketPrice = busDetails.TicketPrice;

            await _unitOfWork.SaveAsync();

            return new BusResponse(true, "Updated bus data", bus);
        }

        public IEnumerable<Bus> GetAllBus(Guid OwnerId, BusResourceParamter resourceParamter)
        {
            if (OwnerId == null || OwnerId == Guid.Empty)
                throw new ArgumentNullException(nameof(OwnerId));

            if (resourceParamter == null)
                throw new ArgumentNullException(nameof(resourceParamter));

            return _busRepository.GetAllBus(OwnerId,resourceParamter);
        }

        public IEnumerable<Ticket> GetAllTicket(TicketResourceParameter resourceParameter)
        {
            if (resourceParameter.BusId == null || resourceParameter.BusId == Guid.Empty)
                throw new ArgumentNullException(nameof(resourceParameter.BusId));

            if (resourceParameter == null)
                throw new ArgumentNullException(nameof(resourceParameter));

            return _ticketRepository.GetAllTickets(resourceParameter);
        }

        public async Task<OwnerResponse> CheckTicketAvailabilty(TicketResourceParameter resourceParameter)
        {
            if (resourceParameter.BusId == null || resourceParameter.BusId == Guid.Empty)
                throw new ArgumentNullException(nameof(resourceParameter.BusId));

            if (resourceParameter == null)
                throw new ArgumentNullException(nameof(resourceParameter));
            
            var bus = await _busRepository.GetBusById(resourceParameter.BusId);

            resourceParameter.TravelDate = resourceParameter.TravelDate.AddHours(bus.DepartureTime.Hour).AddMinutes(bus.DepartureTime.Minute);
            var tickets = _ticketRepository.GetAllTickets(resourceParameter);
            int ticketsBooked = 0;
            foreach(var ticket in tickets)
            {
                ticketsBooked +=ticket.TicketCount;
            }
            if(resourceParameter.TravelDate < DateTime.Now)
                return new OwnerResponse(true, $"{bus.TotalSeats - ticketsBooked} tickets are booked on {resourceParameter.TravelDate.ToString("d-M-yyyy")}", null);
            return new OwnerResponse(true, $"{bus.TotalSeats - ticketsBooked} tickets are available on {resourceParameter.TravelDate.ToString("d-M-yyyy")}", null);
            
        }

        public async Task<Owner> GetOwnerByEmail(string email, string password)
        {
            var owner = await _ownerRepository.GetOwnerByEmail(email);
            if (owner != null && _passwordHasher.PasswordMatches(password, owner.Password))
                return owner;

            return null;
        }
    }
}
