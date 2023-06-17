
using Microsoft.AspNetCore.Identity;
using OnlineBarterSystemDAL.HelperModels;
using OnlineBarterSystemDAL.Interfaces;
using OnlineBarterSystemDAL.Models;
using Microsoft.EntityFrameworkCore;
using OnlineBarterSystemDAL.Exceptions;

namespace OnlineBarterSystemDAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AspNetUser> _userManager;
        //private readonly SignInManager<AspNetUser> _signInManager;

        public AccountRepository(UserManager<AspNetUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<AspNetUser> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }
        public async Task<IdentityResult> SignUpAsync(HSignUpModel signUpModel)
        {
            var user = new AspNetUser()
            {
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.Email,
                UserName = signUpModel.UserName,
                PhoneNumber = signUpModel.PhoneNumber,
                CityId = signUpModel.CityId,
            };

            return await _userManager.CreateAsync(user, signUpModel.Password);
        }

        public Task<AspNetUser> SignInAsync(HSignInModel signInModel)
        {
            throw new NotImplementedException();
        }

        public async Task<AspNetUser> GetUserByUserNameAsync(string userName)
        {
            var user = await _userManager.Users.Include(u => u.City).Where(u => u.UserName.ToLower().Equals(userName.ToLower())).FirstOrDefaultAsync();
            return user;
        }

        public async Task<AspNetUser> UpdateUserAsync(string? userName, string? currentPassword, string? newPassword, string? firstName,
            string? lastName, string? phoneNumber, long? cityId)
        {
            var user = await GetUserByUserNameAsync(userName);
            if (user != null)
            {
                if (currentPassword != null && newPassword != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                    if (!result.Succeeded)
                    {
                        throw new UnAutharizedException("Password is incorrect");
                    }
                }
                if (firstName != null)
                {
                    user.FirstName = firstName;
                }
                if (lastName != null)
                {
                    user.LastName = lastName;
                }
                if (phoneNumber != null)
                {
                    user.PhoneNumber = phoneNumber;
                }
                if (cityId != null)
                {
                    user.CityId = (long)cityId;
                }
                await _userManager.UpdateAsync(user);
            }
            return user;
        }
    }
}
