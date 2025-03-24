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
        /// <summary>
        /// Endpoint to get animal types
        /// </summary>
        /// <returns>animal types</returns>
        [HttpGet(EndpointConstants.AnimalTypes)]
        [Authorize]
        public IActionResult GetAnimalTypes()
        {
            return Ok(ApiResponse.OkResponse(WebConstants.AnimalTypes, animalTypeMapper.MapAnimalTypes()));
        }

        /// <summary>
        /// Endpoint to get user previous games
        /// </summary>
        /// <param name="start">start date</param>
        /// <param name="end">end date</param>
        /// <returns>user games in range of dates</returns>
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
