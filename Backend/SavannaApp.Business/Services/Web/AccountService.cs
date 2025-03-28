﻿using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using SavannaApp.Business.Interfaces.Web;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Dto.Account;
using SavannaApp.Data.Entities.Auth;
using SavannaApp.Data.Enums;
using SavannaApp.Data.Helpers.Exceptions;
using SavannaApp.Data.Interfaces.Repo;

namespace SavannaApp.Business.Services.Web
{
    public class AccountService(
        IAccountRepository accountRepository,
        IValidationService validationService,
        IJwtTokenService jwtTokenService,
        IMapper mapper
        ) : IAccountService
    {
        /// <summary>
        /// Method for creating refresh token
        /// </summary>
        /// <param name="sessionId">session id</param>
        /// <param name="userId">user id</param>
        /// <returns>created refresh token</returns>
        public async Task<string> CreateRefreshTokenAsync(Guid sessionId, string userId)
        {
            var user = await accountRepository.FindUserByIdAsync(userId);

            if (user == null)
                throw new NotFoundException(WebServiceConstants.UserNotFound);

            return jwtTokenService.CreateRefreshToken(sessionId, user.Id);
        }

        /// <summary>
        /// Method to get access token from refresh token
        /// </summary>
        /// <param name="refreshToken">refresh token</param>
        /// <returns>parsed access token</returns>
        public async Task<AccessTokenDto> GetAccessTokenFromRefreshToken(string? refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken) ||
                !jwtTokenService.TryParseRefreshToken(refreshToken, out var claims) ||
                claims?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value is not string userId)
            {
                throw new BusinessRuleValidationException(WebServiceConstants.InvalidRefreshToken);

            }

            var user = await accountRepository.FindUserByIdAsync(userId);

            if (user == null)
                throw new NotFoundException(WebServiceConstants.UserNotFound);

            var userRoles = await accountRepository.GetUserRolesAsync(user);

            var accessToken = jwtTokenService.CreateAccessToken(user.UserName!, user.Id, userRoles);

            return new AccessTokenDto
            {
                UserId = user.Id,
                Token = accessToken
            };
        }

        /// <summary>
        /// Method to get sessionId from refresh token
        /// </summary>
        /// <param name="refreshToken">refresh token</param>
        /// <returns>parsed session id</returns>
        public string GetSessionIdFromRefreshToken(string? refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken) ||
                !jwtTokenService.TryParseRefreshToken(refreshToken, out var claims) ||
                claims?.FindFirst(WebServiceConstants.SessiondId)?.Value is not string sessionId)
            {
                throw new BusinessRuleValidationException(WebServiceConstants.InvalidRefreshToken);
            }

            return sessionId;
        }

        /// <summary>
        /// Method for login
        /// </summary>
        /// <param name="loginDto">login data</param>
        /// <returns>access token info</returns>
        public async Task<AccessTokenDto> LoginAsync(LoginDto loginDto)
        {
            var user = await accountRepository.FindUserByUsernameAsync(loginDto.UserName);

            if (user == null)
                throw new BusinessRuleValidationException(WebServiceConstants.InvalidUsernamePassword);

            var isPasswordValid = await accountRepository.IsPasswordValidAsync(user, loginDto.Password);

            if (!isPasswordValid)
                throw new BusinessRuleValidationException(WebServiceConstants.InvalidUsernamePassword);

            var allowerdRoles = new List<string> { UserRoles.User};
            var isAllowrd = await accountRepository.IsUserInRoleAsync(user, allowerdRoles);

            if(!isAllowrd)
                throw new BusinessRuleValidationException(WebServiceConstants.UserNotAllowedToSignIn);

            var userRoles = await accountRepository.GetUserRolesAsync(user);

            var accessToken = jwtTokenService.CreateAccessToken(user.UserName!, user.Id, userRoles);

            return new AccessTokenDto
            {
                UserId = user.Id,
                Token = accessToken
            };
        }

        /// <summary>
        /// Method to register user
        /// </summary>
        /// <param name="registerDto">registration data</param>
        /// <returns>registered user</returns>
        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = await accountRepository.FindUserByUsernameAsync(registerDto.UserName);

            if (user != null)
                throw new BusinessRuleValidationException(String.Format(WebServiceConstants.UsernameTaken, registerDto.UserName));

            validationService.ValidateRegisterPassword(registerDto.Password, registerDto.ConfirmPassword);

            var newUser = mapper.Map<User>(registerDto);

            var created = await accountRepository.CreateUserAsync(newUser, registerDto.Password, UserRoles.User);

            return mapper.Map<UserDto>(created);
        }
    }
}
