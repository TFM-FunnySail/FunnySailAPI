using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
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

            //Agregando OwnerInvoice
            _dbContextFake.AddRange(OwnerInvoiceFaker());

            //Agregando Mooring
            _dbContextFake.AddRange(MooringFaker());

            //Agregando Port
            _dbContextFake.AddRange(PortsFaker());

            //Agregando activities
            _dbContextFake.AddRange(ActivitiesFaker());

            //Agregando services
            _dbContextFake.AddRange(ServicesFaker());
            _dbContextFake.AddRange(ServicesBookingFaker());
            _dbContextFake.AddRange(TecServicesFaker());
            _dbContextFake.AddRange(TechnicalServiceBoatFaker());

            //Agregando bookings
            _dbContextFake.AddRange(BookingFaker());

            //Agregando recursos
            _dbContextFake.AddRange(ResourcesFaker());
            _dbContextFake.AddRange(BoatResourcesFaker());

            _dbContextFake.AddRange(ClientInvoiceFaker());

            _dbContextFake.AddRange(RefundFaker());

            _dbContextFake.SaveChanges();
        }

        private List<BoatResourceEN> BoatResourcesFaker()
        {
            return new List<BoatResourceEN>
            {
                new BoatResourceEN
                {
                    BoatId = 1,
                    ResourceId =3,
                },
                new BoatResourceEN
                {
                    BoatId = 1,
                    ResourceId =2,
                },
            };
        }

        private List<ResourcesEN> ResourcesFaker()
        {
            return new List<ResourcesEN>
            {
                new ResourcesEN
                {
                    Id = 1,
                    Main = true,
                    Type = ResourcesEnum.Image,
                    Uri = "Images/1.jpg"
                },
                new ResourcesEN
                {
                    Id = 2,
                    Main = false,
                    Type = ResourcesEnum.Image,
                    Uri = "Images/2.jpg"
                },
                new ResourcesEN
                {
                    Id = 3,
                    Main = false,
                    Type = ResourcesEnum.Image,
                    Uri = "Images/3.jpg"
                }
            };
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
                },
                new UsersEN
                {
                    UserId = "2",
                    BoatOwner = false,
                    LastName = "Quiez",
                    FirstName = "Rodri",
                    ReceivePromotion = false
                },
                new UsersEN
                {
                    UserId = "3",
                    BoatOwner = true,
                    LastName = "Quiez",
                    FirstName = "Rodri",
                    ReceivePromotion = false
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
                    OwnerId = "1",
                    BoatType = new BoatTypeEN
                    {
                        Id = 1,
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
                        Id = 2,
                        Name = "Barco aprobado",
                        Description = "Desc Barco aprobado"
                    }
                }
            };

        }

        private List<OwnerInvoiceEN> OwnerInvoiceFaker()
        {
            return new List<OwnerInvoiceEN>
            {
                new OwnerInvoiceEN
                {
                    OwnerId = "1",
                    Id = 1,
                    IsCanceled = false,
                    Amount = 10, 
                    IsPaid = false,
                    Date = DateTime.Now
                },
                new OwnerInvoiceEN
                {
                    OwnerId = "1",
                    Id = 2,
                    IsCanceled = true,
                    Amount = 10,
                    IsPaid = false,
                    Date = DateTime.Now
                },
                new OwnerInvoiceEN
                {
                    OwnerId = "1",
                    Id = 3,
                    IsCanceled = true,
                    Amount = 10,
                    IsPaid = true,
                    Date = DateTime.Now
                }
            };
        }

        private List<MooringEN> MooringFaker()
        {
            return new List<MooringEN>
            {
                new MooringEN
                {
                    Id = 1,
                    Alias = "mooring1",
                    PortId = 1,
                    Type = MooringEnum.Small
                },
                new MooringEN
                {
                    Id = 2,
                    Alias = "mooring2",
                    PortId = 1,
                    Type = MooringEnum.Small
                },
                new MooringEN
                {
                    Id = 3,
                    Alias = "mooring3",
                    PortId = 2,
                    Type = MooringEnum.Small
                },
                new MooringEN
                {
                    Id = 4,
                    Alias = "mooring4",
                    PortId = 2,
                    Type = MooringEnum.Small
                }
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
                },
                new PortEN
                {
                    Id = 3,
                    Name = "Puerto de la ironia",
                    Location = "c/Río Ebro"
                },
                new PortEN
                {
                    Id = 4,
                    Name = "Puerto de la estrés",
                    Location = "c/Río Ebro"
                },
                new PortEN
                {
                    Id = 5,
                    Name = "Puerto de la apatía",
                    Location = "c/Río Ebro"
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
                    Name = "Buceo",
                    Price = 330,
                    Description = "Actividad de prueba 1"
                },
                new ActivityEN
                {
                    Id = 2,
                    Active = true,
                    Name = "Pesca",
                    Price = 430,
                    Description = "Actividad de prueba 2"
                },
                new ActivityEN
                {
                    Id = 3,
                    Active = true,
                    Name = "Pesca",
                    Price = 350,
                    Description = "Actividad de prueba 3"
                },
                  new ActivityEN
                {
                    Id = 4,
                    Active = false,
                    Name = "Buceo",
                    Price = 280,
                    Description = "Actividad de prueba 4"
                },
                    new ActivityEN
                {
                    Id = 5,
                    Active = false,
                    Name = "Pesca",
                    Price = 250,
                    Description = "Actividad de prueba 5"
                },
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

        private List<TechnicalServiceEN> TecServicesFaker()
        {
            return new List<TechnicalServiceEN>
            {
                new TechnicalServiceEN
                {
                    Active = true,
                    Description = "prueba con barcos",
                    Price = 20,
                    Id = 1
                },

                new TechnicalServiceEN
                {
                    Active = false,
                    Description = "prueba con barcos 2",
                    Price = 20,
                    Id = 2
                },
            };
        }

        private List<TechnicalServiceBoatEN> TechnicalServiceBoatFaker()
        {
            return new List<TechnicalServiceBoatEN>
            {
                new TechnicalServiceBoatEN
                {
                    BoatId = 1,
                    CreatedDate = DateTime.Now,
                    Done = false,
                    Price = 20,
                    ServiceDate = DateTime.Now.AddDays(10),
                    TechnicalServiceId = 1,
                    Id = 1
                },
            };
        }


        private List<BookingEN> BookingFaker() 
        {
            return new List<BookingEN>
            {
                new BookingEN
                {
                    Id = 1,
                    ClientId = "1",
                    CreatedDate = DateTime.Now,
                    EntryDate = DateTime.Now.AddDays(10),
                    DepartureDate = DateTime.Now.AddDays(10).AddHours(5),
                    TotalPeople = 10,
                    Paid = false,
                    RequestCaptain = true,
                    Status = BookingStatusEnum.Booking,
                    InvoiceLine = new InvoiceLineEN{
                        BookingId = 1,
                        ClientInvoiceId = 1,
                        Currency = CurrencyEnum.EUR,
                        TotalAmount = 10
                    }
                }
            };
        }

        private List<ClientInvoiceEN> ClientInvoiceFaker()
        {
            return new List<ClientInvoiceEN>
            {
                new ClientInvoiceEN{
                    Id = 1,
                    CreatedDate = DateTime.Now,
                    Paid = false,
                    Canceled = false, 
                    TotalAmount = 100,
                    ClientId = "1"
                },
                 new ClientInvoiceEN{
                    Id = 2,
                    CreatedDate = DateTime.Now,
                    Paid = true,
                    Canceled = false,
                    TotalAmount = 10000,
                    ClientId = "1"
                },
                  new ClientInvoiceEN{
                    Id = 3,
                    CreatedDate = DateTime.Now,
                    Paid = false,
                    Canceled = true,
                    TotalAmount = 900,
                    ClientId = "1"
                }
            };
        }

        private List<RefundEN> RefundFaker() 
        {
            return new List<RefundEN> 
            { 
                new RefundEN 
                { 
                    Id = 1, 
                    Description = "descripcion", 
                    AmountToReturn = (decimal)100,
                    Date = DateTime.UtcNow,
                    BookingId = 1,
                    ClientInvoiceId = 1
                },
                new RefundEN
                {
                    Id = 2,
                    Description = "descripcion2",
                    AmountToReturn = (decimal)1040,
                    Date = DateTime.UtcNow,
                    BookingId = 1,
                    ClientInvoiceId = 1
                }
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