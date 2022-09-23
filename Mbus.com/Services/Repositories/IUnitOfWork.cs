using System.Threading.Tasks;

namespace Mbus.com.Services.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
