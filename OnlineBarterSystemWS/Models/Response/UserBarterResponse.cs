using OnlineBarterSystemWS.Generic.Models.Response;

namespace OnlineBarterSystemWS.Models.Response
{
    public class UserBarterResponse : AEntityResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public CityResponse City { get; set; }
        public string PhoneNumber { get; set; }
        public virtual List<BarterResponse> BarterInitiators { get; set; }
        public virtual List<BarterResponse> BarterJoiners { get; set; }
    }
}
