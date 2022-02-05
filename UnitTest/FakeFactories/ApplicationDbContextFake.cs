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
            builder.EnableSensitiveDataLogging();

            _dbContextFake = new ApplicationDbContext(builder.Options);
        }

        public async Task DisposeAsyn()
        {
            await _dbContextFake.DisposeAsync();
        }
    }
}
