using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
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

        public BookingCEN(IBookingCAD bookingCAD) 
        {
            _bookingCAD = bookingCAD;
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

        public async Task<BookingEN> UpdateBooking(BookingEN bookingEN)
        {
            return await _bookingCAD.Update(bookingEN);
        }
    }
}
