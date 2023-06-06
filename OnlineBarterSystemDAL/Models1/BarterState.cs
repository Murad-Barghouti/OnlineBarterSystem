using System;
using System.Collections.Generic;

namespace OnlineBarterSystemDAL.Models1
{
    public partial class BarterState
    {
        public BarterState()
        {
            Barters = new HashSet<Barter>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Barter> Barters { get; set; }
    }
}
