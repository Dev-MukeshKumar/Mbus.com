using Mbus.com.Entities;

namespace Mbus.com.Helpers.Responses
{
    public class BusResponse : BaseResponse
    {
        public Bus Bus {get; private set;}

        public BusResponse(bool success,string message,Bus bus): base(success, message)
        {
            Bus = bus;
        }
    }
}
