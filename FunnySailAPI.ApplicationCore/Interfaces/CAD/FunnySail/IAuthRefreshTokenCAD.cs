using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IAuthRefreshTokenCAD : IBaseCAD<AuthRefreshToken>
    {
        Task<AuthRefreshToken> GetRefreshToken(string token);
        Task RemoveOldRefreshTokens(ApplicationUser user, int refreshTokenTTL);
        Task<bool> AnyUserWithToken(string userId, string token);
    }
}
