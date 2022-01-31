﻿using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
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

            modelBuilder.Entity<BoatInfoEN>()
                .HasKey(b => b.BoatId);

            modelBuilder.Entity<BoatEN>()
            .HasOne(b => b.BoatInfo)
            .WithOne(i => i.Boat)
            .HasForeignKey<BoatInfoEN>(b => b.BoatId);

            modelBuilder.Entity<RequiredBoatTitleEN>()
                .HasKey(b => new { b.BoatId,b.TitleId });

            modelBuilder.Entity<RequiredBoatTitleEN>()
                .Property(r => r.TitleId)
                .HasConversion<int>();
        }

        public DbSet<BoatTypeEN> BoatTypes { get; set; }
        public DbSet<BoatEN> Boats { get; set; }
        public DbSet<BoatResourceEN> BoatResources { get; set; }
        public DbSet<BoatInfoEN> BoatInfos { get; set; }
        public DbSet<RequiredBoatTitleEN> RequiredBoatTitles { get; set; }
    }
}
