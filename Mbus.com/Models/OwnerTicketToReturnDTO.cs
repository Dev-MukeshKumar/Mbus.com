using System;

namespace Mbus.com.Models
{
    public class OwnerTicketToReturnDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public int TicketCount { get; set; }
        public int TotalPrice { get; set; }
        public string BookedDate { get; set; }
        public string TravelDate { get; set; }
    }
}
