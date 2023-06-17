using OnlineBarterSystemWS.Generic.Models.Response;

namespace OnlineBarterSystemWS.Models.Response
{
    public class UserResponse : AEntityResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public CityResponse City { get; set; }
        public string PhoneNumber { get; set; }
    }
}
