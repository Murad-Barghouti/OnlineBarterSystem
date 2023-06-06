using System;
using System.Collections.Generic;

namespace OnlineBarterSystemDAL.ModelsScaffold
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
