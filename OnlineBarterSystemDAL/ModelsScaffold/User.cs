using System;
using System.Collections.Generic;

namespace OnlineBarterSystemDAL.ModelsScaffold
{
    public partial class User
    {
        public User()
        {
            BarterInitiators = new HashSet<Barter>();
            BarterJoiners = new HashSet<Barter>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public long CityId { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual ICollection<Barter> BarterInitiators { get; set; }
        public virtual ICollection<Barter> BarterJoiners { get; set; }
    }
}
