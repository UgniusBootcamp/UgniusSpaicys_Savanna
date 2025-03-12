﻿using System.Text.Json.Serialization;
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

        /// <summary>
        /// Ok response
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="data">data</param>
        /// <returns>ok response</returns>
        public static ApiResponse OkResponse(string message, object? data = null)
        {
            return new ApiResponse(true, StatusCodes.Status200OK, message, data, null);
        }

        /// <summary>
        /// Created response
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="data">data</param>
        /// <returns>created response</returns>
        public static ApiResponse CreatedResponse(string message, object? data = null)
        {
            return new ApiResponse(true, StatusCodes.Status201Created, message, data, null);
        }

        /// <summary>
        /// No content response
        /// </summary>
        /// <returns>no content response</returns>
        public static ApiResponse NoContentResponse()
        {
            return new ApiResponse(true, StatusCodes.Status204NoContent, WebServiceConstants.NoContent, null, null);
        }

        /// <summary>
        /// Error response
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="errors">errors</param>
        /// <returns>error response</returns>
        public static ApiResponse ErrorResponse(string message, List<string> errors)
        {
            return new ApiResponse(false, StatusCodes.Status400BadRequest, message, null, errors);
        }

        /// <summary>
        /// Not found response
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>not found response</returns>
        public static ApiResponse NotFoundResponse(string message)
        {
            return new ApiResponse(false, StatusCodes.Status404NotFound, message, null, null);
        }

        /// <summary>
        /// Internal server error response
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>internal server error</returns>
        public static ApiResponse InternalServerErrorResponse(string message)
        {
            return new ApiResponse(false, StatusCodes.Status500InternalServerError, message, null, null);
        }

        /// <summary>
        /// Unauthorized access response
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>unauthorized access</returns>
        public static ApiResponse UnauthorizedResponse(string message)
        {
            return new ApiResponse(false, StatusCodes.Status401Unauthorized, message, null, null);
        }

        /// <summary>
        /// Forbidden access response
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>forbidden access response</returns>
        public static ApiResponse ForbiddenResponse(string message)
        {
            return new ApiResponse(false, StatusCodes.Status403Forbidden, message, null, null);
        }

        /// <summary>
        /// Unprocessable entity response
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>unprocessable entity</returns>
        public static ApiResponse UnprocessableEntityResponse(string message)
        {
            return new ApiResponse(false, StatusCodes.Status422UnprocessableEntity, message, null, null);
        }
    }
}
