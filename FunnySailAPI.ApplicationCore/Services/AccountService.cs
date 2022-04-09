using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Account;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.User;
using FunnySailAPI.ApplicationCore.Models.DTO.Output.Account;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppSettings _appSettings;
        private readonly IAuthRefreshTokenCEN _authRefreshTokenCEN;
        public AccountService(SignInManager<ApplicationUser> signInManager,
                              UserManager<ApplicationUser> userManager,
                              IOptions<AppSettings> appSettings,
                              IAuthRefreshTokenCEN authRefreshTokenCEN)
        {
            _signInManager = signInManager; 
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _authRefreshTokenCEN = authRefreshTokenCEN;
        }

        public async Task<bool> IsOwnsToken(UsersEN user, string token)
        {
            return await _authRefreshTokenCEN.GetAuthRefreshTokenCAD()
                .AnyUserWithToken(user.UserId,token);
        }

        public async Task<AuthenticateResponseDTO> LoginUser(LoginUserInputDTO loginUserInput,
                                                             string ipAddress)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(loginUserInput.Email, loginUserInput.Password, true, lockoutOnFailure: false);
            if (!result.Succeeded)
                throw new DataValidationException("Invalid username or password.",
                    "Usuario o contraseña inválida.");

            ApplicationUser user = await _userManager.FindByEmailAsync(loginUserInput.Email);

            string jwtToken = _authRefreshTokenCEN.GenerateJwtToken(user);

            AuthRefreshToken refreshToken = await _authRefreshTokenCEN.
                GenerateRefreshTokens(user,ipAddress,null);

            return new AuthenticateResponseDTO
            {
                Email = loginUserInput.Email,
                Id = user.Id,
                IsVerified = user.EmailConfirmed,
                JwtToken = jwtToken,
                RefreshToken = refreshToken.Token,
                Created = refreshToken.Created,
                JwtTokenExpiresIn = TokenInfoConstant.tokenExpiresInSeconds,
                RefreshTokenExpiresIn = TokenInfoConstant.refreshTokenExpiresInSeconds
            };
        }

        public async Task<AuthenticateResponseDTO> RefreshToken(string token, string ipAddress)
        {
            var refreshToken = await _authRefreshTokenCEN.GetAuthRefreshTokenCAD().GetRefreshToken(token);
            ApplicationUser user = await _userManager.FindByIdAsync(refreshToken.UserId);

            var newRefreshToken = await _authRefreshTokenCEN.GenerateRefreshTokens(user,ipAddress, refreshToken);

            // generate new jwt
            string jwtToken = _authRefreshTokenCEN.GenerateJwtToken(user);

            return new AuthenticateResponseDTO
            {
                Email = user.Email,
                Id = user.Id,
                IsVerified = user.EmailConfirmed,
                JwtToken = jwtToken,
                RefreshToken = newRefreshToken.Token,
                Created = newRefreshToken.Created
            };
        }

        public async Task RevokeToken(string token, string ipAddress)
        {
            await _authRefreshTokenCEN.RevokeToken(token, ipAddress);
        }
        
    }
}
