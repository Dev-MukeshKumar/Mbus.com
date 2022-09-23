using Mbus.com.Entities;

namespace Mbus.com.Helpers.Responses
{
    public class TicketResponse : BaseResponse
    {
        public Ticket Ticket{ get; private set; }
        public TicketResponse(bool success,string message,Ticket ticket): base(success,message)
        {
            Ticket = ticket;
        }
    }
}
