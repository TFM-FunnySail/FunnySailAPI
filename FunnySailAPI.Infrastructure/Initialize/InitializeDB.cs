using FunnySailAPI.ApplicationCore.Constants;
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

                if (!  _dbContext.Roles.Any(ro => ro.Name == UserRoleEnum.Admin.ToString()))
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
                    List<ActivityEN> activities = new List<ActivityEN>();

                    activities.Add(new ActivityEN
                    {
                        Active = true,
                        Description = $"Paseo en catamarán con unas espectaculares vistas.",
                        Name = $"Paseo en catamarán",
                        Price = 10,
                        ActivityResources = new List<ActivityResourcesEN>
                        {
                            new ActivityResourcesEN
                            {
                                Resource = new ResourcesEN
                                {
                                    Main = true,
                                    Type = ResourcesEnum.Image,
                                    Uri = "paseo_catamaran1.jpg"
                                }
                            },
                            new ActivityResourcesEN
                            {
                                Resource = new ResourcesEN
                                {
                                    Main = false,
                                    Type = ResourcesEnum.Image,
                                    Uri = "paseo_catamaran2.jpg"
                                }
                            }
                        }
                    });

                    activities.Add(new ActivityEN
                    {
                        Active = true,
                        Description = $"Buceo en un espectacular banco de peces.",
                        Name = $"Buceo",
                        Price = 25,
                        ActivityResources = new List<ActivityResourcesEN>
                        {
                            new ActivityResourcesEN
                            {
                                Resource = new ResourcesEN
                                {
                                    Main = true,
                                    Type = ResourcesEnum.Image,
                                    Uri = "buceo1.jpg"
                                }
                            },
                            new ActivityResourcesEN
                            {
                                Resource = new ResourcesEN
                                {
                                    Main = false,
                                    Type = ResourcesEnum.Image,
                                    Uri = "buceo2.jpg"
                                }
                            },
                            new ActivityResourcesEN
                            {
                                Resource = new ResourcesEN
                                {
                                    Main = false,
                                    Type = ResourcesEnum.Image,
                                    Uri = "buceo3.jpg"
                                }
                            }
                        }
                    });

                    activities.Add(new ActivityEN
                    {
                        Active = true,
                        Description = $"Carrera de motos acuáticas durante 1 hora.",
                        Name = $"Motos acuáticas",
                        Price = 22,
                        ActivityResources = new List<ActivityResourcesEN>
                        {
                            new ActivityResourcesEN
                            {
                                Resource = new ResourcesEN
                                {
                                    Main = true,
                                    Type = ResourcesEnum.Image,
                                    Uri = "motos_acuaticas1.jpg"
                                }
                            },
                            new ActivityResourcesEN
                            {
                                Resource = new ResourcesEN
                                {
                                    Main = false,
                                    Type = ResourcesEnum.Image,
                                    Uri = "motos_acuaticas2.jpg"
                                }
                            }
                        }
                    });

                    await _dbContext.Activity.AddRangeAsync(activities);
                    await _dbContext.SaveChangesAsync();
                }

                if (!await _dbContext.Services.AnyAsync())
                {
                    List<ServiceEN> services = new List<ServiceEN>();

                    services.Add(new ServiceEN
                    {
                        Description = $"Una profesional de la limpieza se hará cargo de su camarote todos los días",
                        Name = $"Limpieza de la embracación",
                        Active = true,
                        Price = (decimal)27
                    });
                    services.Add(new ServiceEN
                    {
                        Description = $"Todas las mañanas tendrá acceso a nuestro surtido desayuno",
                        Name = $"Desayuno",
                        Active = true,
                        Price = (decimal)15.65
                    });
                    services.Add(new ServiceEN
                    {
                        Description = $"Tendrá acceso a nuestra wifi exclusiva",
                        Name = $"Wifi",
                        Active = true,
                        Price = (decimal)35.23
                    });
                    services.Add(new ServiceEN
                    {
                        Description = $"Un mayordomo lo acompañará a donde vaya, y se hará cargo de todas sus necesidades",
                        Name = $"Mayordomo",
                        Active = true,
                        Price = (decimal)64
                    });

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
                    List<BoatTypeEN> boats = new List<BoatTypeEN>();

                    boats.AddRange(new List<BoatTypeEN>
                    {
                        new BoatTypeEN
                        {
                            Name = "Velero",
                            Description = ""
                        },
                        new BoatTypeEN
                        {
                            Name = "Lancha",
                            Description = ""
                        },
                        new BoatTypeEN
                        {
                            Name = "Neumática",
                            Description = ""
                        },
                        new BoatTypeEN
                        {
                            Name = "Catamarán",
                            Description = ""
                        },
                        new BoatTypeEN
                        {
                            Name = "Goleta",
                            Description = ""
                        },
                        new BoatTypeEN
                        {
                            Name = "Moto de agua",
                            Description = ""
                        },
                        new BoatTypeEN
                        {
                            Name = "Casa flotante",
                            Description = ""
                        },
                        new BoatTypeEN
                        {
                            Name = "Yate",
                            Description = ""
                        }
                    });

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

                if (!await _dbContext.BoatTitles.AnyAsync())
                {

                    List<BoatTitlesEN> requiredTitles = new List<BoatTitlesEN>();

                    requiredTitles.Add(new BoatTitlesEN
                    {
                        Name = "Captaincy",
                        Description = "Titulación de capitanía"
                    });

                    requiredTitles.Add(new BoatTitlesEN
                    {
                        Name = "NavigationLicence",
                        Description = "Licencia de navegación"
                    });

                    requiredTitles.Add(new BoatTitlesEN
                    {
                        Name = "Patronja",
                        Description = "Titulación de patron/a de embarcaciones"
                    });

                    await _dbContext.BoatTitles.AddRangeAsync(requiredTitles);
                    await _dbContext.SaveChangesAsync();
                }

                if (!await _dbContext.Boats.AnyAsync()) {

                    IList<BoatTypeEN> boatsType = await _dbContext.BoatTypes.ToListAsync();
                    var user = await _dbContext.Users.Include(x=>x.Users).FirstOrDefaultAsync();
                    IList<BoatTitlesEN> requiredTitles = await _dbContext.BoatTitles.ToListAsync();

                    _dbContext.Boats.AddRange(new List<BoatEN>
                    {
                        new BoatEN
                        {
                            Active = true,
                            MooringId = 1,
                            CreatedDate = DateTime.UtcNow,
                            OwnerId = user.Id,
                            BoatTypeId = boatsType.FirstOrDefault(x=>x.Name == "Lancha")?.Id ?? 1,
                            BoatResources = new List<BoatResourceEN>
                            {
                                new BoatResourceEN
                                {
                                    Resource = new ResourcesEN
                                    {
                                        Main = true,
                                        Uri = "1.png",
                                        Type = ResourcesEnum.Image
                                    }
                                },new BoatResourceEN
                                {
                                    Resource = new ResourcesEN
                                    {
                                        Main = true,
                                        Uri = "2.png",
                                        Type = ResourcesEnum.Image
                                    }
                                }
                            },
                            BoatInfo = new BoatInfoEN
                            {
                              Capacity = 20,
                              Description = @"Sin patrón y se requiere titulación náutica.
Sundeck es la versión más familiar de la gama Flyer de Beneteau, y se caracteriza por un confort a bordo excepcional en una unidad de este tamaño. Con dos asientos de pilotaje, una amplia banqueta en popa y un solárium en proa, la Flyer 5.5 le destinan al puro descanso en el mar.

Plataformas de baño que hacen muy comodo el acceso al barco desde el mar.

Equipamiento 
Equipo de música, Radio FM, Bluetooth, USB, Agua dulce, Enchufe 12 volt
Bimini, Altavoces de bañera, Mesa de cabina, Ducha exterior, Cojines de bañera, Escalera de baño, Solarium de proa

Equipamiento de navegación
GPS, Plotter, Sonda náutica, Radio VHF, Velocímetro, Ancla, Compás, Luces de Navegación",
                              Name = "Lancha Beneteau Flyer 5.5 115CV",
                              Length = 200,
                              MotorPower = 115,
                              Registration = "DSFKHAGK784854",
                              Sleeve = 35,
                            },
                            BoatPrices = new BoatPricesEN
                            {
                                DayBasePrice = 400,
                                HourBasePrice = (decimal)60.5,
                                PorcentPriceOwner = 20,
                                Supplement = 20
                            },
                            RequiredBoatTitles = requiredTitles.Select(x=> new RequiredBoatTitleEN
                            {
                                TitleId = x.TitleId
                            }).ToList()
                        },
                        new BoatEN
                        {
                            Active = true,
                            MooringId = 2,
                            CreatedDate = DateTime.UtcNow,
                            OwnerId = user.Id,
                            BoatTypeId = boatsType.FirstOrDefault(x=>x.Name == "Yate")?.Id ?? 1,
                            BoatResources = new List<BoatResourceEN>
                            {
                                new BoatResourceEN
                                {
                                    Resource = new ResourcesEN
                                    {
                                        Main = true,
                                        Uri = "3.png",
                                        Type = ResourcesEnum.Image
                                    }
                                },new BoatResourceEN
                                {
                                    Resource = new ResourcesEN
                                    {
                                        Main = true,
                                        Uri = "4.png",
                                        Type = ResourcesEnum.Image
                                    }
                                }
                            },
                            BoatInfo = new BoatInfoEN
                            {
                              Capacity = 30,
                              Description = @"Bienvenidos a nuestro yate de más de 18 metros de eslora y 5 camarotes. 

Navega en Barcelona con este estupendo Yate!

COCTEL DE BIENVENIDA Y APERITIVO.

# Incluidos en el precio
- IVA
- Seguros
- Amarre en el puerto base",
                              Name = "CUSTOM — TRAWLER 60' (2012)",
                              Length = 500,
                              MotorPower = 200,
                              Registration = "YATAJV2176347",
                              Sleeve = 90,
                            },
                            BoatPrices = new BoatPricesEN
                            {
                                DayBasePrice = 700,
                                HourBasePrice = (decimal)125,
                                PorcentPriceOwner = 25,
                                Supplement = 50
                            },
                            RequiredBoatTitles = requiredTitles.Select(x=> new RequiredBoatTitleEN
                            {
                                TitleId = x.TitleId
                            }).ToList()
                        },
                        new BoatEN
                        {
                            Active = true,
                            MooringId = 3,
                            CreatedDate = DateTime.UtcNow,
                            OwnerId = user.Id,
                            BoatTypeId = boatsType.FirstOrDefault(x=>x.Name == "Velero")?.Id ?? 1,
                            BoatResources = new List<BoatResourceEN>
                            {
                                new BoatResourceEN
                                {
                                    Resource = new ResourcesEN
                                    {
                                        Main = true,
                                        Uri = "5.png",
                                        Type = ResourcesEnum.Image
                                    }
                                },new BoatResourceEN
                                {
                                    Resource = new ResourcesEN
                                    {
                                        Main = true,
                                        Uri = "6.png",
                                        Type = ResourcesEnum.Image
                                    }
                                },new BoatResourceEN
                                {
                                    Resource = new ResourcesEN
                                    {
                                        Main = true,
                                        Uri = "7.png",
                                        Type = ResourcesEnum.Image
                                    }
                                }
                            },
                            BoatInfo = new BoatInfoEN
                            {
                              Capacity = 30,
                              Description = @"Alquiler de velero Dufour 430 con un casco muy moderno, diseñado por Umberto Felci, proporciona espacio abundante, especialmente cómodo para un barco de charter, logrando el equilibrio perfecto entre una apariencia muy estética y las dimensiones correctas para cada área. 3 amplias cabinas con salon convertible, 2 baños, salón con tragaluces grandes de cristal para mucha luz natural. Esto da como resultado en más espacio interior sin renunciar a amplios y seguros pasillos laterales en cubierta. Perfecto para que puedas disfrutar de tu alquiler de una embarcación en Barcelona.

En cubierta, se convertirá en una excelente zona de vida y descanso al aire libre, ya que incorpora una plataforma de baño XXL, espacio de estiba en popa (acceso por plataforma de baño) y una espectacular zona de cocina en popa con cocina y barbacoa..
Todo lo necesario para disfrutar de tus vacaciones en velero.",
                              Name = "DUFOUR — DUFORN GID SEA 43 (2001)",
                              Length = 300,
                              MotorPower = 112,
                              Registration = "DDKAB68942",
                              Sleeve = 25,
                            },
                            BoatPrices = new BoatPricesEN
                            {
                                DayBasePrice = 250,
                                HourBasePrice = (decimal)30,
                                PorcentPriceOwner = 20,
                                Supplement = 10
                            }
                        }
                    });
                    
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
