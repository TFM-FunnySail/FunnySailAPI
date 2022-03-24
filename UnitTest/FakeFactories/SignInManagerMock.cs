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
            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            singInManager = new Mock<SignInManager<ApplicationUser>>(userManager.userManager.Object,
                 _contextAccessor.Object, _userPrincipalFactory.Object,
                 null,null,null,null);
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
