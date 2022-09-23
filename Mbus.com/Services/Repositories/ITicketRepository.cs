using Mbus.com.Entities;
using Mbus.com.Helpers.Parameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mbus.com.Services.Repositories
{
    public interface ITicketRepository
    {
        Task AddTicket(Ticket ticket);
        IEnumerable<Ticket> GetAllTickets(Guid UserId, TicketResourceParameter resourceParameter);
        IEnumerable<Ticket> GetAllTickets(TicketResourceParameter resourceParameter);
        Task<Ticket> GetTicketById(Guid TicketId);
        Task DeleteTicket(Ticket ticket);
        bool TicketExists(Guid TicketId);
        int BookedTicketCount(Guid BusId, DateTime TravelDate);
    }
}