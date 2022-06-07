using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.DTO.Output.Boat;
using FunnySailAPI.DTO.Output.Booking;
using System;
using System.Linq;

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
                RequestCaptain = boatBookingEN.RequestCaptain
            };

            if (boatBookingEN.Boat?.BoatResources != null)
            {
                boatBookingOutputDTO.BoatResources = boatBookingEN.Boat.BoatResources.Select(x => new BoatResourcesOutputDTO
                {
                    Id = x.ResourceId,
                    Uri = x.Resource?.Uri,
                    Main = x.Resource?.Main ?? false,
                    Type = x.Resource?.Type ?? ResourcesEnum.Image
                }).ToList();
            }

            if (boatBookingEN.Boat?.BoatInfo != null)
            {
                boatBookingOutputDTO.Name = boatBookingEN.Boat.BoatInfo.Name;
            }

            return boatBookingOutputDTO;
        }
    }
}