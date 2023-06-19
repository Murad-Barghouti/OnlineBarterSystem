using OnlineBarterSystemWS.Generic.Models.Request;
using System.ComponentModel.DataAnnotations;

namespace OnlineBarterSystemWS.Models.Request
{
    public class UpdateUserRequest : AEntityRequest
    {
        public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password and confirmation password don't match")]
        public string? ConfirmPassword { get; set; }
       
        public long? CityId { get; set; }
    }
}
