using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BoatResourceEN>()
                .HasKey(m => new { m.BoatId, m.Uri });
        }

        public DbSet<BoatTypeEN> BoatTypes { get; set; }
        public DbSet<BoatEN> Boats { get; set; }
        public DbSet<BoatResourceEN> BoatResources { get; set; }
    }
}
