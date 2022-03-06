using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Boat;
using FunnySailAPI.DTO.Output.Mooring;
using FunnySailAPI.DTO.Output.Port;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.Assemblers
{
    public static class BoatAssemblers
    {
        public static BoatOutputDTO Convert(BoatEN boatEN)
        {
            BoatOutputDTO boatOutput = new BoatOutputDTO
            {
                Id = boatEN.Id,
                Supplement = boatEN.BoatPrices.Supplement,
                Active = boatEN.Active,
                CreatedDate = boatEN.CreatedDate,
                PendingToReview = boatEN.PendingToReview,
                BoatType = new BoatTypeOutputDTO
                {
                    Id = boatEN.BoatType.Id,
                    Name = boatEN.BoatType.Name,
                    Description = boatEN.BoatType.Description,
                },
                Capacity = boatEN.BoatInfo.Capacity,
                Description = boatEN.BoatInfo.Description,
                Name = boatEN.BoatInfo.Name,
                MotorPower = boatEN.BoatInfo.MotorPower,
                Length = boatEN.BoatInfo.Length,
                Sleeve = boatEN.BoatInfo.Sleeve,
                Registration = boatEN.BoatInfo.Registration,
                DayBasePrice = boatEN.BoatPrices.DayBasePrice,
                HourBasePrice = boatEN.BoatPrices.HourBasePrice,
            };

            boatOutput.RequiredBoatTitles = boatEN.RequiredBoatTitles.Select(x => new RequiredBoatTitleOutputDTO
            {
                TitleId = x.TitleId
            }).ToList();

            boatOutput.BoatResources = boatEN.BoatResources.Select(x => new BoatResourcesOutputDTO
            {
                Uri = x.Resource.Uri,
                Main = x.Resource.Main,
                Type = x.Resource.Type
            }).ToList();

            if(boatEN.Mooring != null)
            {
                boatOutput.Mooring = new MooringOutputDTO
                {
                    Id = boatEN.Mooring.Id,
                    Alias = boatEN.Mooring.Alias,
                    Type = boatEN.Mooring.Type.ToString(),
                    PortId = boatEN.Mooring.PortId,
                    Port = new PortOutputDTO
                    {
                        Location = boatEN.Mooring.Port.Location,
                        Id = boatEN.Mooring.Port.Id,
                        Name = boatEN.Mooring.Port.Name,
                    }
                };
            }

            return boatOutput;
        }
    }
}
