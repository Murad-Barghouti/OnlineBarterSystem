using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBarterSystemDAL.Interfaces;
using OnlineBarterSystemDAL.Models;
using OnlineBarterSystemWS.Models.Response;
using OnlineBarterSystemWS.Utilities;
using System.Net;

namespace OnlineBarterSystemWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IRequestMapper _requestMapper;
        private readonly IResponseMapper _responseMapper;
        private readonly ICityRepository _cityRepository;

        public CityController(IRequestMapper requestMapper, IResponseMapper responseMapper, ICityRepository cityRepository)
        {
            _requestMapper = requestMapper;
            _responseMapper = responseMapper;
            _cityRepository = cityRepository;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetCities()
        {
            try
            {
                var cities = await _cityRepository.GetAllCitiesAsync();
                var citiesResponse = _responseMapper.Map<City, CityResponse>(cities);
                return Ok(citiesResponse);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
