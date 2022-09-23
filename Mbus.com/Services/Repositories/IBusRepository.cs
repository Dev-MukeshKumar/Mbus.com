using Mbus.com.Entities;
using Mbus.com.Helpers.Parameters;
using Mbus.com.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mbus.com.Services.Repositories
{
    public interface IBusRepository
    {
        Task CreateBus(Bus bus);
        IEnumerable<Bus> GetAllBus(Guid OwnerId, BusResourceParamter resourceParameter);
        IEnumerable<Bus> GetAllBus(BusResourceParamter resourceParameter);
        Task<Bus> GetBusById(Guid BusId);
        Task DeleteBus(Bus bus);
        bool BusExists(string Name);
    }
}