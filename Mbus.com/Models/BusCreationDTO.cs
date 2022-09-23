using Mbus.com.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Mbus.com.Models
{
    public class BusCreationDTO
    {

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

        [Required]
        public Guid OwnerId { get; set; }
    }
}
