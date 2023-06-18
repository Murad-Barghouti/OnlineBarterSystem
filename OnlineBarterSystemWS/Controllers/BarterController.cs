using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public BarterController(IRequestMapper requestMapper, IResponseMapper responseMapper, IBarterRepository barterRepository,
            ISubCategoryRepository subCategoryRepository, IAccountRepository accountRepository)
        {
            _requestMapper = requestMapper;
            _responseMapper = responseMapper;
            _barterRepository = barterRepository;
            _subCategoryRepository = subCategoryRepository;
            _accountRepository = accountRepository;
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
                var responseEntity = _responseMapper.Map<Barter,BarterResponse>(barter);
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
    }
}
