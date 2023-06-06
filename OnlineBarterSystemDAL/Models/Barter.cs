using System;
using System.Collections.Generic;

namespace OnlineBarterSystemDAL.Models
{
    public partial class Barter
    {
        public long Id { get; set; }
        public string? Description { get; set; }
        public double? GiveValue { get; set; }
        public double? ReceiveValue { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public long BarterStateId { get; set; }
        public long InitiatorId { get; set; }
        public long? JoinerId { get; set; }
        public long GiveTypeId { get; set; }
        public long ReceiveTypeId { get; set; }

        public virtual BarterState BarterState { get; set; } = null!;
        public virtual SubCategory GiveType { get; set; } = null!;
        public virtual AspNetUser Initiator { get; set; } = null!;
        public virtual AspNetUser? Joiner { get; set; }
        public virtual SubCategory ReceiveType { get; set; } = null!;
    }
}
