using Mbus.com.Entities;
using Mbus.com.Helpers.Parameters;
using Mbus.com.Helpers.Responses;
using Mbus.com.Helpers.Security.Hashing;
using Mbus.com.Models;
using Mbus.com.Services.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mbus.com.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IBusRepository _busRepository;
        private readonly ITicketRepository _ticketRepository;

        public UserServices(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IBusRepository busRepository,
            ITicketRepository ticketRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _busRepository = busRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task<UserResponse> RegisterUser(User user)
        {
            var existingUser = _userRepository.UserExist(user.Email);
            if (existingUser)
            {
                return new UserResponse(false, "Email already in use.", null);
            }

            user.Password = _passwordHasher.HashPassword(user.Password);

            await _userRepository.CreateUser(user);
            await _unitOfWork.SaveAsync();

            return new UserResponse(true, null, user);
        }

        public bool UserExists(Guid UserId)
        {
            var user = _userRepository.GetUserById(UserId);

            return user == null ? false:true;
        }

        public async Task<Bus> GetBusById(Guid id)
        {
            return await _busRepository.GetBusById(id);
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<UserResponse> DeleteUser(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return new UserResponse(false, "User does not exists.", null);
            }
            await _userRepository.DeleteUser(user);
            await _unitOfWork.SaveAsync();
            return new UserResponse(true, "User account removed.", user);
        }

        public IEnumerable<Bus> GetAllBus(BusResourceParamter resourceParamter)
        {
            if (resourceParamter == null)
                throw new ArgumentNullException(nameof(resourceParamter));

            return _busRepository.GetAllBus(resourceParamter);
        }

        public IEnumerable<Ticket> GetAllTicket(Guid UserId, TicketResourceParameter resourceParamter)
        {
            if (resourceParamter == null)
                throw new ArgumentNullException(nameof(resourceParamter));

            return _ticketRepository.GetAllTickets(UserId,resourceParamter);
        }

        public async Task<TicketResponse> BookTicket(Guid userId,Ticket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException(nameof(ticket));

            var user = await _userRepository.GetUserById(userId);

            if(user == null)
                return new TicketResponse(false, "Not a valid user", null);

            var bus = await _busRepository.GetBusById(ticket.BusId);
            if (bus == null) return new TicketResponse(false, "Bus does not exists.",null);

            ticket.TravelDate = ticket.TravelDate.AddHours(bus.DepartureTime.Hour).AddMinutes(bus.DepartureTime.Minute);
            if(ticket.TravelDate < DateTime.Now) return new TicketResponse(false, "Enter valid TravelDate!", null);

            int bookedCount = _ticketRepository.BookedTicketCount(bus.Id, ticket.TravelDate);

            if (bookedCount == bus.TotalSeats) return new TicketResponse(false, "No ticket available for the given date.", null);
            if(bookedCount+ticket.TicketCount > bus.TotalSeats) return new TicketResponse(false, $"Only {bus.TotalSeats - bookedCount} tickets available.", null);

            ticket.TotalPrice = bus.TicketPrice * ticket.TicketCount;
            ticket.UserId = userId;
            ticket.UserName = user.Name;
            ticket.BusName = bus.Name;
            ticket.BookedDate = DateTime.Now;

            await _ticketRepository.AddTicket(ticket);
            await _unitOfWork.SaveAsync();

            return new TicketResponse(true, null, ticket);
        }

        public async Task<TicketResponse> confirmBooking(Guid TicketId)
        {
            if (TicketId == null)
                throw new ArgumentNullException(nameof(TicketId));

            var exisitngTicket = _ticketRepository.TicketExists(TicketId);

            if (!exisitngTicket)
            {
                return new TicketResponse(false, "Ticket not booked", null);
            }

            var ticket = await _ticketRepository.GetTicketById(TicketId);
            return new TicketResponse(true, null, ticket);
        }

        public async Task<TicketResponse> CancelBooking(Guid UserId,Guid TicketId)
        {
            var ticket = await _ticketRepository.GetTicketById(TicketId);

            if (ticket == null || ticket.UserId != UserId)
            {
                return new TicketResponse(false, "Ticket does not exists!", null);
            }

            await _ticketRepository.DeleteTicket(ticket);
            await _unitOfWork.SaveAsync();

            return new TicketResponse(true, "Ticket cancelled", ticket);
        }

        public async Task<UserResponse> UpdateUser(User user, UserUpdationDTO userDetails)
        {

            if (!string.IsNullOrWhiteSpace(userDetails.Name))
                user.Name = userDetails.Name;

            if (!string.IsNullOrWhiteSpace(userDetails.Email))
                user.Email = userDetails.Email;

            _userRepository.UpdateUser(user);
            await _unitOfWork.SaveAsync();

            return new UserResponse(true, "Updated user data", user);
        }

        public async Task<User> GetUserByEmail(string email,string password)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if(user != null && _passwordHasher.PasswordMatches(password, user.Password))
                return user;

            return null;
        }
    }
}
