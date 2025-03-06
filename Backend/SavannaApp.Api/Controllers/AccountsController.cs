using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SavannaApp.Business.Interfaces.Web;
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

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var user = await accountService.RegisterAsync(registerDto);
                return Created("", ApiResponse.CreatedResponse("User regiseter", user));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

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

                return Ok(ApiResponse.OkResponse("Login successful", new { AccessToken = login.Token }));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        [Route("AccessToken")]
        public async Task<IActionResult> AccessToken()
        {
            HttpContext.Request.Cookies.TryGetValue("RefreshToken", out var refreshToken);

            if (string.IsNullOrEmpty(refreshToken))
            {
                return UnprocessableEntity(ApiResponse.UnprocessableEntityResponse("Refresh token not found"));
            }

            try
            {
                var login = await accountService.GetAccessTokenFromRefreshToken(refreshToken);

                var sessionId = Guid.Parse(accountService.GetSessionIdFromRefreshToken(refreshToken));

                var sessionValid = await sessionService.IsSessionValidAsync(sessionId, refreshToken);
                if (!sessionValid)
                    return UnprocessableEntity(ApiResponse.UnprocessableEntityResponse("Session is not valid anymore"));

                var newRefreshToken = await accountService.CreateRefreshTokenAsync(sessionId, login.UserId);

                UpdateCookie(newRefreshToken);

                await sessionService.ExtendSessionsAsync(sessionId, newRefreshToken, DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays));

                return Ok(ApiResponse.OkResponse("Access token refreshed", new { AccessToken = login.Token }));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Request.Cookies.TryGetValue("RefreshToken", out var refreshToken);

            if (string.IsNullOrEmpty(refreshToken))
                return UnprocessableEntity(ApiResponse.UnprocessableEntityResponse("Refresh token not found"));

            try
            {
                var sessionId = Guid.Parse(accountService.GetSessionIdFromRefreshToken(refreshToken));

                await sessionService.InvalidateSessionAsync(sessionId);

                RemoveCookie("RefreshToken");

                return Ok(ApiResponse.OkResponse("Logout successful"));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetData()
        {
            var testData = "Auth is working";
            return Ok(ApiResponse.OkResponse("Auth test", new { testData }));
        }

        private void UpdateCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
                Secure = true
            };

            Response.Cookies.Append("RefreshToken", refreshToken, cookieOptions);
        }

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
