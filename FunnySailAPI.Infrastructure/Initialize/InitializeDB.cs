﻿using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Booking;
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
using FunnySailAPI.ApplicationCore.Models.DTO.Input;

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

                if (!await _dbContext.Services.AnyAsync())
                {
                    int index = 1;
                    List<ServiceEN> services = new List<ServiceEN>();
                    for (; index <= 50; index++)
                    {
                        bool active = NextFloat(0, 1) > (decimal)0.5;
                        services.Add(new ServiceEN
                        {
                            Description = $"Desc {index}",
                            Name = $"Servicio {index}",
                            Active = active,
                            Price = NextFloat(1, 100)
                        });
                    }
                    await _dbContext.Services.AddRangeAsync(services);
                    await _dbContext.SaveChangesAsync();
                }

                //if (!await _dbContext.Bookings.AnyAsync())
                //{
                //    //Crear reservas de servicios y actividades
                //    int index = 1;
                //    List<UsersEN> users = await _dbContext.UsersInfo.ToListAsync();
                //    List<ServiceEN> services = await _dbContext.Services.ToListAsync();
                //    List<ActivityEN> activities = await _dbContext.Activity.ToListAsync();
                //    for (; index <= 20; index++)
                //    {
                //        bool requestCapitan = NextFloat(0, 1) > (decimal)0.5;
                //        UsersEN user = users.ElementAt(NextInt(0, users.Count));
                //        int dayAfter = NextInt(50, 100);
                //        int hoursAfter = NextInt(0, 32);
                //        List<ServiceEN> servicesBooking = new List<ServiceEN>
                //        {
                //            services.ElementAt(NextInt(0, services.Count)),
                //            services.ElementAt(NextInt(0, services.Count)),
                //        };
                //        List<ActivityEN> activitiesBooking = new List<ActivityEN>
                //        {
                //            activities.ElementAt(NextInt(0, activities.Count)),
                //            activities.ElementAt(NextInt(0, activities.Count)),
                //        };
                //        await _unitOfWork.BookingCP.CreateBooking(new AddBookingInputDTO
                //        {
                //            RequestCaptain = requestCapitan,
                //            ClientId = user.UserId,
                //            BoatIds = new List<int>(),
                //            TotalPeople = NextInt(1,20),
                //            DepartureDate = DateTime.UtcNow.AddDays(dayAfter).AddHours(hoursAfter),
                //            EntryDate = DateTime.UtcNow.AddDays(dayAfter),
                //            ActivityIds = activitiesBooking.Select(x=>x.Id).ToList(),
                //            ServiceIds = servicesBooking.Select(x=>x.Id).ToList(),
                //        });
                //    }

                //}
           
                if (!await _dbContext.BoatTypes.AnyAsync())
                {
                    int index = 1;
                    List<BoatTypeEN> boats = new List<BoatTypeEN>();
                    for (; index <= 10; index++)
                    {
                        bool active = NextFloat(0, 1) > (decimal)0.5;
                        boats.Add(new BoatTypeEN
                        {
                            Name = "Tipo prueba " + index,
                            Description = "Desripcion tipo prueba " + index
                        });
                    }
                    await _dbContext.BoatTypes.AddRangeAsync(boats);
                    await _dbContext.SaveChangesAsync();
                }

                if (!await _dbContext.Ports.AnyAsync())
                {
                    int index = 1;
                    List<PortEN> ports = new List<PortEN>();
                    for (; index <= 5; index++)
                    {
                        bool active = NextFloat(0, 1) > (decimal)0.5;
                        ports.Add(new PortEN
                        {
                            Name = "Puerto prueba " + index,
                            Location = "calle prueba " + index
                        });
                    }
                    await _dbContext.Ports.AddRangeAsync(ports);
                    await _dbContext.SaveChangesAsync();
                }

                if (!await _dbContext.Moorings.AnyAsync())
                {
                    int index = 1;
                    List<MooringEN> moorings = new List<MooringEN>();
                    for (; index <= 20; index++)
                    {
                        bool active = NextFloat(0, 1) > (decimal)0.5;
                        moorings.Add(new MooringEN
                        {
                            Alias = "Mooring " + index,
                            PortId = NextInt(1, 5)
                        });
                    }
                    await _dbContext.Moorings.AddRangeAsync(moorings);
                    await _dbContext.SaveChangesAsync();
                }

                if (!await _dbContext.Resources.AnyAsync())
                {
                    int index = 1;
                    List<ResourcesEN> resources = new List<ResourcesEN>();
                    for (; index <= 7; index++)
                    {
                        await _unitOfWork.ResourcesCEN.AddResources(false, 0, index.ToString()+ ".png");
                    }
                    await _dbContext.SaveChangesAsync();
                }

                if (!await _dbContext.TechnicalServices.AnyAsync())
                {
                    int index = 1;
                    List<TechnicalServiceEN> technicalServices = new List<TechnicalServiceEN>();
                    for (; index <= 50; index++)
                    {
                        bool active = NextFloat(0, 1) > (decimal)0.5;
                        technicalServices.Add(new TechnicalServiceEN
                        {
                            Description = $"Desc {index}",
                            Active = active,
                            Price = NextFloat(1, 100)
                        });
                    }
                    await _dbContext.TechnicalServices.AddRangeAsync(technicalServices);
                    await _dbContext.SaveChangesAsync();
                }

                if (!await _dbContext.Boats.AnyAsync()) { 
                    int index = 1;

                    //  Listas vacias
                    List<int> resourcesIds = new List<int>();
                    List<AddRequiredBoatTitleInputDTO> requiredBoatTitles = new List<AddRequiredBoatTitleInputDTO>();

                    for (; index <= 7; index++)
                    {
                        resourcesIds.Clear();
                        resourcesIds.Add(index);

                        requiredBoatTitles.Clear();
                        if (index < 3)
                        {
                            requiredBoatTitles.Add(new AddRequiredBoatTitleInputDTO
                            {
                                Title = BoatTiteEnum.Patronja
                            });
                        }
                        else {
                            requiredBoatTitles.Add(new AddRequiredBoatTitleInputDTO
                            {
                                Title = BoatTiteEnum.NavigationLicence 
                            });
                            requiredBoatTitles.Add(new AddRequiredBoatTitleInputDTO
                            {
                                Title = BoatTiteEnum.Captaincy
                            });
                        }

                        await _unitOfWork.BoatCP.CreateBoat(new AddBoatInputDTO
                        {
                            BoatTypeId = index,
                            Name = "Embarcación prueba " + index,
                            Capacity = index * 3,
                            MotorPower = index * 20,
                            Sleeve = index * 20,
                            Supplement = 20,         
                            Registration = "Reg prueba " + index,
                            OwnerId = "04395eac-522d-476a-99e6-f96e3a409d4b",              
                            Description = "Descripción embarcación prueba " + index,
                            DayBasePrice = NextInt(100, 300),
                            HourBasePrice = NextInt(15, 30),
                            Length = NextInt(40, 100),
                            MooringId = index,
                            MooringPoint = "Puerto prueba " + index,
                            ResourcesIdList = resourcesIds,
                            RequiredBoatTitles = requiredBoatTitles,
                        });
                    }
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
        private int NextInt(int min, int max)
        {
            System.Random random = new System.Random();
            double val = (random.NextDouble() * (max - min) + min);
            return (int)val;
        }
    }


    public interface IInitializeDB
    {
        Task Initialize();
    }
}
