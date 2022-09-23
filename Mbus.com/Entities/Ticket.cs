using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mbus.com.Entities
{
    public class Ticket
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int TicketCount { get; set; }
        
        [Required]
        public int TotalPrice { get; set; }

        [Required]
        public DateTime BookedDate { get; set; }
        
        [Required]
        public DateTime TravelDate { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [ForeignKey("BusId")]
        public Bus Bus { get; set; }
        public Guid BusId { get; set; }
        
        [Required]
        public string BusName { get; set; }
    }
}
