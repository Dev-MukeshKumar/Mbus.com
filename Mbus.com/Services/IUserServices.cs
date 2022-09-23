using Mbus.com.Entities;
using Mbus.com.Helpers.Parameters;
using Mbus.com.Helpers.Responses;
using Mbus.com.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mbus.com.Services
{
    public interface IUserServices
    {
        Task<TicketResponse> BookTicket(Guid UserId,Ticket ticket);
        Task<TicketResponse> CancelBooking(Guid UserId, Guid TicketId);
        Task<TicketResponse> confirmBooking(Guid TicketId);
        Task<UserResponse> DeleteUser(Guid id);
        IEnumerable<Bus> GetAllBus(BusResourceParamter resourceParamter);
        IEnumerable<Ticket> GetAllTicket(Guid UserId, TicketResourceParameter resourceParamter);
        Task<User> GetUserById(Guid id);
        Task<Bus> GetBusById(Guid id);
        bool UserExists(Guid UserId);
        Task<UserResponse> UpdateUser(User user, UserUpdationDTO userDetails);
        Task<UserResponse> RegisterUser(User user);
        Task<User> GetUserByEmail(string email, string password);
    }
}