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
                Id = boatBookingEN.BoatId,
                Price = Math.Round(boatBookingEN.Price,2),
                EntryDate = boatBookingEN.EntryDate,
                DepartureDate = boatBookingEN.DepartureDate,
            };

            if(boatBookingEN.Boat?.BoatInfo != null)
            {
                boatBookingOutputDTO.Name = boatBookingEN.Boat.BoatInfo.Name;
            }

            return boatBookingOutputDTO;
        }
    }
}