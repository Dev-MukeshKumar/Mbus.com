using Mbus.com.Entities;
using System;
using System.Threading.Tasks;

namespace Mbus.com.Services.Repositories
{
    public interface IOwnerRepository
    {
        Task<Owner> GetOwnerById(Guid Id);

        Task CreateOwner(Owner owner);

        Task<Owner> GetOwnerByEmail(string email);

        Task DeleteOwner(Owner owner);

        void UpdateOwner(Owner owner);

        bool OwnerExists(string email);
    }
}
