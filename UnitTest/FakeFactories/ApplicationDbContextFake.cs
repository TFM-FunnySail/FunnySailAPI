using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
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
            builder.UseInMemoryDatabase("funnySail");

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

            _dbContextFake.SaveChanges();
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
