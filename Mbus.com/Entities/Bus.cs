using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mbus.com.Entities
{
    public class Bus
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(maximumLength:30)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength:30)]
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

        [ForeignKey("OwnerId")]
        public Guid OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
