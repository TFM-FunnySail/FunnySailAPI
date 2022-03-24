using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest.FakeFactories
{
    public class SignInManagerMock
    {
        public Mock<SignInManager<ApplicationUser>> singInManager;
        public SignInManagerMock()
        {
            var userManager = new UserManagerMock();

            singInManager = new Mock<SignInManager<ApplicationUser>>(userManager.userManager,
                 new Mock<IHttpContextAccessor>().Object,
                 new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                 new Mock<IOptions<IdentityOptions>>().Object,
                 new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
                 new Mock<IAuthenticationSchemeProvider>().Object);
        }

        public void SetupForLoginPassSuccess()
        {
            singInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);
        }

        public void SetupForLoginPassFailed()
        {
            singInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Failed);
        }
    }
}
