using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN
{
    public interface IAuthRefreshTokenCEN
    {
        Task<AuthRefreshToken> RevokeToken(AuthRefreshToken refreshToken, string ipAddress, string newToken);
        Task<AuthRefreshToken> GenerateRefreshTokens(ApplicationUser user, string ipAddress, AuthRefreshToken oldRefreshToken);
        IAuthRefreshTokenCAD GetAuthRefreshTokenCAD();
        Task RevokeToken(string token, string ipAddress);
        string GenerateJwtToken(ApplicationUser user);
    }
}
