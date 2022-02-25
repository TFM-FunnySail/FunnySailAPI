using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
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

        public async Task<BookingEN> GetAllDataBooking(int bookingId)
        {
            return await _bookingCAD.FindByIdAllData(bookingId);
        }

        public IBookingCAD GetBookingCAD()
        {
            return _bookingCAD;
        }
    }
}
