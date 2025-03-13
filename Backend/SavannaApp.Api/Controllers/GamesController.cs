using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Services;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Responses;

namespace SavannaApp.Api.Controllers
{
    [ApiController]
    [Route(EndpointConstants.Api + "[controller]")]
    public class GamesController(AnimalTypeMapper animalTypeMapper) : BaseController
    {
        [HttpGet("AnimalTypes")]
        [Authorize]
        public IActionResult GetAnimalTypes()
        {
            try
            {
                return Ok(ApiResponse.OkResponse("Animal Types", animalTypeMapper.MapAnimalTypes()));
            }
            catch (Exception ex) 
            {
                return HandleException(ex);
            }
        }
    }
}
