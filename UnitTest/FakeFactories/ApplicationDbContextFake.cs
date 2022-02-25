using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.FakeFactories
{
    public class ApplicationDbContextFake
    {
        public ApplicationDbContext _dbContextFake { get; set; }

        public ApplicationDbContextFake()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("funnySail")
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            _dbContextFake = new ApplicationDbContext(builder.Options);

            Seed();
        }

        public async Task DisposeAsyn()
        {
            await _dbContextFake.DisposeAsync();
        }

        private void Seed()
        {
            _dbContextFake.Database.EnsureDeleted();
            _dbContextFake.Database.EnsureCreated();

            //Agregando botes
            _dbContextFake.AddRange(BoatsFaker());

            //Agregando user
            _dbContextFake.AddRange(UserFaker());

            //Agregando activities
            _dbContextFake.AddRange(ActivitiesFaker());

            //Agregando ports
            _dbContextFake.AddRange(PortsFaker());

            //Agregando services
            _dbContextFake.AddRange(ServicesFaker());
            _dbContextFake.AddRange(ServicesBookingFaker());
            
            _dbContextFake.SaveChanges();
        }

        private List<UsersEN> UserFaker()
        {
            return new List<UsersEN>
            {
                new UsersEN
                {
                    UserId = "1",
                    BoatOwner = false,
                    LastName = "Merten",
                    FirstName = "Pedro",
                    ReceivePromotion = true
                }
            };
        }

        private List<BoatEN> BoatsFaker()
        {
            return new List<BoatEN>
            {
                new BoatEN // Barco peniente de revisión
                {
                    Id = 1,
                    PendingToReview = true,
                    Active = false,
                    CreatedDate = DateTime.UtcNow,
                    BoatType = new BoatTypeEN
                    {
                        Name = "Tipo prueba",
                        Description = "Desc prueba"
                    }
                },
                new BoatEN //Barco aprobado
                {
                    Id = 2,
                    PendingToReview = false,
                    Active = true,
                    CreatedDate = DateTime.UtcNow,
                    BoatType = new BoatTypeEN
                    {
                        Name = "Barco aprobado",
                        Description = "Desc Barco aprobado"
                    }
                }
            };

        }

        private List<ActivityEN> ActivitiesFaker()
        {
            return new List<ActivityEN>
            {
                new ActivityEN
                {
                    Id = 1,
                    Active = true,
                    ActivityDate = DateTime.UtcNow,
                    Name = "Buceo",
                    Price = 330,
                    Description = "Actividad de prueba 1"
                },
                new ActivityEN
                {
                    Id = 2,
                    Active = true,
                    ActivityDate = DateTime.UtcNow,
                    Name = "Pesca",
                    Price = 430,
                    Description = "Actividad de prueba 2"
                },
                new ActivityEN
                {
                    Id = 3,
                    Active = true,
                    ActivityDate = DateTime.UtcNow,
                    Name = "Pesca",
                    Price = 350,
                    Description = "Actividad de prueba 3"
                },
                  new ActivityEN
                {
                    Id = 4,
                    Active = false,
                    ActivityDate = DateTime.UtcNow,
                    Name = "Buceo",
                    Price = 280,
                    Description = "Actividad de prueba 4"
                },
                    new ActivityEN
                {
                    Id = 5,
                    Active = false,
                    ActivityDate = DateTime.UtcNow,
                    Name = "Pesca",
                    Price = 250,
                    Description = "Actividad de prueba 5"
                },
            };
        }

        private List<PortEN> PortsFaker()
        {
            return new List<PortEN>
            {
                new PortEN
                {
                    Id = 1,
                    Name = "Puerto de la felicidad",
                    Location = "c/Río Tajo"
                },
                new PortEN
                {
                    Id = 2,
                    Name = "Puerto de la tristeza",
                    Location = "c/Río Ebro"
                }
            };
        }

        private List<ServiceEN> ServicesFaker()
        {
            return new List<ServiceEN>
            {
                new ServiceEN
                {
                    Id = 1,
                    Active = true,
                    Name = "Servicio 1",
                    Price = 100,
                    Description = "Descripcion servicio 1"
                },
                new ServiceEN
                {
                    Id = 2,
                    Active = true,
                    Name = "Servicio 2",
                    Price = 80,
                    Description = "Descripcion servicio 2"
                },       
            };
        }

        private List<ServiceBookingEN> ServicesBookingFaker()
        {
            return new List<ServiceBookingEN>
            {
                new ServiceBookingEN
                {
                    ServiceId = 2   
                },
            };
        }


        public void Add<T>(T newEntity)
        {
             _dbContextFake.Add(newEntity);
             _dbContextFake.SaveChanges();
        }

        public void Remove<T>(List<T> entityList)
        {
            _dbContextFake.RemoveRange(entityList);
            _dbContextFake.SaveChanges();
        }
    }
}
