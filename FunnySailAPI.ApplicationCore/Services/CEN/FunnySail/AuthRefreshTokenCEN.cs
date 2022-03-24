using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class AuthRefreshTokenCEN : IAuthRefreshTokenCEN
    {
        private readonly IAuthRefreshTokenCAD _authRefreshTokenCAD;
        private readonly AppSettings _appSettings;
        public AuthRefreshTokenCEN(IAuthRefreshTokenCAD authRefreshTokenCAD,
            IOptions<AppSettings> appSettings)
        {
            _authRefreshTokenCAD = authRefreshTokenCAD;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthRefreshToken> GenerateRefreshTokens(ApplicationUser user,
                                                                  string ipAddress,
                                                                  AuthRefreshToken oldRefreshToken)
        {
            AuthRefreshToken refreshToken = new AuthRefreshToken
            {
                Token = randomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress,
                UserId = user.Id
            };

            await _authRefreshTokenCAD.RemoveOldRefreshTokens(user, _appSettings.RefreshTokenTTL);

            await _authRefreshTokenCAD.AddAsync(refreshToken);

            if(oldRefreshToken != null)
            {
                await RevokeToken(oldRefreshToken, ipAddress, refreshToken.Token);
            }

            return refreshToken;
        }


        public async Task<AuthRefreshToken> RevokeToken(AuthRefreshToken refreshToken, string ipAddress, string newToken)
        {
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newToken;

            await _authRefreshTokenCAD.Update(refreshToken);

            return refreshToken;
        }

        private string randomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        public IAuthRefreshTokenCAD GetAuthRefreshTokenCAD()
        {
            return _authRefreshTokenCAD;
        }

        public async Task RevokeToken(string token, string ipAddress)
        {
            AuthRefreshToken refreshToken = await _authRefreshTokenCAD.GetRefreshToken(token);

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;

            await _authRefreshTokenCAD.Update(refreshToken);
        }

        public string GenerateJwtToken(ApplicationUser user)
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
