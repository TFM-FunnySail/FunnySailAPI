using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Booking;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BookingCEN : IBookingCEN
    {
        private readonly IBookingCAD _bookingCAD;
        private readonly IActivityBookingCAD _activityBookingCAD;
        private readonly IServiceBookingCAD _serviceBookingCAD;
        private readonly IBoatBookingCAD _boatBookingCAD;
        private readonly IUserCAD _userCAD;

        public BookingCEN(IBookingCAD bookingCAD, IActivityBookingCAD activityBookingCAD,
            IServiceBookingCAD serviceBookingCAD, IBoatBookingCAD boatBookingCAD,
            IUserCAD userCAD) 
        {
            _bookingCAD = bookingCAD;
            _activityBookingCAD = activityBookingCAD;
            _serviceBookingCAD = serviceBookingCAD;
            _boatBookingCAD = boatBookingCAD;
            _userCAD = userCAD;
        }

        public async Task<int> CreateBooking(BookingEN bookingEN)
        {
            bookingEN = await _bookingCAD.AddAsync(bookingEN);
            return bookingEN.Id;
        }

        public async Task<IList<BookingEN>> GetAll(BookingFilters filters = null, Pagination pagination = null, Func<IQueryable<BookingEN>, IOrderedQueryable<BookingEN>> orderBy = null, Func<IQueryable<BookingEN>, IIncludableQueryable<BookingEN, object>> includeProperties = null)
        {
            var query = _bookingCAD.GetBookingFiltered(filters);

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.Id);

            return await  _bookingCAD.Get(query, orderBy, includeProperties, pagination);
        }

        public async Task<BookingEN> GetAllDataBooking(int bookingId)
        {
            return await _bookingCAD.FindByIdAllData(bookingId);
        }

        public IBookingCAD GetBookingCAD()
        {
            return _bookingCAD;
        }

        public async Task<int> GetTotal(BookingFilters filters = null)
        {
            var query = _bookingCAD.GetBookingFiltered(filters);

            return await _bookingCAD.GetCounter(query);
        }

        public async Task<BookingEN> UpdateBooking(UpdateBookingInputDTO updateBookingInputDTO)
        {
            if (updateBookingInputDTO.Id == 0)
                throw new DataValidationException("Booking Id", "Id Reserva",
                    ExceptionTypesEnum.IsRequired);

            BookingEN bookingEN = (await GetAll(new BookingFilters
            {
                bookingId = updateBookingInputDTO.Id
            },includeProperties: source => source.Include(x => x.ActivityBookings)
                                        .Include(x => x.BoatBookings)
                                        .Include(x => x.ServiceBookings)
                                        .Include(x => x.InvoiceLine)
                                        .ThenInclude(x=>x.ClientInvoice)))
                                        .FirstOrDefault();

            if(bookingEN == null)
                throw new DataValidationException("Booking", "Reserva",
                    ExceptionTypesEnum.NotFound);

            if (bookingEN.Status == BookingStatusEnum.Cancelled)
                throw new DataValidationException("The reservation cannot be modified because it is canceled",
                    "La reserva no puede ser modificada porque está cancelada");

            if (bookingEN.Status == BookingStatusEnum.Completed)
                throw new DataValidationException("The reservation cannot be modified because it is completed",
                    "La reserva no puede ser modificada porque está completada");

            if(updateBookingInputDTO.Status != null)
            {
                bookingEN.Status = (BookingStatusEnum)updateBookingInputDTO.Status;
            }

            if (updateBookingInputDTO.ClientId != null)
            {
                var newUsertoBooking = _userCAD.FindById(updateBookingInputDTO.ClientId);

                if(newUsertoBooking == null)
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
                    ActivityBookingEN activityEN = await _activityBookingCAD.FindByIds(activity, updateBookingInputDTO.Id);
                    if (activityEN != null && !bookingEN.ActivityBookings.Any(x=>x.ActivityId == activity))
                    {
                        bookingEN.ActivityBookings.Add(new ActivityBookingEN
                        {
                            ActivityId = activity,
                            Price = activityEN.Price
                        });
                        extraTotalAmount += activityEN.Price;
                    }  
                }
            }

            if (updateBookingInputDTO.ServiceBookingIds != null)
            {
                foreach (var service in updateBookingInputDTO.ServiceBookingIds)
                {
                    ServiceBookingEN serviceEN = await _serviceBookingCAD.FindByIds(service, updateBookingInputDTO.Id);
                    if (serviceEN != null && !bookingEN.ServiceBookings.Any(x=>x.ServiceId == service))
                    {
                        bookingEN.ServiceBookings.Add(new ServiceBookingEN
                        {
                            Price = serviceEN.Price,
                            ServiceId = serviceEN.ServiceId
                        });
                        extraTotalAmount += serviceEN.Price;
                    }
                }
            }

            if (updateBookingInputDTO.BoatBookingIds != null)
            {
                foreach (var boat in updateBookingInputDTO.BoatBookingIds)
                {
                    BoatBookingEN boatEN = await _boatBookingCAD.FindByIds(boat, updateBookingInputDTO.Id);
                    if (boatEN != null && !bookingEN.BoatBookings.Any(x=>x.BoatId == boat))
                    {
                        bookingEN.BoatBookings.Add(new BoatBookingEN
                        {
                            BoatId = boatEN.BoatId,
                            Price = boatEN.Price
                        });
                        extraTotalAmount += boatEN.Price;
                    }
                }
            }

            if(extraTotalAmount > 0)
            {
                if (bookingEN.Status == BookingStatusEnum.Rented || bookingEN.Paid)
                    throw new DataValidationException("It is not possible to add activities, services or boats to the reservation because it has already been paid",
                        "No se puede agregar actividades, servicios o embarcaciones a la reserva porque ya fue pagada");

                bookingEN.InvoiceLine.TotalAmount += extraTotalAmount;
                if(bookingEN.InvoiceLine.ClientInvoice != null)
                {
                    bookingEN.InvoiceLine.ClientInvoice.TotalAmount += extraTotalAmount;
                }
            }

            return await _bookingCAD.Update(bookingEN);
        }

        public async Task<BookingEN> UpdateBooking(BookingEN bookingEN)
        {
            return await _bookingCAD.Update(bookingEN);
        }
    }
}
