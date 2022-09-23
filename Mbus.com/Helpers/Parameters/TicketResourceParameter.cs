using Mbus.com.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Mbus.com.Helpers.Parameters
{
    public class TicketResourceParameter
    {
        public DateTime BookedDate { get; set; }
        public DateTime TravelDate { get; set; }
        public Guid BusId { get; set; }
    }
}
