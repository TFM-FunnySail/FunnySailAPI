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
                Price = activityBookingEN.Price
            };

            if(activityBookingEN.Activity != null)
            {
                activyBookingOutputDTO.Name = activityBookingEN.Activity.Name;
            }

            return activyBookingOutputDTO;
        }
    }
}