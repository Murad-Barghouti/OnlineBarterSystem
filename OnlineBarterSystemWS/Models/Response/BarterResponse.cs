using OnlineBarterSystemWS.Generic.Models.Response;

namespace OnlineBarterSystemWS.Models.Response
{
    public class BarterResponse : AEntityResponse
    {
        public string Description { get; set; }
        public double? GiveValue { get; set; }
        public double? ReceiveValue { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public long BarterStateId { get; set; }
        public long InitiatorId { get; set; }
        public long? JoinerId { get; set; }
        public long GiveTypeId { get; set; }
        public long ReceiveTypeId { get; set; }
        public virtual BarterStateResponse BarterState { get; set; }
        public virtual SubCategoryResponse GiveType { get; set; }
        public UserResponse Initiator { get; set; }
        public UserResponse Joiner { get; set; }
        public virtual SubCategoryResponse ReceiveType { get; set; }
    }
}
