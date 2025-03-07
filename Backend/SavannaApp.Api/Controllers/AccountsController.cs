using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SavannaApp.Business.Interfaces.Web;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Dto.Account;
using SavannaApp.Data.Helpers.Configuration;
using SavannaApp.Data.Responses;

namespace SavannaApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController(
        IAccountService accountService,
        ISessionService sessionService,
        IOptions<JwtSettings> jwtSettings
        ) : BaseController
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        /// <summary>
        /// Register user to the system
        /// </summary>
        /// <param name="registerDto">registration info</param>
        /// <returns>registered user</returns>
        /// <response code="201">If used was successfully registered</response>>
        /// <response code="422">If username already exists</response>>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var user = await accountService.RegisterAsync(registerDto);
                return Created("", ApiResponse.CreatedResponse(WebConstants.UserRegister, user));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Endpoint for login
        /// </summary>
        /// <param name="loginDto">login data</param>
        /// <returns>access token</returns>
        /// <response code="200">successful login</response>>
        /// <response code="422">login incorrect</response>>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var login = await accountService.LoginAsync(loginDto);

                var sessionId = Guid.NewGuid();

                var refreshToken = await accountService.CreateRefreshTokenAsync(sessionId, login.UserId);

                await sessionService.CreateSessionAsync(sessionId, login.UserId, refreshToken, DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays));

                UpdateCookie(refreshToken);

                return Ok(ApiResponse.OkResponse(WebConstants.LoginSuccessful), new { AccessToken = login.Token }));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Ednpoint for access token refresh
        /// </summary>
        /// <returns>refreshed access token</returns>
        /// <response code="200">new access token</response>>
        /// <response code="422">If refresh not found or invalid or session invalid</response>>
        [HttpPost]
        [Route("AccessToken")]
        public async Task<IActionResult> AccessToken()
        {
            HttpContext.Request.Cookies.TryGetValue(WebConstants.RefreshToken, out var refreshToken);

            if (string.IsNullOrEmpty(refreshToken))
            {
                return UnprocessableEntity(ApiResponse.UnprocessableEntityResponse(WebConstants.RefreshTokenNotFound));
            }

            try
            {
                var login = await accountService.GetAccessTokenFromRefreshToken(refreshToken);

                var sessionId = Guid.Parse(accountService.GetSessionIdFromRefreshToken(refreshToken));

                var sessionValid = await sessionService.IsSessionValidAsync(sessionId, refreshToken);
                if (!sessionValid)
                    return UnprocessableEntity(ApiResponse.UnprocessableEntityResponse(WebConstants.SessionNotValid));

                var newRefreshToken = await accountService.CreateRefreshTokenAsync(sessionId, login.UserId);

                UpdateCookie(newRefreshToken);

                await sessionService.ExtendSessionsAsync(sessionId, newRefreshToken, DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays));

                return Ok(ApiResponse.OkResponse(WebConstants.AccessTokenRefreshed, new { AccessToken = login.Token }));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Endpoint for logout
        /// </summary>
        /// <returns>message for successful logout</returns>
        /// <response code="200">If logout successful</response>>
        /// <response code="401">Not authorized</response>>
        /// <response code="422">Refresh token not found</response>>
        [HttpPost]
        [Route("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Request.Cookies.TryGetValue(WebConstants.RefreshToken, out var refreshToken);

            if (string.IsNullOrEmpty(refreshToken))
                return UnprocessableEntity(ApiResponse.UnprocessableEntityResponse(WebConstants.RefreshTokenNotFound));

            try
            {
                var sessionId = Guid.Parse(accountService.GetSessionIdFromRefreshToken(refreshToken));

                await sessionService.InvalidateSessionAsync(sessionId);

                RemoveCookie(WebConstants.RefreshToken);

                return Ok(ApiResponse.OkResponse(WebConstants.LogoutSuccessful));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Test endpoint to see if auth is working
        /// </summary>
        /// <returns>test data</returns>
        [HttpGet]
        [Authorize]
        public IActionResult GetData()
        {
            var testData = "Auth is working";
            return Ok(ApiResponse.OkResponse("Auth test", new { testData }));
        }

        /// <summary>
        /// Method for refresh token update in cookies
        /// </summary>
        /// <param name="refreshToken">refresh token</param>
        private void UpdateCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
                Secure = true
            };

            Response.Cookies.Append(WebConstants.RefreshToken, refreshToken, cookieOptions);
        }

        /// <summary>
        /// Method for removal of refresh token
        /// </summary>
        /// <param name="cookieName">refresh token to remove</param>
        private void RemoveCookie(string cookieName)
        {
            HttpContext.Response.Cookies.Append(cookieName, "", new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });
        }
    }
}
