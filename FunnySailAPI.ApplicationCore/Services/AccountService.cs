using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
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
        private readonly IAuthRefreshTokenRepository _authRefreshTokenRepository;
        public AccountService(SignInManager<ApplicationUser> signInManager,
                              UserManager<ApplicationUser> userManager,
                              IOptions<AppSettings> appSettings,
                              IAuthRefreshTokenRepository authRefreshTokenRepository)
        {
            _signInManager = signInManager; 
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _authRefreshTokenRepository = authRefreshTokenRepository;
        }


        public async Task<AuthenticateResponseDTO> LoginUser(LoginUserInputDTO loginUserInput,
                                                             string ipAddress)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(loginUserInput.Email, loginUserInput.Password, true, lockoutOnFailure: false);
            if (!result.Succeeded)
                throw new DataValidationException("Invalid username or password.",
                    "Usuario o contraseña inválida.");

            ApplicationUser user = await _userManager.FindByEmailAsync(loginUserInput.Email);

            string jwtToken = generateJwtToken(user);

            AuthRefreshToken refreshToken = await _authRefreshTokenRepository.
                generateRefreshTokens(user,ipAddress,_appSettings.RefreshTokenTTL);

            return new AuthenticateResponseDTO
            {
                Email = loginUserInput.Email,
                Id = user.Id,
                IsVerified = user.EmailConfirmed,
                JwtToken = jwtToken,
                RefreshToken = refreshToken.Token,
                Created = refreshToken.Created
            };
        }

        private string generateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        
    }
}
