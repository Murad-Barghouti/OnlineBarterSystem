using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    public class BarterController : ControllerBase
    {
        private readonly IRequestMapper _requestMapper;
        private readonly IResponseMapper _responseMapper;
        private readonly IBarterRepository _barterRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICityRepository _cityRepository;

        public BarterController(IRequestMapper requestMapper, IResponseMapper responseMapper, IBarterRepository barterRepository,
            ISubCategoryRepository subCategoryRepository, IAccountRepository accountRepository, ICityRepository cityRepository)
        {
            _requestMapper = requestMapper;
            _responseMapper = responseMapper;
            _barterRepository = barterRepository;
            _subCategoryRepository = subCategoryRepository;
            _accountRepository = accountRepository;
            _cityRepository = cityRepository;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateBarter([FromBody] CreateBarterRequest createBarterRequest)
        {
            try
            {
                var user = await _accountRepository.GetUserByIdAsync(createBarterRequest.InitiatorId);
                if (user == null)
                {
                    return BadRequest("User does not exist");
                }

                var receiveCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(createBarterRequest.ReceiveTypeId);
                if (receiveCategory == null)
                {
                    return BadRequest($"Subcategory for receiving does not exist");
                }

                var giveCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(createBarterRequest.GiveTypeId);
                if (giveCategory == null)
                {
                    return BadRequest($"Subcategory for giving does not exist");
                }

                var result = await _barterRepository.IsBarterValidAsync(createBarterRequest.GiveTypeId, createBarterRequest.ReceiveTypeId,
                    createBarterRequest.GiveValue, createBarterRequest.ReceiveValue);
                if (result.Item1 == false)
                {
                    return BadRequest(result.Item2);
                }

                var barter = await _barterRepository.CreateBarter(createBarterRequest.InitiatorId, createBarterRequest.GiveTypeId, createBarterRequest.ReceiveTypeId,
                    createBarterRequest.GiveValue, createBarterRequest.ReceiveValue, createBarterRequest.Description);
                if (barter == null)
                {
                    throw new Exception();
                }
                var responseEntity = _responseMapper.Map<Barter, BarterResponse>(barter);
                return Ok(responseEntity);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<BarterResponse>>> GetBarters()
        {
            try
            {
                var barters = await _barterRepository.GetAllBartersAsync();
                var bartersResponse = _responseMapper.Map<Barter, BarterResponse>(barters);
                return Ok(bartersResponse);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("bartersInCity/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<BarterResponse>>> GetBartersInCity(long id)
        {
            try
            {
                var city = await _cityRepository.GetCityByIdAsync(id);
                if (city == null)
                {
                    return BadRequest("City not found");
                }
                var barters = await _barterRepository.GetBartersInCityAsync(id);
                var bartersResponse = _responseMapper.Map<Barter, BarterResponse>(barters);
                return Ok(bartersResponse);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<BarterResponse>>> GetUserBarters(long id)
        {
            try
            {
                var user = await _accountRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    return BadRequest("The user does not exist");
                }
                var barters = await _barterRepository.GetUserBartersAsync(id);
                if (barters.Count() == 0)
                {
                    return NoContent();
                }
                var bartersResponse = _responseMapper.Map<Barter, BarterResponse>(barters);
                return Ok(bartersResponse);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateBarter(long id, [FromBody] UpdateBarterRequest updateBarterRequest)
        {
            try
            {
                var barter = await _barterRepository.GetBarterByIdAsync(id);
                if (barter == null)
                {
                    return NotFound("Barter not found");
                }
                var updatedBarter = await _barterRepository.UpdateBarterAsync(id, updateBarterRequest.GiveValue, updateBarterRequest.ReceiveValue,
                    updateBarterRequest.Description);

                var barterResponse = _responseMapper.Map<Barter, BarterResponse>(updatedBarter);
                return Ok(barterResponse);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteBarter(long id)
        {
            try
            {
                var barter = await _barterRepository.GetBarterByIdAsync(id);
                if (barter == null)
                {
                    return NotFound("Barter not found");
                }
                var deletedBarter = await _barterRepository.DeleteBarterByIdAsync(id);

                var barterResponse = _responseMapper.Map<Barter, BarterResponse>(deletedBarter);
                return Ok(barterResponse);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("joinBarter/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> JoinBarter(long id, [FromQuery][BindRequired] string userName)
        {
            try
            {
                var barter = await _barterRepository.GetBarterByIdAsync(id);
                if (barter == null)
                {
                    return NotFound("Barter not found");
                }
                var user = await _accountRepository.GetUserByUserNameAsync(userName);
                if (user == null)
                {
                    return BadRequest($"User with user name {userName} not found");
                }
                if (user.Id == barter.InitiatorId)
                {
                    return Conflict("Cant join a barter you initiated");
                }
                barter = await _barterRepository.JoinBarterAsync(id, userName);
                var barterResponse = _responseMapper.Map<Barter, BarterResponse>(barter);
                return Ok(barterResponse);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("leaveBarter/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> LeaveBarter(long id)
        {
            try
            {
                var barter = await _barterRepository.GetBarterByIdAsync(id);
                if (barter == null)
                {
                    return NotFound("Barter not found");
                }
                /*
                var user = await _accountRepository.GetUserByUserNameAsync(userName);
                if (user == null)
                {
                    return BadRequest($"User with user name {userName} not found");
                }
                */
                barter = await _barterRepository.LeaveBarterAsync(id);
                var barterResponse = _responseMapper.Map<Barter, BarterResponse>(barter);
                return Ok(barterResponse);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("rejectBarter/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RejectBarter(long id)
        {
            try
            {
                var barter = await _barterRepository.GetBarterByIdAsync(id);
                if (barter == null)
                {
                    return NotFound("Barter not found");
                }
                if (barter.Joiner == null)
                {
                    return Conflict("Barter doesn't have a joiner");
                }
                /*
                var user = await _accountRepository.GetUserByUserNameAsync(userName);
                if (user == null)
                {
                    return BadRequest($"User with user name {userName} not found");
                }
                */
                barter = await _barterRepository.RejectBarterAsync(id);
                var barterResponse = _responseMapper.Map<Barter, BarterResponse>(barter);
                return Ok(barterResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("approveBarter/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ApproveBarter(long id)
        {
            try
            {
                var barter = await _barterRepository.GetBarterByIdAsync(id);
                if (barter == null)
                {
                    return NotFound("Barter not found");
                }
                if (barter.Joiner == null)
                {
                    return Conflict("Barter doesn't have a joiner");
                }
                /*
                var user = await _accountRepository.GetUserByUserNameAsync(userName);
                if (user == null)
                {
                    return BadRequest($"User with user name {userName} not found");
                }
                */
                barter = await _barterRepository.ApproveBarterAsync(id);
                var barterResponse = _responseMapper.Map<Barter, BarterResponse>(barter);
                return Ok(barterResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
