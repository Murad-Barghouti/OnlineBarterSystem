using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace OnlineBarterSystemDAL.Models
{
    public partial class AspNetUser : IdentityUser<long>
    {
        public AspNetUser()
        {
            
            //AspNetUserClaims = new HashSet<AspNetUserClaim>();
            //AspNetUserLogins = new HashSet<AspNetUserLogin>();
            //AspNetUserTokens = new HashSet<AspNetUserToken>();
            BarterInitiators = new HashSet<Barter>();
            BarterJoiners = new HashSet<Barter>();
            //Roles = new HashSet<AspNetRole>();
            
        }

        //public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsAmdin { get; set; }
        public long CityId { get; set; }
        //public string? UserName { get; set; }
        //public string? NormalizedUserName { get; set; }
        //public string? Email { get; set; }
        //public string? NormalizedEmail { get; set; }
        //public bool EmailConfirmed { get; set; }
        //public string? PasswordHash { get; set; }
        //public string? SecurityStamp { get; set; }
        //public string? ConcurrencyStamp { get; set; }
        //public string? PhoneNumber { get; set; }
        //public bool PhoneNumberConfirmed { get; set; }
        //public bool TwoFactorEnabled { get; set; }
        //public DateTimeOffset? LockoutEnd { get; set; }
        //public bool LockoutEnabled { get; set; }
        //public int AccessFailedCount { get; set; }

        public virtual City City { get; set; } = null!;
       
        //public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        //public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        //public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual ICollection<Barter> BarterInitiators { get; set; }
        public virtual ICollection<Barter> BarterJoiners { get; set; }
        //public virtual ICollection<AspNetRole> Roles { get; set; }
        
    }
}
