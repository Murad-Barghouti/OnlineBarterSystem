using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OnlineBarterSystemDAL.Models
{
    public partial class AspNetUserLogin : IdentityUserLogin<long>
    {
        /*
        public string LoginProvider { get; set; } = null!;
        public string ProviderKey { get; set; } = null!;
        public string? ProviderDisplayName { get; set; }
        public long UserId { get; set; }

        public virtual AspNetUser User { get; set; } = null!;
        */
    }
}
