using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBarterSystemDAL.ModelsBackup
{
    public class SubCategory
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("SubCategory")]
        public virtual Category ParentCategory { get; set; }
        [InverseProperty("SubCategory")]
        public virtual ICollection<Barter> Barters { get; set; }
    }
}
