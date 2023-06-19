using OnlineBarterSystemWS.Generic.Models.Request;
using System.ComponentModel.DataAnnotations;

namespace OnlineBarterSystemWS.Models.Request
{
    public class SignInModel : AEntityRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
