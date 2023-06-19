using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OnlineBarterSystemDAL.Models
{
    public partial class AspNetRoleClaim : IdentityRoleClaim<long>
    {
        /*
        public int Id { get; set; }
        public long RoleId { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }

        public virtual AspNetRole Role { get; set; } = null!;
        */
    }
}
