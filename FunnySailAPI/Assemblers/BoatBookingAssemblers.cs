using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Booking;
using System;

namespace FunnySailAPI.Assemblers
{
    public class BoatBookingAssemblers
    {
        public static BoatBookingOutputDTO Convert(BoatBookingEN boatBookingEN)
        {
            BoatBookingOutputDTO boatBookingOutputDTO = new BoatBookingOutputDTO
            {
                BoatId = boatBookingEN.BoatId,
                Price = boatBookingEN.Price
            };

            return boatBookingOutputDTO;
        }
    }
}