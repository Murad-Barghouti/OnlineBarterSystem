
using Microsoft.AspNetCore.Identity;
using OnlineBarterSystemDAL.HelperModels;
using OnlineBarterSystemDAL.Interfaces;
using OnlineBarterSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
