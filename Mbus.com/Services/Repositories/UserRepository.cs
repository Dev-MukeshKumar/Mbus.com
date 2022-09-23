using Mbus.com.Data;
using Mbus.com.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mbus.com.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task CreateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.TicketsBooked = new List<Ticket>();
            _context.Users.Add(user);
            return Task.CompletedTask;
        }

        public Task DeleteUser(User user)
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user));
            _context.Users.Remove(user);
            return Task.CompletedTask;
        }

        public async Task<User> GetUserById(Guid id)
        {
            if(id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            return await _context.Users.SingleOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            return await _context.Users.SingleOrDefaultAsync(user => user.Email == email);
        }

        public void UpdateUser(User user)
        {
            
        }

        public bool UserExist(string email)
        {
            if(string.IsNullOrWhiteSpace(email) && string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            return _context.Users.Any(user => user.Email == email);
        }
    }
}
