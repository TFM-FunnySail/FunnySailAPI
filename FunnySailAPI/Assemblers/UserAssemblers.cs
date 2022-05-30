using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Booking;
using FunnySailAPI.DTO.Output.ClientInvoice;
using FunnySailAPI.DTO.Output.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.Assemblers
{
    public static class UserAssemblers
    {
        internal static UserOutputDTO Convert(UsersEN userEN, IList<string> roles = null)
        {
            UserOutputDTO user = new UserOutputDTO
            {
                UserId = userEN.UserId,
                BirthDay = userEN.BirthDay,
                BoatOwner = userEN.BoatOwner,
                FirstName = userEN.FirstName,
                LastName = userEN.LastName,
                ReceivePromotion = userEN.ReceivePromotion,
                Roles = roles,
                Address = userEN.Address,
                State = userEN.State,
                City = userEN.City,
                Country = userEN.Country,
                ZipCode = userEN.ZipCode
            };

            if (userEN.ApplicationUser != null)
            {
                user.EmailConfirmed = userEN.ApplicationUser.EmailConfirmed;
                user.Email = userEN.ApplicationUser.Email;
                user.PhoneNumber = userEN.ApplicationUser.PhoneNumber;
                user.UserName = userEN.ApplicationUser.UserName;
            }

            if(userEN.Bookings?.Count > 0)
            {
                user.Bookings = new List<BookingOutputDTO>();
                foreach(var booking in userEN.Bookings.OrderByDescending(x=>x.Id))
                {
                    booking.Client = null;
                    user.Bookings.Add(BookingAssemblers.Convert(booking));
                }
            }

            if(userEN.Invoices?.Count > 0)
            {
                user.Invoices = new List<ClientInvoiceOutputDTO>();
                foreach (var invoice in userEN.Invoices.OrderByDescending(x => x.Id))
                {
                    invoice.Client = null;
                    invoice.InvoiceLines = null;
                    invoice.Refunds = null;
                    user.Invoices.Add(ClientInvoiceAssemblers.Convert(invoice));
                }
            }

            return user;
        }
    }
}
