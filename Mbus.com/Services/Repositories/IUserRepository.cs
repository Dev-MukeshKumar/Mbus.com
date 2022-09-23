using Mbus.com.Entities;
using System;
using System.Threading.Tasks;

namespace Mbus.com.Services.Repositories
{
    public interface IUserRepository
    {
        Task CreateUser(User user);
        void UpdateUser(User user);
        Task DeleteUser(User user);
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByEmail(string email);
        bool UserExist(string email);
    }
}
