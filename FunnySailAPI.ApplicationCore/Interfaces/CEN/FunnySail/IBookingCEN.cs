using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Booking;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IBookingCEN
    {
        Task<int> CreateBooking(BookingEN bookingEN);
        Task<BookingEN> GetAllDataBooking(int bookingId);
        IBookingCAD GetBookingCAD();
        Task<BookingEN> UpdateBooking(BookingEN bookingEN);
        Task<IList<BookingEN>> GetAll(BookingFilters filters = null,
        Pagination pagination = null,
        Func<IQueryable<BookingEN>, IOrderedQueryable<BookingEN>> orderBy = null,
        Func<IQueryable<BookingEN>, IIncludableQueryable<BookingEN, object>> includeProperties = null);
        Task<int> GetTotal(BookingFilters filters = null);
    }
}
