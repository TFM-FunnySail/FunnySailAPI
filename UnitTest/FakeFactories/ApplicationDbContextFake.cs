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
