using FunnySailAPI.ApplicationCore.EN.FunnySail;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD
{
    public class FunnySailDBContext : DbContext
    {
        public FunnySailDBContext()
        {

        }
        public FunnySailDBContext(DbContextOptions<FunnySailDBContext> options) : base(options)
        {

        }

        public DbSet<BoatType> BoatTypes { get; set; }
    }
}
