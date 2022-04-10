﻿using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.User;
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
        private readonly IUnitOfWork _unitOfWork;

        public InitializeDB(ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task Initialize()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _dbContext.Database.Migrate();
                }

                if (!_dbContext.Roles.Any(ro => ro.Name == UserRoleEnum.Admin.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRolesConstant.CLIENT));
                    await _roleManager.CreateAsync(new IdentityRole(UserRolesConstant.ADMIN));
                    await _roleManager.CreateAsync(new IdentityRole(UserRolesConstant.BOAT_OWNER));
                }

                //Crear usuarios
                string email = "admin1@funnysail.com";
                var user1 = await _userManager.FindByEmailAsync(email);
                if (user1 == null)
                {
                    (IdentityResult result, ApplicationUser user) = await _unitOfWork.UserCP.CreateUser(new AddUserInputDTO
                    {
                        Email = "admin1@funnysail.com",
                        ConfirmPassword = "Admin1**23*er",
                        FirstName = "Admin",
                        LastName = "Admin Last Name",
                        Password = "Admin1**23*er",
                        PhoneNumber = "5555555"
                    });
                    user.EmailConfirmed = true;
                    await _dbContext.SaveChangesAsync();
                    await _userManager.AddToRoleAsync(user, UserRolesConstant.ADMIN);
                }
                
                if(!await _dbContext.Activity.AnyAsync())
                {
                    int index = 1;
                    List<ActivityEN> activities = new List<ActivityEN>();
                    for (; index <= 50; index++)
                    {
                        bool active = NextFloat(0, 1) > (decimal)0.5;
                        activities.Add(new ActivityEN
                        {
                            Active = active,
                            Description = $"Actividad de prueba {index}",
                            ActivityDate = DateTime.UtcNow,
                            Name = $"Actividad {index}",
                            Price = NextFloat(1, 100)
                        });
                    }
                    await _dbContext.Activity.AddRangeAsync(activities);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

            }
        }
        private decimal NextFloat(float min, float max)
        {
            System.Random random = new System.Random();
            double val = (random.NextDouble() * (max - min) + min);
            return (decimal)val;
        }
    }


    public interface IInitializeDB
    {
        Task Initialize();
    }
}
