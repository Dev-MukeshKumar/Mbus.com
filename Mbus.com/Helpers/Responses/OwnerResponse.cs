using Mbus.com.Entities;

namespace Mbus.com.Helpers.Responses
{
    public class OwnerResponse: BaseResponse
    {

        public Owner Owner { get; private set; }

        public OwnerResponse(bool success,string message, Owner owner): base(success, message)
        {
            Owner = owner;
        }
    }
}
