using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBarterSystemDAL.ModelsBackup
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        [Required]
        public long CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        [InverseProperty("ApplicationUser")]
        public virtual City City { get; set; }

        [InverseProperty("ApplicationUser")]
        public virtual ICollection<Barter> InitiatedBarters { get; set; }
        [InverseProperty("ApplicationUser")]
        public virtual ICollection<Barter> JoinedBarters { get; set; }
    }
}
