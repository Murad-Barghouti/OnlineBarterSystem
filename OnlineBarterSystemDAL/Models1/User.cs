using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace OnlineBarterSystemDAL.Models1
{
    public partial class User : IdentityUser
    {
        public User() : base()
        {
            BarterInitiators = new HashSet<Barter>();
            BarterJoiners = new HashSet<Barter>();
        }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsAmdin { get; set; }
        public long CityId { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual ICollection<Barter> BarterInitiators { get; set; }
        public virtual ICollection<Barter> BarterJoiners { get; set; }
    }
}
