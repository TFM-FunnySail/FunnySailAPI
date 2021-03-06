using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Extensions;
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
using System.Linq;
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
                                                             string ipAddress,
                                                             bool toAdmin = false)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(loginUserInput.Email, loginUserInput.Password, true, lockoutOnFailure: false);
            if (!result.Succeeded)
                throw new DataValidationException("Invalid username or password.",
                    "Usuario o contraseña inválida.");

            ApplicationUser user = await _userManager.FindByEmailAsync(loginUserInput.Email);

            if (toAdmin && !await _userManager.IsInRoleAsync(user, UserRolesConstant.ADMIN))
            {
                throw new DataValidationException("Denied access.",
                        "Acceso denegado.");
            }

            (string jwtToken,DateTime expires) = _authRefreshTokenCEN.GenerateJwtToken(user);

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
                JwtTokenExpiresIn = expires.ToUnixLongTimeStamp(),
            };
        }

        public async Task<AuthenticateResponseDTO> RefreshToken(string token, string ipAddress)
        {
            var refreshToken = await _authRefreshTokenCEN.GetAuthRefreshTokenCAD().GetRefreshToken(token);
            ApplicationUser user = await _userManager.FindByIdAsync(refreshToken.UserId);

            var newRefreshToken = await _authRefreshTokenCEN.GenerateRefreshTokens(user,ipAddress, refreshToken);

            // generate new jwt
            (string jwtToken,DateTime expires) = _authRefreshTokenCEN.GenerateJwtToken(user);

            return new AuthenticateResponseDTO
            {
                Email = user.Email,
                Id = user.Id,
                IsVerified = user.EmailConfirmed,
                JwtToken = jwtToken,
                RefreshToken = newRefreshToken.Token,
                Created = newRefreshToken.Created,
                JwtTokenExpiresIn = expires.ToUnixLongTimeStamp(),
            };
        }

        public async Task RevokeToken(string token, string ipAddress)
        {
            await _authRefreshTokenCEN.RevokeToken(token, ipAddress);
        }

        public async Task ChangePassword(ApplicationUser user, ChangePasswordDTO changePasswordInput)
        {
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, changePasswordInput.OldPassword, changePasswordInput.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                string errors = String.Join($"{Environment.NewLine}",
                    changePasswordResult.Errors?.Select(x => x.Description).ToList());

                throw new DataValidationException(errors, errors);
            }

            await _signInManager.RefreshSignInAsync(user);
        }
    }
}
