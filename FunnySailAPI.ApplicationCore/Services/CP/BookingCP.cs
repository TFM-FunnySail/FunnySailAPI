using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Booking;
using Microsoft.EntityFrameworkCore;

namespace FunnySailAPI.ApplicationCore.Services.CP
{
    public class BookingCP : IBookingCP
    {
        private readonly IBookingCEN _bookingCEN;
        private readonly IUserCEN _userCEN;
        private readonly IClientInvoiceLineCEN _invoiceLineCEN;
        private readonly IOwnerInvoiceLineCEN _ownerInvoiceLineCEN;
        private readonly IBoatBookingCEN _boatBookingCEN;
        private readonly IServiceBookingCEN _serviceBookingCEN;
        private readonly IActivityBookingCEN _activityBookingCEN;
        private readonly IActivityCEN _activityCEN;
        private readonly IBoatCEN _boatCEN;
        private readonly IServiceCEN _serviceCEN;
        private readonly IBoatCP _boatCP;
        private readonly IClientInvoiceCEN _clientInvoiceCEN;
        private readonly IRefundCEN _refundCEN;
        private readonly IBoatPricesCEN _boatPricesCEN;
        private readonly IOwnerInvoiceCEN _ownerInvoiceCEN;
        private IDatabaseTransactionFactory _databaseTransactionFactory;

        public BookingCP(IBookingCEN bookingCEN,
                         IUserCEN userCEN,
                         IClientInvoiceLineCEN invoiceLineCEN,
                         IOwnerInvoiceLineCEN ownerInvoiceLineCEN,
                         IBoatBookingCEN boatBookingCEN,
                         IServiceBookingCEN serviceBookingCEN,
                         IActivityBookingCEN activityBookingCEN,
                         IActivityCEN activityCEN,
                         IBoatCEN boatCEN,
                         IServiceCEN serviceCEN,
                         IBoatCP boatCP,
                         IClientInvoiceCEN clientInvoiceCEN,
                         IDatabaseTransactionFactory databaseTransactionFactory,
                         IRefundCEN refundCEN,
                         IBoatPricesCEN boatPricesCEN,
                         IOwnerInvoiceCEN ownerInvoiceCEN) 
        {
            _bookingCEN = bookingCEN;
            _userCEN = userCEN;
            _invoiceLineCEN = invoiceLineCEN;
            _ownerInvoiceLineCEN = ownerInvoiceLineCEN;
            _boatBookingCEN = boatBookingCEN;
            _serviceBookingCEN = serviceBookingCEN;
            _activityBookingCEN = activityBookingCEN;
            _activityCEN = activityCEN;
            _boatCEN = boatCEN;
            _serviceCEN = serviceCEN;
            _boatCP = boatCP;
            _clientInvoiceCEN = clientInvoiceCEN;
            _databaseTransactionFactory = databaseTransactionFactory;
            _refundCEN = refundCEN;
            _boatPricesCEN = boatPricesCEN;
            _ownerInvoiceCEN = ownerInvoiceCEN;
        }
        public async Task<int> CreateBooking(AddBookingInputDTO addBookingInput)
        {
            int bookingId = 0;
            // VALIDATIONS
            if (addBookingInput.ClientId == null)
                throw new DataValidationException("Client Id",
                    "Id Cliente",ExceptionTypesEnum.IsRequired);

            if(addBookingInput.BoatIds.Count == 0 && addBookingInput.ActivityIds.Count == 0 && addBookingInput.ServiceIds.Count == 0)
                throw new DataValidationException("Booking empty",
                   "La reserva debe tener al me.... ");

            UsersEN user = await _userCEN.GetUserCAD().FindById(addBookingInput.ClientId);           
            
            if (user == null)
                throw new DataValidationException("Client",
                  "Cliente", ExceptionTypesEnum.NotFound);


            List<BoatEN> boats = new List<BoatEN>();
            List<ActivityEN> activities = new List<ActivityEN>();
            List<ServiceEN> services = new List<ServiceEN>();

            if (addBookingInput.BoatIds.Count > 0) 
            {
                var boatsNotAvailable = await _boatCEN.GetBoatCAD().GetBoatIdsNotAvailable
                    (addBookingInput.EntryDate, addBookingInput.DepartureDate,addBookingInput.BoatIds);

                if (boatsNotAvailable.Any(x => addBookingInput.BoatIds.Contains(x)))
                    throw new DataValidationException($"The Boats {String.Join(",", boatsNotAvailable)} not avialable",
                            $"Los barcos {String.Join(",",boatsNotAvailable)} no disponibles");

                boats = await _boatCEN.GetBoatCAD().GetBoatFilteredList(new BoatFilters { BoatIdList= addBookingInput.BoatIds});

            }

            if (addBookingInput.ServiceIds.Count > 0) 
            {
                var servicesNotAvailable = await _serviceCEN.GetServiceCAD().GetServiceIdsNotAvailable
                    (addBookingInput.EntryDate, addBookingInput.DepartureDate, addBookingInput.ServiceIds);

                if (servicesNotAvailable.Any(x=> addBookingInput.ServiceIds.Contains(x)))
                    throw new DataValidationException($"The services {String.Join(",", servicesNotAvailable)} not avialable",
                            $"Los servicios {String.Join(",", servicesNotAvailable)} no están disponibles para las fechas seleccionadas");

                services = await _serviceCEN.GetServiceCAD().GetServiceFilteredList(
                                    new ServiceFilters { ServiceIdList = addBookingInput.ServiceIds });

            }

            if (addBookingInput.ActivityIds.Count > 0) 
            {
                var activitiesNotAvailable = await _activityCEN.GetActivityCAD().GetActivityIdsNotAvailable
                    (addBookingInput.EntryDate, addBookingInput.DepartureDate, addBookingInput.ActivityIds);

                if (activitiesNotAvailable.Any(x => addBookingInput.ActivityIds.Contains(x)))
                    throw new DataValidationException($"The activities {String.Join(",", activitiesNotAvailable)} are not avialable",
                            $"Las actividades {String.Join(",", activitiesNotAvailable)} no están disponibles");

                activities = await _activityCEN.GetActivityCAD().GetServiceFilteredList(
                                    new ActivityFilters { ActivityIdList = addBookingInput.ActivityIds });
            }

            if (boats.Count == 0 && services.Count == 0 && activities.Count == 0)
                throw new DataValidationException("You must create the order with at least one activity, service or boat",
                    "Debe crear la orden con al menos una actividad, servicio o embarcación");

            double hoursOfDifference = (addBookingInput.DepartureDate - addBookingInput.EntryDate).TotalHours;
            double daysOfDifference = (addBookingInput.DepartureDate - addBookingInput.EntryDate).TotalDays;

            using (var databaseTransaction = _databaseTransactionFactory.BeginTransaction())
            {
                try
                {
                    bookingId = await _bookingCEN.CreateBooking(new BookingEN
                    {
                        ClientId = addBookingInput.ClientId,
                        CreatedDate = DateTime.Today,
                        RequestCaptain = addBookingInput.RequestCaptain,
                        EntryDate = addBookingInput.EntryDate,
                        DepartureDate = addBookingInput.DepartureDate,
                        TotalPeople = addBookingInput.TotalPeople
                    });

                    BookingEN bookingEN = await _bookingCEN.GetBookingCAD().FindById(bookingId);

                    decimal totalAmount = 0;
                    if (boats.Count > 0)
                    {
                        List<OwnerInvoiceLineEN> ownerInvoiceLines = new List<OwnerInvoiceLineEN>();
                        
                        foreach (var boat in boats)
                        {
                            decimal price = _boatPricesCEN.CalculatePrice(boat.BoatPrices,daysOfDifference,hoursOfDifference);
                            await _boatBookingCEN.CreateBoatBooking(new BoatBookingEN
                            {
                                BoatId = boat.Id,
                                BookingId = bookingId,
                                Price = price
                            });
                            totalAmount += price;

                            if (!ownerInvoiceLines.Any(x => x.OwnerId == boat.OwnerId))
                            {
                                ownerInvoiceLines.Add(new OwnerInvoiceLineEN
                                {
                                    BookingId = bookingId,
                                    OwnerId = boat.OwnerId,
                                    Price = price* (decimal)boat.BoatPrices.PorcentPriceOwner
                                });
                            }
                            else
                            {
                                ownerInvoiceLines.FirstOrDefault(x => x.OwnerId == boat.OwnerId)
                                    .Price = price * (decimal)boat.BoatPrices.PorcentPriceOwner;
                            }
                        }
                        if(ownerInvoiceLines.Count > 0)
                        {
                            foreach(var ownerInvoiceLine in ownerInvoiceLines) 
                            {
                                await _ownerInvoiceCEN.CreateOwnerInvoice(ownerInvoiceLine.OwnerId, ownerInvoiceLine.Price, true);
                            }
                        }
                    }

                    if (services.Count > 0)
                    {
                        foreach (var service in services)
                        {
                            await _serviceBookingCEN.CreateServiceBooking(new ServiceBookingEN
                            {
                                ServiceId = service.Id,
                                BookingId = bookingId,
                                Price = service.Price
                            });
                            totalAmount += service.Price;
                        }
                    }

                    if (activities.Count > 0)
                    {
                        foreach (var activity in activities)
                        {
                            await _activityBookingCEN.CreateActivityBooking(new ActivityBookingEN
                            {
                                ActivityId = activity.Id,
                                BookingId = bookingId,
                                Price = activity.Price
                            });
                            totalAmount += activity.Price;
                        }
                    }


                    int invoiceLineId = await _invoiceLineCEN.CreateInvoiceLine(new InvoiceLineEN
                    {
                        BookingId = bookingId,
                        Currency = CurrencyEnum.EUR,
                        TotalAmount = totalAmount,
                    });
                    await databaseTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await databaseTransaction.RollbackAsync();
                    throw ex;
                }
            }

            return bookingId;
        }

        public async Task<int> PayBooking(int idBooking) 
        {
            BookingEN bookingEN = (await _bookingCEN.GetAll(new BookingFilters
            {
                bookingId = idBooking
            }, includeProperties: source => source.Include(x => x.InvoiceLine)))
                                        .FirstOrDefault();

            if (bookingEN == null)
                throw new DataValidationException("Booking Id",
                   "Id Booking", ExceptionTypesEnum.NotFound);

            if (bookingEN.Paid)
                throw new DataValidationException("The reservation has already been paid",
                    "La reserva ya ha sido pagada");

            int clientInvoice = 0;

            using (var databaseTransaction = _databaseTransactionFactory.BeginTransaction())
            {
                try
                {
                    clientInvoice = await _clientInvoiceCEN.CreateClientInvoice(new ClientInvoiceEN
                    {
                        ClientId = bookingEN.ClientId,
                        CreatedDate = DateTime.UtcNow,
                        Paid = true,
                        TotalAmount = bookingEN.InvoiceLine.TotalAmount
                    });

                    bookingEN.Paid = true;
                    await _bookingCEN.UpdateBooking(bookingEN);

                    await databaseTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await databaseTransaction.RollbackAsync();
                    throw ex;
                }
            }
                
            return clientInvoice;
        }

        public async Task CancelBooking(int idBooking)
        {
            BookingEN bookingEN = await _bookingCEN.GetBookingCAD().FindByIdAllData(idBooking);

            if (bookingEN == null)
                throw new DataValidationException("Booking Id",
                   "Id Booking", ExceptionTypesEnum.NotFound);

            if (bookingEN.Status == BookingStatusEnum.Completed)
                throw new DataValidationException("The reservation has already been completed, it cannot be canceled",
                    "La reserva ya ha sido completada, no se puede cancelar");

            using (var databaseTransaction = _databaseTransactionFactory.BeginTransaction())
            {
                try
                {
                    if (bookingEN.Paid)
                    {
                        await _refundCEN.CreateRefund(bookingEN.Id,
                                                "Orden cancelada",
                                                bookingEN.InvoiceLine.TotalAmount);
                    }

                    bookingEN.Status = BookingStatusEnum.Cancelled;
                    await _bookingCEN.UpdateBooking(bookingEN);

                    await databaseTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await databaseTransaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task<BookingEN> UpdateBooking(UpdateBookingInputDTO updateBookingInputDTO)
        {
            if (updateBookingInputDTO.Id == 0)
                throw new DataValidationException("Booking Id", "Id Reserva",
                    ExceptionTypesEnum.IsRequired);

            BookingEN bookingEN = (await _bookingCEN.GetAll(new BookingFilters
            {
                bookingId = updateBookingInputDTO.Id
            }, includeProperties: source => source
                                         .Include(x => x.Client)
                                        .ThenInclude(x => x.ApplicationUser)
                                        .Include(x => x.ActivityBookings)
                                        .ThenInclude(x => x.Activity)
                                        .Include(x => x.BoatBookings)
                                        .ThenInclude(x => x.Boat.BoatInfo)
                                        .Include(x => x.ServiceBookings)
                                        .ThenInclude(x => x.service)
                                        .Include(x => x.InvoiceLine)
                                         .ThenInclude(x => x.ClientInvoice)))
                                        .FirstOrDefault();

            if (bookingEN == null)
                throw new DataValidationException("Booking", "Reserva",
                    ExceptionTypesEnum.NotFound);

            if (bookingEN.Status == BookingStatusEnum.Cancelled)
                throw new DataValidationException("The reservation cannot be modified because it is canceled",
                    "La reserva no puede ser modificada porque está cancelada");

            if (bookingEN.Status == BookingStatusEnum.Completed)
                throw new DataValidationException("The reservation cannot be modified because it is completed",
                    "La reserva no puede ser modificada porque está completada");

            if (updateBookingInputDTO.Status != null)
            {
                bookingEN.Status = (BookingStatusEnum)updateBookingInputDTO.Status;
            }

            if (updateBookingInputDTO.ClientId != null && updateBookingInputDTO.ClientId != bookingEN.ClientId)
            {
                var newUsertoBooking = _userCEN.GetUserCAD().FindById(updateBookingInputDTO.ClientId);

                if (newUsertoBooking == null)
                    throw new DataValidationException("New user to booking", "Nuevo usuario para la Reserva",
                    ExceptionTypesEnum.DontExists);

                bookingEN.ClientId = updateBookingInputDTO.ClientId;
            }

            if (updateBookingInputDTO.EntryDate != null)
                bookingEN.EntryDate = (DateTime)updateBookingInputDTO.EntryDate;

            if (updateBookingInputDTO.DepartureDate != null)
                bookingEN.DepartureDate = (DateTime)updateBookingInputDTO.DepartureDate;

            if (updateBookingInputDTO.TotalPeople != null)
                bookingEN.TotalPeople = (int)updateBookingInputDTO.TotalPeople;

            if (updateBookingInputDTO.RequestCaptain != null)
                bookingEN.RequestCaptain = (bool)updateBookingInputDTO.RequestCaptain;

            decimal extraTotalAmount = 0;
            if (updateBookingInputDTO.ActivityBookingIds != null)
            {
                foreach (var activity in updateBookingInputDTO.ActivityBookingIds)
                {
                    ActivityEN activityEN = await _activityCEN.GetActivityCAD().FindById(activity);
                    if (activityEN == null)
                        throw new DataValidationException($"Activity {activity}", $"Actividad {activity}",
                            ExceptionTypesEnum.DontExists);

                    if (!bookingEN.ActivityBookings.Any(x => x.ActivityId == activity))
                    {
                        bookingEN.ActivityBookings.Add(new ActivityBookingEN
                        {
                            ActivityId = activity,
                            Price = activityEN.Price
                        });
                        extraTotalAmount += activityEN.Price;
                    }
                }

                extraTotalAmount -= bookingEN.ActivityBookings.Where(x =>
                !updateBookingInputDTO.ActivityBookingIds.Contains(x.ActivityId)).Sum(x => x.Price);

                bookingEN.ActivityBookings.RemoveAll(x =>
                !updateBookingInputDTO.ActivityBookingIds.Contains(x.ActivityId));
            }

            if (updateBookingInputDTO.ServiceBookingIds != null)
            {
                foreach (var service in updateBookingInputDTO.ServiceBookingIds)
                {
                    ServiceEN serviceEN = await _serviceCEN.GetServiceCAD().FindById(service);
                    if (serviceEN == null)
                        throw new DataValidationException($"Service {service}", $"Servicio {service}",
                            ExceptionTypesEnum.DontExists);
                    if (!bookingEN.ServiceBookings.Any(x => x.ServiceId == service))
                    {
                        bookingEN.ServiceBookings.Add(new ServiceBookingEN
                        {
                            Price = serviceEN.Price,
                            ServiceId = service
                        });
                        extraTotalAmount += serviceEN.Price;
                    }
                }

                extraTotalAmount -= bookingEN.ServiceBookings.Where(x =>
                !updateBookingInputDTO.ServiceBookingIds.Contains(x.ServiceId)).Sum(x => x.Price);

                bookingEN.ServiceBookings.RemoveAll(x =>
                !updateBookingInputDTO.ServiceBookingIds.Contains(x.ServiceId));
            }

            if (updateBookingInputDTO.BoatBookingIds != null)
            {
                foreach (var boat in updateBookingInputDTO.BoatBookingIds)
                {
                    BoatPricesEN boatEN = await _boatPricesCEN.GetBoatPricesCAD().FindById(boat);
                    if (boatEN == null)
                        throw new DataValidationException($"Boat {boat}", $"Embarcación {boat}",
                            ExceptionTypesEnum.DontExists);
                    
                    if (!bookingEN.BoatBookings.Any(x => x.BoatId == boat))
                    {
                        decimal price = CalculateBoatPriceInAOrder(bookingEN, boatEN);
                        bookingEN.BoatBookings.Add(new BoatBookingEN
                        {
                            BoatId = boatEN.BoatId,
                            Price = price
                        });
                        extraTotalAmount += price;
                    }
                }

                extraTotalAmount -= bookingEN.BoatBookings.Where(x =>
                !updateBookingInputDTO.BoatBookingIds.Contains(x.BoatId)).Sum(x => x.Price);

                bookingEN.BoatBookings.RemoveAll(x =>
                !updateBookingInputDTO.BoatBookingIds.Contains(x.BoatId));
            }

            if (extraTotalAmount != 0)
            {
                if (bookingEN.Status == BookingStatusEnum.Rented || bookingEN.Paid)
                    throw new DataValidationException("It is not possible to update activities, services or boats to the reservation because it has already been paid",
                        "No se puede modificar actividades, servicios o embarcaciones a la reserva porque ya fue pagada");

                bookingEN.InvoiceLine.TotalAmount += extraTotalAmount;
                if (bookingEN.InvoiceLine.ClientInvoice != null)
                {
                    bookingEN.InvoiceLine.ClientInvoice.TotalAmount += extraTotalAmount;
                }
            }

            return await _bookingCEN.UpdateBooking(bookingEN);
        }

        public decimal CalculateBoatPriceInAOrder(BookingEN booking,BoatPricesEN boatPrices)
        {
            double hoursOfDifference = (booking.DepartureDate - booking.EntryDate).TotalHours;
            double daysOfDifference = (booking.DepartureDate - booking.EntryDate).TotalDays;

            return _boatPricesCEN.CalculatePrice(boatPrices, daysOfDifference, hoursOfDifference);
        }
    }
}
