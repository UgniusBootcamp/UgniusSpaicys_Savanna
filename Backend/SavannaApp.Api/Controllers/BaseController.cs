using Microsoft.AspNetCore.Mvc;
using SavannaApp.Data.Helpers.Exceptions;
using SavannaApp.Data.Responses;

namespace SavannaApp.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Generic method for handling errors in controllers
        /// </summary>
        /// <param name="ex">exception</param>
        /// <returns>appropriate status code</returns>
        protected IActionResult HandleException(Exception ex)
        {
            var message = ex.Message;

            var response = ex switch
            {
                NotFoundException => ApiResponse.NotFoundResponse(message),
                BusinessRuleValidationException => ApiResponse.UnprocessableEntityResponse(message),
                ForbiddenException => ApiResponse.ForbiddenResponse(message),
                _ => ApiResponse.InternalServerErrorResponse(message)
            };

            return StatusCode(response.Status, response);
        }
    }
}
