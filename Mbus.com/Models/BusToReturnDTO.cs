using System.ComponentModel.DataAnnotations;
using System;

namespace Mbus.com.Models
{
    public class BusToReturnDTO
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
        public string DepartureTime { get; set; }
    }
}
