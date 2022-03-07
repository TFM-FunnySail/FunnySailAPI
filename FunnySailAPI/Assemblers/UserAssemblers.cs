using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.Assemblers
{
    public static class UserAssemblers
    {
        internal static UserOutputDTO Convert(UsersEN userEN)
        {
            UserOutputDTO user = new UserOutputDTO
            {
                UserId = userEN.UserId,
                BirthDay = userEN.BirthDay,
                BoatOwner = userEN.BoatOwner,
                FirstName = userEN.FirstName,
                LastName = userEN.LastName,
                ReceivePromotion = userEN.ReceivePromotion,
                EmailConfirmed = userEN.ApplicationUser.EmailConfirmed,
                Email = userEN.ApplicationUser.Email,
                PhoneNumber = userEN.ApplicationUser.PhoneNumber,
                UserName = userEN.ApplicationUser.UserName,
            };
            return user;
        }
    }
}
