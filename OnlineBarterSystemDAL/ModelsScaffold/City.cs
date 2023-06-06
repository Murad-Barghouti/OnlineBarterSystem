﻿using System;
using System.Collections.Generic;

namespace OnlineBarterSystemDAL.ModelsScaffold
{
    public partial class City
    {
        public City()
        {
            Users = new HashSet<User>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
