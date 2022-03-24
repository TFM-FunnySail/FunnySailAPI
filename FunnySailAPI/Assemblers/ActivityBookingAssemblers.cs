using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Booking;
using System;

namespace FunnySailAPI.Assemblers
{
    public class ActivityBookingAssemblers
    {
        public static ActivityBookingOutputDTO Convert(ActivityBookingEN activityBookingEN)
        {
            ActivityBookingOutputDTO activyBookingOutputDTO = new ActivityBookingOutputDTO
            {
                ActivityId = activityBookingEN.ActivityId,
                Price = activityBookingEN.Price
            };

            return activyBookingOutputDTO;
        }
    }
}