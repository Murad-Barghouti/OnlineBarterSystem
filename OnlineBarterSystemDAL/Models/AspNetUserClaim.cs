using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OnlineBarterSystemDAL.Models
{
    public partial class AspNetUserClaim : IdentityUserClaim<long>
    {
        /*
        public int Id { get; set; }
        public long UserId { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }

        public virtual AspNetUser User { get; set; } = null!;
        */
    }
}
