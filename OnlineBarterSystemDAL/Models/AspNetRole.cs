using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace OnlineBarterSystemDAL.Models
{
    public partial class AspNetRole : IdentityRole<long>
    {
        public AspNetRole()
        {
            /*
            AspNetRoleClaims = new HashSet<AspNetRoleClaim>();
            Users = new HashSet<AspNetUser>();
            */
        }
        /*
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }

        public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; }

        public virtual ICollection<AspNetUser> Users { get; set; }
        */
    }
}
