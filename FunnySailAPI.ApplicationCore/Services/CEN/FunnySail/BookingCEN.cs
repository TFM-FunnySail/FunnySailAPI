using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Booking;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
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

        public BookingCEN(IBookingCAD bookingCAD, IActivityBookingCAD activityBookingCAD, IServiceBookingCAD serviceBookingCAD, IBoatBookingCAD boatBookingCAD) 
        {
            _bookingCAD = bookingCAD;
            _activityBookingCAD = activityBookingCAD;
            _serviceBookingCAD = serviceBookingCAD;
            _boatBookingCAD = boatBookingCAD;
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
            if (updateBookingInputDTO.Id == null)
                throw new DataValidationException("Booking Id", "Id Reserva",
                    ExceptionTypesEnum.IsRequired);

            BookingEN bookingEN = await _bookingCAD.FindById(updateBookingInputDTO.Id);

            if (updateBookingInputDTO.EntryDate != null)
                bookingEN.EntryDate = (DateTime)updateBookingInputDTO.EntryDate;

            if (updateBookingInputDTO.DepartureDate != null)
                bookingEN.DepartureDate = (DateTime)updateBookingInputDTO.DepartureDate;

            if (updateBookingInputDTO.TotalPeople != null)
                bookingEN.TotalPeople = (int)updateBookingInputDTO.TotalPeople;

            if (updateBookingInputDTO.ClientId != null)
                bookingEN.ClientId = (string)updateBookingInputDTO.ClientId;

            if (updateBookingInputDTO.RequestCaptain != null)
                bookingEN.RequestCaptain = (bool)updateBookingInputDTO.RequestCaptain;

            if (updateBookingInputDTO.ActivityBookingIds != null) 
            {
                List<ActivityBookingEN> activityENs = new List<ActivityBookingEN>();
                foreach (var activity in updateBookingInputDTO.ActivityBookingIds)
                {
                    ActivityBookingEN activityEN = await _activityBookingCAD.FindByIds(activity.Item1, activity.Item2);
                    if (activityEN != null)
                        activityENs.Add(activityEN);
                }
            }

            if (updateBookingInputDTO.ServiceBookingIds != null)
            {
                List<ServiceBookingEN> servicesENs = new List<ServiceBookingEN>();
                foreach (var service in updateBookingInputDTO.ServiceBookingIds)
                {
                    ServiceBookingEN serviceEN = await _serviceBookingCAD.FindByIds(service.Item1, service.Item2);
                    if (serviceEN != null)
                        servicesENs.Add(serviceEN);
                }
            }

            if (updateBookingInputDTO.BoatBookingIds != null)
            {
                List<BoatBookingEN> boatENs = new List<BoatBookingEN>();
                foreach (var boat in updateBookingInputDTO.BoatBookingIds)
                {
                    BoatBookingEN boatEN = await _boatBookingCAD.FindByIds(boat.Item1, boat.Item2);
                    if (boatEN != null)
                        boatENs.Add(boatEN);
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
