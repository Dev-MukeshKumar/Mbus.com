using System.ComponentModel.DataAnnotations;

namespace Mbus.com.Models
{
    public class BusUpdationDTO
    {
        public string Name { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public int TotalSeats { get; set; }

        public int TicketPrice { get; set; }

        public string DepartureTime { get; set; }
    }
}
