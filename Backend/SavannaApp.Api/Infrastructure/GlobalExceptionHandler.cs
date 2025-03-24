using Microsoft.AspNetCore.Diagnostics;
using SavannaApp.Data.Helpers.Exceptions;
using SavannaApp.Data.Responses;

namespace SavannaApp.Api.Infrastructure
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        /// <summary>
        /// global exception handler
        /// </summary>
        /// <param name="httpContext">context</param>
        /// <param name="exception">exception</param>
        /// <param name="cancellationToken">cancelation token</param>
        /// <returns>json response to appropriate exception</returns>
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var message = exception.Message;
            var response = exception switch
            {
                NotFoundException => ApiResponse.NotFoundResponse(message),
                BusinessRuleValidationException => ApiResponse.UnprocessableEntityResponse(message),
                ForbiddenException => ApiResponse.ForbiddenResponse(message),
                _ => ApiResponse.InternalServerErrorResponse(message)
            };

            httpContext.Response.StatusCode = response.Status;

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}
