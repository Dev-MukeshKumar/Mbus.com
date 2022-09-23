using Mbus.com.Data;
using Mbus.com.Entities;
using Mbus.com.Helpers.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mbus.com.Services.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException(nameof(ticket));

            await _context.Tickets.AddAsync(ticket);
        }

        public async Task<Ticket> GetTicketById(Guid TicketId)
        {
            if (TicketId == Guid.Empty)
                throw new ArgumentNullException(nameof(TicketId));

            var ticket = await _context.Tickets.SingleOrDefaultAsync(ticket => ticket.Id == TicketId);

            return ticket;
        }

        public IEnumerable<Ticket> GetAllTickets(Guid UserId, TicketResourceParameter resourceParameter)
        {

            var tickets = _context.Tickets as IQueryable<Ticket>;

            tickets = tickets.Where(ticket => ticket.UserId == UserId);

            if (resourceParameter.BusId != Guid.Empty)
            {
                var BusId = resourceParameter.BusId;
                tickets = tickets.Where(ticket => ticket.BusId == BusId);
            }

            if (resourceParameter.BookedDate != DateTime.MinValue)
            {
                var BookedDate = resourceParameter.BookedDate;
                tickets = tickets.ToList().Where(ticket => ticket.BookedDate.ToString("d-M-yyyy") == BookedDate.ToString("d-M-yyyy")).AsQueryable();
            }

            if (resourceParameter.TravelDate != DateTime.MinValue)
            {
                var TravelDate = resourceParameter.TravelDate;
                tickets = tickets.ToList().Where(ticket => ticket.TravelDate.ToString("d-M-yyyy") == TravelDate.ToString("d-M-yyyy")).AsQueryable();
            }

            return tickets;
        }

        public IEnumerable<Ticket> GetAllTickets(TicketResourceParameter resourceParameter)
        {

            var tickets = _context.Tickets as IQueryable<Ticket>;

            if (resourceParameter.BusId != Guid.Empty)
            {
                var BusId = resourceParameter.BusId;
                tickets = tickets.Where(ticket => ticket.BusId == BusId);
            }

            if (resourceParameter.BookedDate != DateTime.MinValue)
            {
                var BookedDate = resourceParameter.BookedDate;
                tickets = tickets.ToList().Where(ticket => ticket.BookedDate.ToString("d-M-yyyy") == BookedDate.ToString("d-M-yyyy")).AsQueryable();
            }

            if (resourceParameter.TravelDate != DateTime.MinValue)
            {
                var TravelDate = resourceParameter.TravelDate;
                tickets = tickets.ToList().Where(ticket => ticket.TravelDate.ToString("d-M-yyyy") == TravelDate.ToString("d-M-yyyy")).AsQueryable();
            }

            return tickets;
        }

        public Task DeleteTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException(nameof(ticket));

            _context.Tickets.Remove(ticket);
            return Task.CompletedTask;
        }

        public bool TicketExists(Guid TicketId)
        {
            if (TicketId == Guid.Empty || TicketId == null)
                throw new ArgumentNullException(nameof(TicketId));

            return _context.Tickets.Any(ticket => ticket.Id == TicketId);
        }

        public int BookedTicketCount(Guid BusId, DateTime TravelDate)
        {
            if (BusId == Guid.Empty || BusId == null)
                throw new ArgumentNullException(nameof(BusId));

            if (TravelDate == DateTime.MinValue)
                throw new ArgumentException(nameof(TravelDate));

            var tickets = _context.Tickets as IQueryable<Ticket>;

            tickets = tickets.Where(ticket => ticket.BusId == BusId);
            tickets = tickets.ToList().Where(ticket => ticket.TravelDate.ToString("dd-MM-yyyy") == TravelDate.ToString("dd-MM-yyyy")).AsQueryable();

            int bookedCount = 0;
            foreach(var ticket in tickets.ToList())
            {
                bookedCount += ticket.TicketCount;
            }

            return bookedCount;
        }
    }
}
