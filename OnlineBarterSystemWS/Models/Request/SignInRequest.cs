using OnlineBarterSystemWS.Generic.Models.Request;
using System.ComponentModel.DataAnnotations;

namespace OnlineBarterSystemWS.Models.Request
{
    public class SignInRequest : AEntityRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
