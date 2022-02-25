using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Filters;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CP
{
    public class BookingCP : IBookingCP
    {
        private readonly IBookingCEN _bookingCEN;
        private readonly IUserCEN _userCEN;
        private readonly IInvoiceLineCEN _invoiceLineCEN;
        private readonly IOwnerInvoiceLineCEN _ownerInvoiceLineCEN;
        private readonly IBoatBookingCEN _boatBookingCEN;
        private readonly IServiceBookingCEN _serviceBookingCEN;
        private readonly IActivityBookingCEN _activityBookingCEN;
        private readonly IActivityCEN _activityCEN;
        private readonly IBoatCEN _boatCEN;
        private readonly IServiceCEN _serviceCEN;
        private readonly IBoatCP _boatCP;
        private readonly IClientInvoiceCEN _clientInvoiceCEN;

        public BookingCP(IBookingCEN bookingCEN,
                         IUserCEN userCEN,
                         IInvoiceLineCEN invoiceLineCEN,
                         IOwnerInvoiceLineCEN ownerInvoiceLineCEN,
                         IBoatBookingCEN boatBookingCEN,
                         IServiceBookingCEN serviceBookingCEN,
                         IActivityBookingCEN activityBookingCEN,
                         IActivityCEN activityCEN,
                         IBoatCEN boatCEN,
                         IServiceCEN serviceCEN,
                         IBoatCP boatCP,
                         IClientInvoiceCEN clientInvoiceCEN) 
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
            
            if (boats.Count > 0 || services.Count > 0 || activities.Count > 0) 
            {
                Task<int> idBooking = _bookingCEN.CreateBooking(new BookingEN{ 
                    ClientId = addBookingInput.ClientId,
                    CreatedDate = DateTime.Today,
                    RequestCaptain = addBookingInput.RequestCaptain,
                    EntryDate = addBookingInput.EntryDate,
                    DepartureDate = addBookingInput.DepartureDate,
                    TotalPeople = addBookingInput.TotalPeople
                });
                bookingId = idBooking.Result;

                BookingEN bookingEN = _bookingCEN.GetBookingCAD().FindById(bookingId).Result;

                Task<Tuple<int, int?>> invoiceLine = _invoiceLineCEN.CreateInvoiceLine(new InvoiceLineEN
                {
                    BookingId = bookingId
                });

                InvoiceLineEN invoiceLineEN = _invoiceLineCEN.GetInvoiceLineCAD().FindByIds(invoiceLine.Result.Item1, invoiceLine.Result.Item2).Result;
                bookingEN.InvoiceLine = invoiceLineEN;

                if (boats.Count > 0) {
                    List<BoatBookingEN> boatBookings = new List<BoatBookingEN>(); 
                    foreach (var boat in boats) 
                    {
                        Task<decimal> price = _boatCP.CalculatePrice();
                        Task<Tuple<int, int>> boatBooking = _boatBookingCEN.CreateBoatBooking(new BoatBookingEN
                        {
                            BoatId = boat.Id,
                            BookingId = bookingId,
                            Price = price.Result
                        });
                        boatBookings.Add(_boatBookingCEN.GetBoatBookingCAD().FindByIds(boat.Id, bookingId).Result);
                        invoiceLineEN.TotalAmount += price.Result;
                    }
                    bookingEN.BoatBookings = boatBookings;
                }

                if (services.Count > 0)
                {
                    List<ServiceBookingEN> serviceBookings = new List<ServiceBookingEN>();
                    foreach (var service in services)
                    {
                        Task<Tuple<int, int>> serviceBooking = _serviceBookingCEN.CreateServiceBooking(new ServiceBookingEN
                        {
                            ServiceId = service.Id,
                            BookingId = bookingId,
                            Price = service.Price
                        });
                        serviceBookings.Add(_serviceBookingCEN.GetServiceBookingCAD().FindByIds(service.Id, bookingId).Result);
                        invoiceLineEN.TotalAmount += service.Price;
                    }
                    bookingEN.ServiceBookings = serviceBookings; 
                }

                if (activities.Count > 0)
                {
                    List<ActivityBookingEN> activityBookings = new List<ActivityBookingEN>();
                    foreach (var activity in activities)
                    {
                        Task<Tuple<int, int>> activityBooking = _activityBookingCEN.CreateActivityBooking(new ActivityBookingEN
                        {
                            ActivityId = activity.Id,
                            BookingId = bookingId,
                            Price = activity.Price
                        });
                        activityBookings.Add(_activityBookingCEN.GetActivityBookingCAD().FindByIds(activity.Id, bookingId).Result);
                        invoiceLineEN.TotalAmount += activity.Price;
                    }
                    bookingEN.ActivityBookings = activityBookings;
                }

            }

            

            return bookingId;
        }

        public async Task<int> PayBooking(int idBooking) 
        {
            BookingEN bookingEN = _bookingCEN.GetBookingCAD().FindById(idBooking)?.Result;

            if (bookingEN == null)
                throw new DataValidationException("Booking Id",
                   "Id Booking", ExceptionTypesEnum.NotFound);

            bookingEN.Paid = true;

            List<InvoiceLineEN> invoiceLines = new List<InvoiceLineEN>();

            invoiceLines.Add(bookingEN.InvoiceLine);

            int clientInvoice = await _clientInvoiceCEN.CreateClientInvoice(new ClientInvoiceEN {
                ClientId = bookingEN.ClientId,
                CreatedDate = DateTime.Now,
                InvoiceLines = invoiceLines,
                TotalAmount = invoiceLines[0].TotalAmount
            });

            return clientInvoice;
        }

        public async Task<int> CancelBooking(int idBooking)
        {

            BookingEN bookingEN = _bookingCEN.GetBookingCAD().FindById(idBooking)?.Result;

            if (bookingEN == null)
                throw new DataValidationException("Booking Id",
                   "Id Booking", ExceptionTypesEnum.NotFound);

            bookingEN.Status = BookingStatusEnum.Cancelled;

            if (bookingEN.Paid)
            {
                //REFOUND!
            }

            if (bookingEN.ActivityBookings.Count > 0) 
            {
                foreach (var activityBooking in bookingEN.ActivityBookings) {
                    await _activityBookingCEN.GetActivityBookingCAD().Delete(activityBooking);   
                }
            }

            if (bookingEN.BoatBookings.Count > 0)
            {
                foreach (var boatBooking in bookingEN.BoatBookings)
                {
                    await _boatBookingCEN.GetBoatBookingCAD().Delete(boatBooking);
                }
            }

            if (bookingEN.ServiceBookings.Count > 0)
            {
                foreach (var serviceBooking in bookingEN.ServiceBookings)
                {
                    await _serviceBookingCEN.GetServiceBookingCAD().Delete(serviceBooking);
                }
            }

            return 0;// ???
        }

    }
}
