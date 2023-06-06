using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBarterSystemDAL.ModelsBackup
{
    public class BarterState
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [InverseProperty("BarterState")]
        public virtual ICollection<Barter> Barters { get; set; }
    }
}
