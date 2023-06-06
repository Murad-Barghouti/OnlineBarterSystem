using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBarterSystemDAL.ModelsBackup
{
    public class Barter
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string? Description { get; set; }
        public double? GiveValue { get; set; }
        public double? ReceiveValue { get; set; }
        [Required]
        public long BarterStateId { get; set; }
        [Required]
        public long InitiatorId { get; set; }
        public long? JoinerId { get; set; }
        public long? GiveTypeId { get; set; }
        public long? ReceiveTypeId { get; set; }

        [ForeignKey(nameof(BarterStateId))]
        [InverseProperty("Barter")]
        public virtual BarterState State { get; set; }

        [ForeignKey(nameof(InitiatorId))]
        [InverseProperty("Barter")]
        public virtual ApplicationUser Initiator { get; set; }

        [ForeignKey(nameof(JoinerId))]
        [InverseProperty("Barter")]
        public virtual ApplicationUser Joiner { get; set; }

        [ForeignKey(nameof(GiveTypeId))]
        [InverseProperty("Barter")]
        public virtual SubCategory GiveType { get; set; }

        [ForeignKey(nameof(ReceiveTypeId))]
        [InverseProperty("Barter")]
        public virtual SubCategory ReceiveType { get; set; }

    }
}
