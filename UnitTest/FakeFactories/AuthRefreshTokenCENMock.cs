using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest.FakeFactories
{
    public class AuthRefreshTokenCENMock
    {
        public Mock<IAuthRefreshTokenCEN> authRefreshToken;
        public AuthRefreshTokenCENMock(IOptions<AppSettings> appSettings)
        {
            var applicationDbContextFake = new ApplicationDbContextFake();

            IAuthRefreshTokenCAD authRefreshTokenCAD = new AuthRefreshTokenCAD(applicationDbContextFake._dbContextFake);
            
            authRefreshToken = new Mock<AuthRefreshTokenCEN>(authRefreshTokenCAD, appSettings)
                .As<IAuthRefreshTokenCEN>();
        }

        public void SetupForGenerateToken()
        {
            authRefreshToken.Setup(x => x.GenerateRefreshTokens(It.IsAny<ApplicationUser>(), It.IsAny<string>(),
                It.IsAny<AuthRefreshToken>()))
                .ReturnsAsync( new AuthRefreshToken());
            
            authRefreshToken.Setup(x => x.GenerateJwtToken(It.IsAny<ApplicationUser>()))
                .Returns(("ssdsdsd", new DateTime()));
        }
    }
}
