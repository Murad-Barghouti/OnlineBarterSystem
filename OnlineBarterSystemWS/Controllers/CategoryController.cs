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
    public class CategoryController : ControllerBase
    {
        private readonly IRequestMapper _requestMapper;
        private readonly IResponseMapper _responseMapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(IRequestMapper requestMapper, IResponseMapper responseMapper, ICategoryRepository categoryRepository)
        {
            _requestMapper = requestMapper;
            _responseMapper = responseMapper;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetCategories();
                var categoriesResponse = _responseMapper.Map<Category, ParentCategoryResponse>(categories);
                return Ok(categoriesResponse);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
