using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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

            modelBuilder.Entity<BoatEN>(b =>
            {
                b.HasOne(i => i.BoatInfo)
                .WithOne(i => i.Boat)
                .HasForeignKey<BoatInfoEN>(b => b.BoatId);
            });

            modelBuilder.Entity<RequiredBoatTitleEN>(rb => {
                
                rb.HasKey(b => new { b.BoatId, b.TitleId });

                rb.Property(r => r.TitleId).HasConversion<int>();
            });

            modelBuilder.Entity<BoatTitlesEnumsEN>(bt => {
                
                bt.HasKey(bt => bt.TitleId);

                bt.Property(bt => bt.TitleId).HasConversion<int>();

                bt.Property(bt => bt.Name).HasConversion<string>();
            });

            modelBuilder.Entity<BoatPricesEN>(bp =>
            {
                bp.HasKey(bt => bt.BoatId);
            });

            modelBuilder.Entity<ApplicationUser>(u =>
            {
                u.HasOne(i => i.Users)
                .WithOne(i => i.ApplicationUser)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey<UsersEN>(b => b.UserId);
            });

            modelBuilder.Entity<UsersEN>(u =>
            {
                u.HasKey(b => b.UserId);

            });

            modelBuilder.Entity<InvoiceLineEN>(x =>
            {
                x.HasKey(b => b.BookingId);

                x.Property(b => b.Currency).HasConversion<string>();
            });

            modelBuilder.Entity<BookingEN>(x =>
            {
                x.HasOne(i => i.InvoiceLine)
                .WithOne(i => i.Booking)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey<InvoiceLineEN>(b => b.BookingId);

                x.Property(b => b.Status).HasConversion<string>();
            });

            modelBuilder.Entity<BoatBookingEN>(bb =>
            {
                bb.HasKey(b => new { b.BookingId,b.BoatId});
            });
        }

        public DbSet<BoatTypeEN> BoatTypes { get; set; }
        public DbSet<BoatEN> Boats { get; set; }
        public DbSet<BoatResourceEN> BoatResources { get; set; }
        public DbSet<BoatInfoEN> BoatInfos { get; set; }
        public DbSet<RequiredBoatTitleEN> RequiredBoatTitles { get; set; }
        public DbSet<BoatTitlesEnumsEN> BoatTitlesEnums { get; set; }
        public DbSet<UsersEN> UsersInfo { get; set; }
        public DbSet<BookingEN> Bookings { get; set; }
        public DbSet<InvoiceLineEN> InvoiceLines { get; set; }
        public DbSet<ClientInvoiceEN> ClientInvoices { get; set; }
        public DbSet<BoatBookingEN> BoatBookings { get; set; }
    }
}
