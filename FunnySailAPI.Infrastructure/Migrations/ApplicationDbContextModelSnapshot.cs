﻿// <auto-generated />
using System;
using FunnySailAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FunnySailAPI.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatBookingEN", b =>
                {
                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("BoatId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("BookingId", "BoatId");

                    b.HasIndex("BoatId");

                    b.ToTable("BoatBooking");
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatEN", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("BoatTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("PendingToReview")
                        .HasColumnType("bit");

                    b.Property<string>("UsersENUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BoatTypeId");

                    b.HasIndex("UsersENUserId");

                    b.ToTable("Boats");
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatInfoEN", b =>
                {
                    b.Property<int>("BoatId")
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<decimal>("Length")
                        .HasColumnType("decimal(9, 2)");

                    b.Property<string>("MooringPoint")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("MotorPower")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Registration")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<decimal>("Sleeve")
                        .HasColumnType("decimal(9, 2)");

                    b.HasKey("BoatId");

                    b.ToTable("BoatInfo");
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatPricesEN", b =>
                {
                    b.Property<int>("BoatId")
                        .HasColumnType("int");

                    b.Property<decimal>("DayBasePrice")
                        .HasColumnType("money");

                    b.Property<decimal>("HourBasePrice")
                        .HasColumnType("money");

                    b.Property<float>("Supplement")
                        .HasColumnType("real");

                    b.HasKey("BoatId");

                    b.ToTable("BoatPrices");
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatResourceEN", b =>
                {
                    b.Property<int>("BoatId")
                        .HasColumnType("int");

                    b.Property<string>("Uri")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Main")
                        .HasColumnType("bit");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("BoatId", "Uri");

                    b.ToTable("BoatResources");
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatTitlesEnumsEN", b =>
                {
                    b.Property<int>("TitleId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("TitleId");

                    b.ToTable("BoatTitlesEnums");
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatTypeEN", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("BoatTypes");
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BookingEN", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("ClientUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.Property<bool>("RequestCaptain")
                        .HasColumnType("bit");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalPeople")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientUserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.ClientInvoiceEN", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Canceled")
                        .HasColumnType("bit");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("ClientUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("ClientUserId");

                    b.ToTable("ClientInvoice");
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.InvoiceLineEN", b =>
                {
                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int?>("ClientInvoiceId")
                        .HasColumnType("int");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("money");

                    b.HasKey("BookingId");

                    b.HasIndex("ClientInvoiceId");

                    b.ToTable("InvoiceLines");
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.RequiredBoatTitleEN", b =>
                {
                    b.Property<int>("BoatId")
                        .HasColumnType("int");

                    b.Property<int>("TitleId")
                        .HasColumnType("int");

                    b.HasKey("BoatId", "TitleId");

                    b.HasIndex("TitleId");

                    b.ToTable("RequiredBoatTitles");
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.UsersEN", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("date");

                    b.Property<bool>("BoatOwner")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<bool>("ReceivePromotion")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.ToTable("UsersInfo");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatBookingEN", b =>
                {
                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatEN", "Boat")
                        .WithMany()
                        .HasForeignKey("BoatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BookingEN", "Booking")
                        .WithMany()
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatEN", b =>
                {
                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatTypeEN", "BoatType")
                        .WithMany("Boats")
                        .HasForeignKey("BoatTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.UsersEN", null)
                        .WithMany("Boats")
                        .HasForeignKey("UsersENUserId");
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatInfoEN", b =>
                {
                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatEN", "Boat")
                        .WithOne("BoatInfo")
                        .HasForeignKey("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatInfoEN", "BoatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatPricesEN", b =>
                {
                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatEN", "Boat")
                        .WithOne("BoatPrices")
                        .HasForeignKey("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatPricesEN", "BoatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatResourceEN", b =>
                {
                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatEN", "Boat")
                        .WithMany("BoatResources")
                        .HasForeignKey("BoatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BookingEN", b =>
                {
                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.UsersEN", "Client")
                        .WithMany()
                        .HasForeignKey("ClientUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.ClientInvoiceEN", b =>
                {
                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.UsersEN", "Client")
                        .WithMany()
                        .HasForeignKey("ClientUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.InvoiceLineEN", b =>
                {
                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BookingEN", "Booking")
                        .WithOne("InvoiceLine")
                        .HasForeignKey("FunnySailAPI.ApplicationCore.Models.FunnySailEN.InvoiceLineEN", "BookingId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.ClientInvoiceEN", "ClientInvoice")
                        .WithMany("InvoiceLines")
                        .HasForeignKey("ClientInvoiceId");
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.RequiredBoatTitleEN", b =>
                {
                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatEN", "Boat")
                        .WithMany("RequiredBoatTitles")
                        .HasForeignKey("BoatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.BoatTitlesEnumsEN", null)
                        .WithMany("RequiredBoatTitles")
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FunnySailAPI.ApplicationCore.Models.FunnySailEN.UsersEN", b =>
                {
                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.ApplicationUser", "ApplicationUser")
                        .WithOne("Users")
                        .HasForeignKey("FunnySailAPI.ApplicationCore.Models.FunnySailEN.UsersEN", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FunnySailAPI.ApplicationCore.Models.FunnySailEN.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
