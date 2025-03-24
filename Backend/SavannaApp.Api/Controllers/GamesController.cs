using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Interfaces.Web;
using SavannaApp.Business.Services;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Responses;

namespace SavannaApp.Api.Controllers
{
    [ApiController]
    [Route(EndpointConstants.Api + "[controller]")]
    public class GamesController(AnimalTypeMapper animalTypeMapper, IBlobService blobService) : BaseController
    {
        [HttpGet(EndpointConstants.AnimalTypes)]
        [Authorize]
        public IActionResult GetAnimalTypes()
        {
            return Ok(ApiResponse.OkResponse(WebConstants.AnimalTypes, animalTypeMapper.MapAnimalTypes()));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetGames(DateOnly start, DateOnly end)
        {
            var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(ApiResponse.UnauthorizedResponse("User ID is missing from the token"));

            var games = await blobService.GetUserGamesAsync(userId, start, end);

            return Ok(ApiResponse.OkResponse("User Games", games));
        }
    }
}
