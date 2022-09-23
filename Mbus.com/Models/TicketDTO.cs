using Mbus.com.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Mbus.com.Models
{
    public class TicketDTO
    {
        public Guid Id { get; set; }
        public int TicketCount { get; set; }
        public int TotalPrice { get; set; }
        public DateTime BookedDate { get; set; }
        public DateTime TravelDate { get; set; }
        public Guid BusId { get; set; }
    }
}
