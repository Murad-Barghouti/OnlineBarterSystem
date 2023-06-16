using OnlineBarterSystemWS.Generic.Models.Request;
using System.ComponentModel.DataAnnotations;

namespace OnlineBarterSystemWS.Models.Request
{
    public class SignUpModel : AEntityRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public long CityId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password don't match")]
        public string ConfirmPassword { get; set; }
    }
}
