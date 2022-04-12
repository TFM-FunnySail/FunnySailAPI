using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Booking;
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
                Roles = roles
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

            return user;
        }
    }
}
