using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using SavannaApp.Data.Constants;

namespace SavannaApp.Data.Responses
{
    public class ApiResponse(bool success, int status, string message, object? data, List<string>? errors)
    {
        public bool Success { get; set; } = success;
        public int Status { get; set; } = status;
        public string Message { get; set; } = message;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Data { get; set; } = data;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Errors { get; set; } = errors;

        public static ApiResponse OkResponse(string message, object? data = null)
        {
            return new ApiResponse(true, StatusCodes.Status200OK, message, data, null);
        }

        public static ApiResponse CreatedResponse(string message, object? data = null)
        {
            return new ApiResponse(true, StatusCodes.Status201Created, message, data, null);
        }

        public static ApiResponse NoContentResponse()
        {
            return new ApiResponse(true, StatusCodes.Status204NoContent, WebServiceConstants.NoContent, null, null);
        }

        public static ApiResponse ErrorResponse(string message, List<string> errors)
        {
            return new ApiResponse(false, StatusCodes.Status400BadRequest, message, null, errors);
        }

        public static ApiResponse NotFoundResponse(string message)
        {
            return new ApiResponse(false, StatusCodes.Status404NotFound, message, null, null);
        }

        public static ApiResponse InternalServerErrorResponse(string message)
        {
            return new ApiResponse(false, StatusCodes.Status500InternalServerError, message, null, null);
        }

        public static ApiResponse UnauthorizedResponse(string message)
        {
            return new ApiResponse(false, StatusCodes.Status401Unauthorized, message, null, null);
        }

        public static ApiResponse ForbiddenResponse(string message)
        {
            return new ApiResponse(false, StatusCodes.Status403Forbidden, message, null, null);
        }

        public static ApiResponse UnprocessableEntityResponse(string message)
        {
            return new ApiResponse(false, StatusCodes.Status422UnprocessableEntity, message, null, null);
        }
    }
}
