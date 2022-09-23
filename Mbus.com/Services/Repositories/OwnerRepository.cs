using Mbus.com.Data;
using Mbus.com.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mbus.com.Services.Repositories
{
    public class OwnerRepository: IOwnerRepository
    {
        private readonly AppDbContext _context;

        public OwnerRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task CreateOwner(Owner owner)
        {
            if(owner == null)
                throw new ArgumentNullException(nameof(owner));

             _context.Owners.Add(owner);

            return Task.CompletedTask;
        }

        public Task DeleteOwner(Owner owner)
        {
            if (owner == null)
                throw new ArgumentNullException(nameof(owner));

            _context.Owners.Remove(owner);

            return Task.CompletedTask;

        }

        public async Task<Owner> GetOwnerById(Guid Id)
        {
            if (Id == Guid.Empty)
                throw new ArgumentNullException(nameof(Id));

            return await _context.Owners.SingleOrDefaultAsync(owner => owner.Id == Id);
        }

        public async Task<Owner> GetOwnerByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            return await _context.Owners.SingleOrDefaultAsync(owner => owner.Email == email);
        }

        public void UpdateOwner(Owner owner)
        {

        }

        public bool OwnerExists(string email)
        {
            if (string.IsNullOrWhiteSpace(email) && string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            return _context.Owners.Any(owner => owner.Email == email);
        }
    }
}
