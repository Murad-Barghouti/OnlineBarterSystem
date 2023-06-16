using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBarterSystemDAL.HelperModels;
using OnlineBarterSystemDAL.Interfaces;
using OnlineBarterSystemWS.Models.Request;
using OnlineBarterSystemWS.Utilities;
using System.Net;

namespace OnlineBarterSystemWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRequestMapper _requestMapper;
        private readonly IAccountRepository _accountRepository;
        private readonly ICityRepository _cityRepository;

        public AccountController(IRequestMapper requestMapper, IAccountRepository accountRepository, ICityRepository cityRepository)
        {
            _requestMapper = requestMapper;
            _accountRepository = accountRepository;
            _cityRepository = cityRepository;
        }

        [HttpPost("signup")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            try
            {
                var city = await _cityRepository.GetCityByIdAsync(signUpModel.CityId);
                if (city == null)
                {
                    return NotFound($"City with Id {signUpModel.CityId} does not exist");
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
    }
}
