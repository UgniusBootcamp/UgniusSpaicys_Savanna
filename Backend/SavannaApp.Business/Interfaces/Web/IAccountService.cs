using SavannaApp.Data.Dto.Account;

namespace SavannaApp.Business.Interfaces.Web
{
    public interface IAccountService
    {
        /// <summary>
        /// Method to register user
        /// </summary>
        /// <param name="registerDto">registration data</param>
        /// <returns>registered user</returns>
        public Task<UserDto> RegisterAsync(RegisterDto registerDto);

        /// <summary>
        /// Method for login
        /// </summary>
        /// <param name="loginDto">login data</param>
        /// <returns>access token info</returns>
        public Task<AccessTokenDto> LoginAsync(LoginDto loginDto);

        /// <summary>
        /// Method for creating refresh token
        /// </summary>
        /// <param name="sessionId">session id</param>
        /// <param name="userId">user id</param>
        /// <returns>created refresh token</returns>
        public Task<string> CreateRefreshTokenAsync(Guid sessionId, string userId);

        /// <summary>
        /// Method to get access token from refresh token
        /// </summary>
        /// <param name="refreshToken">refresh token</param>
        /// <returns>parsed access token</returns>
        public Task<AccessTokenDto> GetAccessTokenFromRefreshToken(string? refreshToken);

        /// <summary>
        /// Method to get sessionId from refresh token
        /// </summary>
        /// <param name="refreshToken">refresh token</param>
        /// <returns>parsed session id</returns>
        public string GetSessionIdFromRefreshToken(string? refreshToken);
    }
}
