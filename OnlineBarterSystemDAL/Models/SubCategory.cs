using System;
using System.Collections.Generic;

namespace OnlineBarterSystemDAL.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            BarterGiveTypes = new HashSet<Barter>();
            BarterReceiveTypes = new HashSet<Barter>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public long CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Barter> BarterGiveTypes { get; set; }
        public virtual ICollection<Barter> BarterReceiveTypes { get; set; }
    }
}
