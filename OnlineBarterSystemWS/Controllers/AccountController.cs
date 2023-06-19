using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBarterSystemDAL.Exceptions;
using OnlineBarterSystemDAL.HelperModels;
using OnlineBarterSystemDAL.Interfaces;
using OnlineBarterSystemDAL.Models;
using OnlineBarterSystemWS.Models.Request;
using OnlineBarterSystemWS.Models.Response;
using OnlineBarterSystemWS.Utilities;
using System.Net;

namespace OnlineBarterSystemWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRequestMapper _requestMapper;
        private readonly IResponseMapper _responseMapper;
        private readonly IAccountRepository _accountRepository;
        private readonly ICityRepository _cityRepository;

        public AccountController(IRequestMapper requestMapper, IResponseMapper responseMapper, IAccountRepository accountRepository, ICityRepository cityRepository)
        {
            _requestMapper = requestMapper;
            _responseMapper = responseMapper;
            _accountRepository = accountRepository;
            _cityRepository = cityRepository;
        }

        [HttpPost("signup")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            try
            {
                var city = await _cityRepository.GetCityByIdAsync(signUpModel.CityId);
                if (city == null)
                {
                    return BadRequest($"City with Id {signUpModel.CityId} does not exist");
                }
                var signUpHelper = _requestMapper.Map<SignUpModel, HSignUpModel>(signUpModel);
                var result = await _accountRepository.SignUpAsync(signUpHelper);

                if (result.Succeeded)
                {
                    return Ok();
                }
                return Unauthorized(result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("signin")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest signInRequest)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{userName}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetUser(string userName)
        {
            try
            {
                var user = await _accountRepository.GetUserByUserNameAsync(userName);
                if (user == null)
                {
                    return NotFound($"User with user name {userName} does not exist");
                }
                var userResponse = _responseMapper.Map<AspNetUser, UserResponse>(user);

                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("updateUserInformation")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest updateUserRequest)
        {
            try
            {
                if (updateUserRequest.CityId != null)
                {
                    var city = await _cityRepository.GetCityByIdAsync((long)updateUserRequest.CityId);
                    if (city == null)
                    {
                        return BadRequest($"City with Id {updateUserRequest.CityId} does not exist");
                    }
                }
                var user = await _accountRepository.UpdateUserAsync(updateUserRequest.UserName, updateUserRequest.CurrentPassword,
                    updateUserRequest.NewPassword, updateUserRequest.FirstName, updateUserRequest.LastName, updateUserRequest.PhoneNumber,
                    updateUserRequest.CityId);
                if (user == null)
                {
                    return NotFound($"User with user name {updateUserRequest.UserName} does not exist");
                }
                var userResponse = _responseMapper.Map<AspNetUser, UserResponse>(user);
                return Ok(userResponse);
            }
            catch (UnAutharizedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
