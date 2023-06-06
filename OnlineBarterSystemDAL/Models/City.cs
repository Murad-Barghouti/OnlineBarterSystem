using System;
using System.Collections.Generic;

namespace OnlineBarterSystemDAL.Models
{
    public partial class City
    {
        public City()
        {
            AspNetUsers = new HashSet<AspNetUser>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
