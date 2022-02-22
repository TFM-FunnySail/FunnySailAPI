using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.Initialize
{
    public class InitializeDB : IInitializeDB
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public InitializeDB(ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Initialize()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _dbContext.Database.Migrate();
                }

                if (_dbContext.Roles.Any(ro => ro.Name == UserRoleEnum.Admin.ToString())) return;

                await _roleManager.CreateAsync(new IdentityRole(UserRolesConstant.CLIENT));
                await _roleManager.CreateAsync(new IdentityRole(UserRolesConstant.ADMIN));
                await _roleManager.CreateAsync(new IdentityRole(UserRolesConstant.BOAT_OWNER));

                IdentityResult identityResult = await _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@funnysail.com",
                    NormalizedUserName = "ADMIN"
                }, "Admin1234*");

                var user = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(user, UserRolesConstant.ADMIN);
            }
            catch (Exception)
            {

            }
        }
    }

    public interface IInitializeDB
    {
        Task Initialize();
    }
}
