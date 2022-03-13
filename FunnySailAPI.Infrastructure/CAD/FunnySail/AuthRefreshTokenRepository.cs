using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class AuthRefreshTokenRepository : BaseCAD<AuthRefreshToken>, IAuthRefreshTokenRepository
    {
        public AuthRefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<AuthRefreshToken> generateRefreshTokens(ApplicationUser user,
                                                                  string ipAddress,
                                                                  int refreshTokenTTL)
        {
            AuthRefreshToken refreshToken = new AuthRefreshToken
            {
                Token = randomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };

            await _dbContext.AuthRefreshTokens.AddAsync(refreshToken);
            
            removeOldRefreshTokens(user, refreshTokenTTL);

            await _dbContext.SaveChangesAsync();

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

        private void removeOldRefreshTokens(ApplicationUser user,int refreshTokenTTL)
        {
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(refreshTokenTTL) <= DateTime.UtcNow);
        }
    }
}
