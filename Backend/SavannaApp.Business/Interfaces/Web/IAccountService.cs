using SavannaApp.Data.Dto.Account;

namespace SavannaApp.Business.Interfaces.Web
{
    public interface IAccountService
    {
        public Task<UserDto> RegisterAsync(RegisterDto registerDto);
        public Task<AccessTokenDto> LoginAsync(LoginDto loginDto);
        public Task<string> CreateRefreshTokenAsync(Guid sessionId, string userId);
        public Task<AccessTokenDto> GetAccessTokenFromRefreshToken(string? refreshToken);
        public string GetSessionIdFromRefreshToken(string? refreshToken);
    }
}
