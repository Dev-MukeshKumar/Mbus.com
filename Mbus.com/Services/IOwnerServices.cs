using Mbus.com.Entities;
using Mbus.com.Helpers.Parameters;
using Mbus.com.Helpers.Responses;
using Mbus.com.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mbus.com.Services
{
    public interface IOwnerServices
    {
        Task<OwnerResponse> CreateOwner(Owner owner);
        Task<OwnerResponse> DeleteOwner(Guid Id);
        Task<Owner> GetOwner(Guid Id);
        Task<BusResponse> AddBus(Bus bus);
        Task<Bus> GetBus(Guid Id);
        Task<BusResponse> DeleteBus(Guid OwnerId, Guid BusId);
        IEnumerable<Bus> GetAllBus(Guid OwnerId, BusResourceParamter resourceParamter);
        IEnumerable<Ticket> GetAllTicket(TicketResourceParameter resourceParamter);
        Task<OwnerResponse> CheckTicketAvailabilty(TicketResourceParameter resourceParameter);
        Task<OwnerResponse> UpdateOwner(Owner owner, OwnerUpdationDTO ownerDetails);
        Task<BusResponse> UpdateBus(Bus bus, BusUpdationDTO busDetails);
        Task<Owner> GetOwnerByEmail(string email, string password);
    }
}