using OnlineBarterSystemWS.Generic.Models.Request;

namespace OnlineBarterSystemWS.Models.Request
{
    public class UpdateBarterRequest : AEntityRequest
    {
        public string? Description { get; set; }
        public double? GiveValue { get; set; }
        public double? ReceiveValue { get; set; }
    }
}
