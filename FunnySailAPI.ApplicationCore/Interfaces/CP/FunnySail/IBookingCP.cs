using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Booking;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail
{
    public interface IBookingCP
    {
        Task<int> CreateBooking(AddBookingInputDTO addBookingInput);
        Task<int> PayBooking(int idBooking);
        Task CancelBooking(int idBooking);
        Task<BookingEN> UpdateBooking(UpdateBookingInputDTO updateBookingInputDTO);
    }
}
