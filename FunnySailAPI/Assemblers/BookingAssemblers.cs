using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Booking;
using FunnySailAPI.DTO.Output.ClientInvoice;
using FunnySailAPI.DTO.Output.Refund;
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
                RequestCaptain = booking.RequestCaptain,
                Status = booking.Status.ToString(),
                ActivyBookings = new List<ActivityBookingOutputDTO>(),
                BoatBookings = new List<BoatBookingOutputDTO>(),
                ServiceBookings = new List<ServiceBookingOutputDTO>()
            };

            if (booking.Client != null)
            {
                booking.Client.Bookings = null;
                bookingOutputDTO.client = UserAssemblers.Convert(booking.Client);
            }
                

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

            if(booking.InvoiceLine != null)
            {
                bookingOutputDTO.ClientInvoiceLine = new ClientInvoiceLinesOutputDTO
                {
                    BookingId = booking.Id,
                    Currency = booking.InvoiceLine.Currency.ToString(),
                    TotalAmount = Math.Round(booking.InvoiceLine.TotalAmount, 2),
                };
            }

            if(booking.Refunds != null)
            {
                bookingOutputDTO.Refunds = new List<RefundOutputDTO>();
                foreach (var refund in booking.Refunds)
                {
                    bookingOutputDTO.Refunds.Add(RefundAssemblers.Convert(refund));
                }
            }

            return bookingOutputDTO;
        }
    }
}
