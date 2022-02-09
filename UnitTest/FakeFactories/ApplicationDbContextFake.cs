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
    }
}
