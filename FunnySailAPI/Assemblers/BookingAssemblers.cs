using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.Assemblers
{
    public class BookingAssemblers
    {
        public static BookingOutputDTO Convert(BookingEN booking) 
        {
            BookingOutputDTO bookingOutputDTO = new BookingOutputDTO
            {
                Id = booking.Id,
                CreatedDate = booking.CreatedDate,
                EntryDate = booking.EntryDate,
                DepartureDate = booking.DepartureDate,
                Paid = booking.Paid,
                TotalPeople = booking.TotalPeople,
                RequestCaptain = booking.RequestCaptain
            };

            if (booking.ClientId != null)
                bookingOutputDTO.client = UserAssemblers.Convert(booking.Client);

            if (booking.ActivityBookings != null)
            {
                foreach (var activityBooking in booking.ActivityBookings)
                {
                    bookingOutputDTO.ActivyBookings.Add(ActivityBookingAssemblers.Convert(activityBooking));
                }
            }

            if (booking.BoatBookings != null)
            {
                foreach (var boatBooking in booking.BoatBookings)
                {
                    bookingOutputDTO.BoatBookings.Add(BoatBookingAssemblers.Convert(boatBooking));
                }
            }

            if (booking.ServiceBookings != null)
            {
                foreach (var serviceBooking in booking.ServiceBookings)
                {
                    bookingOutputDTO.ServiceBookings.Add(ServiceBookingAssemblers.Convert(serviceBooking));
                }
            }

            return bookingOutputDTO;
        }
    }
}
