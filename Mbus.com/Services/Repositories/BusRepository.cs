using Mbus.com.Data;
using Mbus.com.Entities;
using Mbus.com.Helpers.Parameters;
using Mbus.com.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mbus.com.Services.Repositories
{
    public class BusRepository : IBusRepository
    {
        private readonly AppDbContext _context;

        public BusRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateBus(Bus bus)
        {
            if (bus == null)
                throw new ArgumentNullException(nameof(bus));

            await _context.Buses.AddAsync(bus);
        }

        public async Task<Bus> GetBusById(Guid BusId)
        {
            if (BusId == Guid.Empty)
                throw new ArgumentNullException(nameof(BusId));

            var bus = await _context.Buses.SingleOrDefaultAsync(bus => bus.Id == BusId);

            return bus;
        }

        public IEnumerable<Bus> GetAllBus(Guid OwnerId, BusResourceParamter resourceParameter)
        {

            var buses = _context.Buses as IQueryable<Bus>;

            buses = buses.Where(bus => bus.OwnerId == OwnerId);

            if (!string.IsNullOrWhiteSpace(resourceParameter.From))
            {
                var From = resourceParameter.From.Trim().ToLower();
                buses = buses.Where(bus => bus.From == From);
            }

            if (!string.IsNullOrWhiteSpace(resourceParameter.To))
            {
                var To = resourceParameter.To.Trim().ToLower();
                buses = buses.Where(bus => bus.To == To);
            }

            if (resourceParameter.DepartureTime != DateTime.MinValue)
            {
                var DepartureTime = resourceParameter.DepartureTime;
                buses = buses.ToList().Where(bus => bus.DepartureTime.ToString("H:mm") == DepartureTime.ToString("H:mm")).AsQueryable();
                //buses = buses.Where(bus => bus.DepartureTime.ToString("H:mm") == DepartureTime.ToString("H:mm"));
            }

            return buses;
        }

        public IEnumerable<Bus> GetAllBus(BusResourceParamter resourceParameter)
        {

            var buses = _context.Buses as IQueryable<Bus>;

            if (!string.IsNullOrWhiteSpace(resourceParameter.From))
            {
                var From = resourceParameter.From.Trim().ToLower();
                buses = buses.Where(bus => bus.From == From);
            }

            if (!string.IsNullOrWhiteSpace(resourceParameter.To))
            {
                var To = resourceParameter.To.Trim().ToLower();
                buses = buses.Where(bus => bus.To == To);
            }

            if (resourceParameter.DepartureTime != DateTime.MinValue)
            {
                var DepartureTime = resourceParameter.DepartureTime;
                buses = buses.ToList().Where(bus => bus.DepartureTime.ToString("H:mm") == DepartureTime.ToString("H:mm")).AsQueryable();
                //buses = buses.Where(bus => bus.DepartureTime.ToString("H:mm") == DepartureTime.ToString("H:mm"));
            }

            return buses;
        }

        public Task DeleteBus(Bus bus)
        {
            if (bus == null)
                throw new ArgumentNullException(nameof(bus));

            _context.Buses.Remove(bus);
            return Task.CompletedTask;
        }


        public bool BusExists(string Name)
        {
            if(string.IsNullOrEmpty(Name) && string.IsNullOrWhiteSpace(Name))
                throw new ArgumentNullException(nameof(Name));

            return _context.Buses.Any(bus => bus.Name == Name);
        }
    }
}
