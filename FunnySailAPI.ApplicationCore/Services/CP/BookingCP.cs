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

            if(addBookingInput.Boats.Count == 0 && addBookingInput.ActivityIds.Count == 0 && addBookingInput.ServiceIds.Count == 0)
                throw new DataValidationException("Booking empty",
                   "La reserva debe tener al me.... ");

            UsersEN user = await _userCEN.GetUserCAD().FindById(addBookingInput.ClientId);           
            
            if (user == null)
                throw new DataValidationException("Client",
                  "Cliente", ExceptionTypesEnum.NotFound);


            IList<BoatEN> boats = new List<BoatEN>();
            IList<ActivityEN> activities = new List<ActivityEN>();
            IList<ServiceEN> services = new List<ServiceEN>();

            if (addBookingInput.Boats?.Count > 0) 
            {
                var boatsIds = addBookingInput.Boats.Select(x => x.BoatId).ToList();

                foreach(var boat in addBookingInput.Boats)
                {
                    var boatsNotAvailable = await _boatCEN.GetBoatCAD().GetBoatIdsNotAvailable
                    (boat.EntryDate, boat.DepartureDate, new List<int> { boat.BoatId });

                    var boatNotAvailable = await _boatCEN.GetBoatCAD().FindByIdAllData(boat.BoatId);

                    if (boatsNotAvailable.Count > 0)
                        throw new DataValidationException($"The Boat '{String.Join(",", boatNotAvailable.BoatInfo.Name)}' is not avialable",
                                $"El barco '{ String.Join(",", boatNotAvailable.BoatInfo.Name)}' no está disponible");

                    if (boat.EntryDate > boat.DepartureDate)
                        throw new DataValidationException("Invalid dates", "Fechas inválidas");

                }

                boats = await _boatCEN.GetAll(new BoatFilters { BoatIdList= boatsIds },
                    new Pagination { Limit = 3000},null, 
                    source => source.Include(x => x.BoatInfo).Include(x=>x.BoatPrices)
                    );

            }

            if (addBookingInput.ServiceIds?.Count > 0) 
            {
                
                services = await _serviceCEN.GetServiceCAD().GetServiceFilteredList(
                                    new ServiceFilters { ServiceIdList = addBookingInput.ServiceIds });

            }

            if (addBookingInput.ActivityIds?.Count > 0) 
            {
                
                activities = await _activityCEN.GetActivityCAD().GetServiceFilteredList(
                                    new ActivityFilters { ActivityIdList = addBookingInput.ActivityIds });
            }

            if (boats.Count == 0 && services.Count == 0 && activities.Count == 0)
                throw new DataValidationException("You must create the order with at least one activity, service or boat",
                    "Debe crear la orden con al menos una actividad, servicio o embarcación");

            
            using (var databaseTransaction = _databaseTransactionFactory.BeginTransaction())
            {
                try
                {
                    BookingEN booking = new BookingEN
                    {
                        ClientId = addBookingInput.ClientId,
                        CreatedDate = DateTime.Today,
                        TotalPeople = addBookingInput.TotalPeople
                    };

                    if(addBookingInput.Boats?.Count > 0)
                    {
                        if(addBookingInput.Boats.Any(x => x.EntryDate != null))
                            booking.EntryDate = addBookingInput.Boats.Where(x => x.EntryDate != null).Min(x => x.EntryDate);
                        
                        if(addBookingInput.Boats.Any(x => x.DepartureDate != null))
                            booking.DepartureDate = addBookingInput.Boats.Where(x => x.DepartureDate != null).Max(x => x.DepartureDate);
                    }

                    bookingId = await _bookingCEN.CreateBooking(booking);

                    BookingEN bookingEN = await _bookingCEN.GetBookingCAD().FindById(bookingId);

                    decimal totalAmount = 0;
                    if (boats.Count > 0)
                    {
                        List<OwnerInvoiceLineEN> ownerInvoiceLines = new List<OwnerInvoiceLineEN>();
                        
                        foreach (var boat in boats)
                        {
                            AddBoatBookingInputDTO boatInput = addBookingInput.Boats.FirstOrDefault(x => x.BoatId == boat.Id);
                            double hoursOfDifference = (boatInput.DepartureDate - boatInput.EntryDate).TotalHours;
                            double daysOfDifference = (boatInput.DepartureDate - boatInput.EntryDate).TotalDays;

                            decimal price = _boatPricesCEN.CalculatePrice(boat.BoatPrices,daysOfDifference,hoursOfDifference);
                            await _boatBookingCEN.CreateBoatBooking(new BoatBookingEN
                            {
                                BoatId = boat.Id,
                                BookingId = bookingId,
                                Price = price,
                                RequestCaptain = boatInput.RequestCaptain,
                                DepartureDate = boatInput.DepartureDate,
                                EntryDate = boatInput.EntryDate,
                            });
                            totalAmount += price;

                            if (!ownerInvoiceLines.Any(x => x.OwnerId == boat.OwnerId))
                            {
                                ownerInvoiceLines.Add(new OwnerInvoiceLineEN
                                {
                                    BookingId = bookingId,
                                    OwnerId = boat.OwnerId,
                                    Price = Math.Round(price * (decimal)boat.BoatPrices.PorcentPriceOwner,2)
                                });
                            }
                            else
                            {
                                ownerInvoiceLines.FirstOrDefault(x => x.OwnerId == boat.OwnerId)
                                    .Price = Math.Round(price * (decimal)boat.BoatPrices.PorcentPriceOwner, 2);
                            }
                        }
                        if(ownerInvoiceLines.Count > 0)
                        {
                            await _ownerInvoiceLineCEN.CreateOwnerInvoiceLines(ownerInvoiceLines);
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
                                Price = Math.Round(service.Price,2)
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
                                Price = Math.Round(activity.Price,2)
                            });
                            totalAmount += activity.Price;
                        }
                    }


                    int invoiceLineId = await _invoiceLineCEN.CreateInvoiceLine(new InvoiceLineEN
                    {
                        BookingId = bookingId,
                        Currency = CurrencyEnum.EUR,
                        TotalAmount = Math.Round(totalAmount,2),
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

                    bookingEN.InvoiceLine.ClientInvoiceId = clientInvoice;

                    bookingEN.Paid = true;
                    bookingEN.Status = BookingStatusEnum.Rented;
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
                    await CancelBookingAction(bookingEN);
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

        private async Task CancelBookingAction(BookingEN bookingEN)
        {
            if (bookingEN.Paid)
            {
                await _refundCEN.CreateRefund(bookingEN.Id,
                                        "Orden cancelada",
                                        bookingEN.InvoiceLine.TotalAmount,
                                        bookingEN.InvoiceLine.ClientInvoiceId);
            }

            bookingEN.Status = BookingStatusEnum.Cancelled;
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

            using (var databaseTransaction = _databaseTransactionFactory.BeginTransaction())
            {
                try
                {
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

                    if (updateBookingInputDTO.BoatBookings != null)
                    {
                        foreach (var boat in updateBookingInputDTO.BoatBookings)
                        {
                            BoatPricesEN boatEN = await _boatPricesCEN.GetBoatPricesCAD().FindById(boat.BoatId);
                            if (boatEN == null)
                                throw new DataValidationException($"Boat {boat}", $"Embarcación {boat}",
                                    ExceptionTypesEnum.DontExists);

                            if (boat.EntryDate > boat.DepartureDate)
                                throw new DataValidationException("Invalid dates","Fechas inválidas");

                            var boatBooking = bookingEN.BoatBookings.FirstOrDefault(x => x.BoatId == boat.BoatId);
                            if (boatBooking == null)
                            {
                                decimal price = CalculateBoatPriceInAOrder(boat, boatEN);
                                bookingEN.BoatBookings.Add(new BoatBookingEN
                                {
                                    BoatId = boatEN.BoatId,
                                    Price = price,
                                    RequestCaptain = boat.RequestCaptain,
                                    EntryDate = boat.EntryDate,
                                    DepartureDate = boat.DepartureDate,
                                });
                                extraTotalAmount += price;
                            }
                            else
                            {
                                if(boat.EntryDate != boatBooking.EntryDate || 
                                    boat.DepartureDate != boatBooking.DepartureDate ||
                                    boatBooking.RequestCaptain != boat.RequestCaptain)
                                {
                                    extraTotalAmount -= boatBooking.Price;
                                    decimal price = CalculateBoatPriceInAOrder(boat, boatEN);

                                    boatBooking.EntryDate = boat.EntryDate;
                                    boatBooking.DepartureDate = boat.DepartureDate;
                                    boatBooking.Price = price;
                                    boatBooking.RequestCaptain = boat.RequestCaptain;
                                    await _boatBookingCEN.GetBoatBookingCAD().Update(boatBooking);

                                    extraTotalAmount += price;
                                }
                            }
                        }

                        IList<int> boatBookingsIds = updateBookingInputDTO.BoatBookings.Select(x => x.BoatId).ToList();

                        extraTotalAmount -= bookingEN.BoatBookings.Where(x =>
                        !boatBookingsIds.Contains(x.BoatId)).Sum(x => x.Price);

                        bookingEN.BoatBookings.RemoveAll(x =>
                        !boatBookingsIds.Contains(x.BoatId));
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

                    if (updateBookingInputDTO.ClientId != null && updateBookingInputDTO.ClientId != bookingEN.ClientId)
                    {
                        var newUsertoBooking = _userCEN.GetUserCAD().FindById(updateBookingInputDTO.ClientId);

                        if (newUsertoBooking == null)
                            throw new DataValidationException("New user to booking", "Nuevo usuario para la Reserva",
                            ExceptionTypesEnum.DontExists);

                        bookingEN.ClientId = updateBookingInputDTO.ClientId;
                    }

                    if (updateBookingInputDTO.Status != null)
                    {
                        if(updateBookingInputDTO.Status == BookingStatusEnum.Cancelled)
                        {
                            await CancelBookingAction(bookingEN);
                        }
                        else
                        {
                            bookingEN.Status = (BookingStatusEnum)updateBookingInputDTO.Status;
                        }
                    }

                    if (updateBookingInputDTO.EntryDate != null)
                        bookingEN.EntryDate = (DateTime)updateBookingInputDTO.EntryDate;

                    if (updateBookingInputDTO.DepartureDate != null)
                        bookingEN.DepartureDate = (DateTime)updateBookingInputDTO.DepartureDate;

                    if (updateBookingInputDTO.TotalPeople != null)
                        bookingEN.TotalPeople = (int)updateBookingInputDTO.TotalPeople;


                    bookingEN = await _bookingCEN.UpdateBooking(bookingEN);

                    await databaseTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await databaseTransaction.RollbackAsync();
                    throw ex;
                }
            }

            return bookingEN;
        }

        public decimal CalculateBoatPriceInAOrder(AddBoatBookingInputDTO booking,BoatPricesEN boatPrices)
        {
            double hoursOfDifference = (booking.DepartureDate - booking.EntryDate).TotalHours;
            double daysOfDifference = (booking.DepartureDate - booking.EntryDate).TotalDays;

            return _boatPricesCEN.CalculatePrice(boatPrices, daysOfDifference, hoursOfDifference);
        }
    }
}
