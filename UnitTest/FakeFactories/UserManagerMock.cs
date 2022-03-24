using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest.FakeFactories
{
    public class UserManagerMock
    {
        public Mock<UserManager<ApplicationUser>> userManager;
        public UserManagerMock()
        {

            var userStore = new Mock<IUserStore<ApplicationUser>>();

            userManager = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
            userManager.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            userManager.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

        }

        public void SetupForCreateUser()
        {
            userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser user, string pass) => {
                    user.Id = "sadaddad";
                    return IdentityResult.Success;
                });

            userManager.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
        }

        public void SetupForEditUser()
        {
            userManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);
        }

        public void SetupForFindByEmail()
        {
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email)=> {
                    return new ApplicationUser { Email = email };
                });
        }
    }
}
