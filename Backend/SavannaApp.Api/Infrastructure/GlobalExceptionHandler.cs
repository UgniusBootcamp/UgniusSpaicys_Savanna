using Microsoft.AspNetCore.Diagnostics;
using SavannaApp.Data.Helpers.Exceptions;
using SavannaApp.Data.Responses;

namespace SavannaApp.Api.Infrastructure
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
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
