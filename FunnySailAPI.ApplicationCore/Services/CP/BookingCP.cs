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

                if (addBookingInput.BoatIds.Count != boatsNotAvailable.Count)
                    throw new DataValidationException($"The Boats {String.Join(",", boatsNotAvailable)} not avialable",
                            $"Los barcos {String.Join(",",boatsNotAvailable)} no disponibles");

                boats = await _boatCEN.GetBoatCAD().GetBoatFilteredList(new BoatFilters { BoatIdList= addBookingInput.BoatIds});

            }

            if (addBookingInput.ServiceIds.Count > 0) 
            {
                var servicesNotAvailable = await _serviceCEN.GetServiceCAD().GetServiceIdsNotAvailable
                    (addBookingInput.EntryDate, addBookingInput.DepartureDate, addBookingInput.BoatIds);

                if (addBookingInput.ServiceIds.Count != servicesNotAvailable.Count)
                    throw new DataValidationException($"The services {String.Join(",", servicesNotAvailable)} not avialable",
                            $"Los servicios {String.Join(",", servicesNotAvailable)} no disponibles");

                services = await _serviceCEN.GetServiceCAD().GetServiceFilteredList(
                                    new ServiceFilters { ServiceIdList = addBookingInput.ServiceIds });

            }

            if (addBookingInput.ActivityIds.Count > 0) 
            {
                var activitiesNotAvailable = await _activityCEN.GetActivityCAD().GetActivityIdsNotAvailable
                    (addBookingInput.EntryDate, addBookingInput.DepartureDate, addBookingInput.ActivityIds);

                if (addBookingInput.ActivityIds.Count != activitiesNotAvailable.Count)
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
                        List<BoatBookingEN> boatBookings = new List<BoatBookingEN>();
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
                            boatBookings.Add(await _boatBookingCEN.GetBoatBookingCAD().FindByIds(boat.Id, bookingId));
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
                        bookingEN.BoatBookings = boatBookings;

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
                        List<ServiceBookingEN> serviceBookings = new List<ServiceBookingEN>();
                        foreach (var service in services)
                        {
                            await _serviceBookingCEN.CreateServiceBooking(new ServiceBookingEN
                            {
                                ServiceId = service.Id,
                                BookingId = bookingId,
                                Price = service.Price
                            });
                            serviceBookings.Add(await _serviceBookingCEN.GetServiceBookingCAD().FindByIds(service.Id, bookingId));
                            totalAmount += service.Price;
                        }
                        bookingEN.ServiceBookings = serviceBookings;
                    }

                    if (activities.Count > 0)
                    {
                        List<ActivityBookingEN> activityBookings = new List<ActivityBookingEN>();
                        foreach (var activity in activities)
                        {
                            await _activityBookingCEN.CreateActivityBooking(new ActivityBookingEN
                            {
                                ActivityId = activity.Id,
                                BookingId = bookingId,
                                Price = activity.Price
                            });
                            activityBookings.Add(await _activityBookingCEN.GetActivityBookingCAD().FindByIds(activity.Id, bookingId));
                            totalAmount += activity.Price;
                        }
                        bookingEN.ActivityBookings = activityBookings;
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
            BookingEN bookingEN = await _bookingCEN.GetBookingCAD().FindById(idBooking);

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
                    List<InvoiceLineEN> invoiceLines = new List<InvoiceLineEN>();

                    invoiceLines.Add(bookingEN.InvoiceLine);

                    clientInvoice = await _clientInvoiceCEN.CreateClientInvoice(new ClientInvoiceEN
                    {
                        ClientId = bookingEN.ClientId,
                        CreatedDate = DateTime.Now,
                        InvoiceLines = invoiceLines,
                        TotalAmount = invoiceLines.Sum(x=>x.TotalAmount)
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

    }
}
