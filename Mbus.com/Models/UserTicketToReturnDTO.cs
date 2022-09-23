using Mbus.com.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Mbus.com.Models
{
    public class UserTicketToReturnDTO
    {
        public Guid Id { get; set; }
        public int TicketCount { get; set; }
        public int TotalPrice { get; set; }
        public string BookedDate { get; set; }
        public string TravelDate { get; set; }
        public Guid BusId { get; set; }
        public string BusName { get; set; }
    }
}
