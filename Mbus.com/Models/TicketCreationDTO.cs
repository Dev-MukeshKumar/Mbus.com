using System;
using System.ComponentModel.DataAnnotations;

namespace Mbus.com.Models
{
    public class TicketCreationDTO
    {
        [Required]
        public int TicketCount { get; set; }
        [Required]
        public string TravelDate { get; set; }
        [Required]
        public Guid BusId { get; set; }
    }
}
