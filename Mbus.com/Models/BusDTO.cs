using System;
using System.ComponentModel.DataAnnotations;

namespace Mbus.com.Models
{
    public class BusDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(maximumLength: 30)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 30)]
        public string From { get; set; }

        [Required]
        [StringLength(maximumLength: 30)]
        public string To { get; set; }

        [Required]
        public int TotalSeats { get; set; }

        [Required]
        public int TicketPrice { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public Guid OwnerId { get; set; }
    }
}
