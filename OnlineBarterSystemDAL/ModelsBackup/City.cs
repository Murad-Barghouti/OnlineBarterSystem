using System.ComponentModel.DataAnnotations;


namespace OnlineBarterSystemDAL.ModelsBackup
{
    public class City
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
