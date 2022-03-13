using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class AuthRefreshTokenCAD : BaseCAD<AuthRefreshToken>, IAuthRefreshTokenCAD
    {
        public AuthRefreshTokenCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<AuthRefreshToken> GetRefreshToken(string token)
        {
            return await _dbContext.AuthRefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task RemoveOldRefreshTokens(ApplicationUser user,int refreshTokenTTL)
        {
            var refrehsTokenToDelete = await _dbContext.AuthRefreshTokens.Where(x =>
                x.Revoked != null || DateTime.UtcNow >= x.Expires ||
                x.Created.AddDays(refreshTokenTTL) <= DateTime.UtcNow).ToListAsync();
            
            _dbContext.AuthRefreshTokens.RemoveRange(refrehsTokenToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
