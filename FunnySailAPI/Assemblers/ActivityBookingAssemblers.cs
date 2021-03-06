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
                Id = activityBookingEN.ActivityId,
                Price = Math.Round(activityBookingEN.Price, 2)
            };

            if(activityBookingEN.Activity != null)
            {
                activyBookingOutputDTO.Name = activityBookingEN.Activity.Name;
            }

            return activyBookingOutputDTO;
        }
    }
}