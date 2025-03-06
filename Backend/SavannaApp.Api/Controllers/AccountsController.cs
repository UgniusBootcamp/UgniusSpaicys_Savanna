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

            Response.Cookies.Append(WebConstants.RefreshToken, refreshToken, cookieOptions);
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
