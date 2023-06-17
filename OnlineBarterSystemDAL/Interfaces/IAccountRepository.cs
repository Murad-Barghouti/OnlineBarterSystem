using Microsoft.AspNetCore.Identity;
using OnlineBarterSystemDAL.HelperModels;
using OnlineBarterSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBarterSystemDAL.Interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(HSignUpModel signUpModel);
        Task<AspNetUser> SignInAsync(HSignInModel signInModel);
        Task<AspNetUser> GetUserByUserNameAsync(string userName);
        Task<AspNetUser> UpdateUserAsync(string? userName, string? currentPassword, string? newPassword, string? firstName,
            string? lastName, string? phoneNumber, long? cityId);
    }
}
