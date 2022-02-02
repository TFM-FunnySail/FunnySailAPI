using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Output
{
    public class BoatOutputDTO
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public bool PendingToReview { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Registration { get; set; }
        public string MooringPoint { get; set; }
        public decimal Length { get; set; }
        public decimal Sleeve { get; set; }
        public int Capacity { get; set; }
        public int MotorPower { get; set; }
        public decimal DayBasePrice { get; set; }
        public decimal HourBasePrice { get; set; }
        public float Supplement { get; set; }
        public BoatTypeOutputDTO BoatType { get; set; }
        public List<BoatResourcesOutputDTO> BoatResources { get; set; }
        public List<RequiredBoatTitleOutputDTO> RequiredBoatTitles { get; set; }

        public BoatOutputDTO()
        {

        }

        public BoatOutputDTO(BoatEN boatEN)
        {
            Id = boatEN.Id;
            Supplement = boatEN.BoatPrices.Supplement;
            Active = boatEN.Active;
            CreatedDate = boatEN.CreatedDate;
            PendingToReview = boatEN.PendingToReview;
            BoatResources = boatEN.BoatResources.Select(x => new BoatResourcesOutputDTO
            {
                Main = x.Main,
                Type = x.Type,
                Uri = x.Uri
            }).ToList();
            BoatType = new BoatTypeOutputDTO
            {
                Id = boatEN.BoatType.Id,
                Name = boatEN.BoatType.Name,
                Description = boatEN.BoatType.Description,
            };
            Capacity = boatEN.BoatInfo.Capacity;
            Description = boatEN.BoatInfo.Description;
            Name = boatEN.BoatInfo.Name;
            MooringPoint = boatEN.BoatInfo.MooringPoint;
            MotorPower = boatEN.BoatInfo.MotorPower;
            Length = boatEN.BoatInfo.Length;
            Sleeve = boatEN.BoatInfo.Sleeve;
            Registration = boatEN.BoatInfo.Registration;
            DayBasePrice = boatEN.BoatPrices.DayBasePrice;
            HourBasePrice = boatEN.BoatPrices.HourBasePrice;
            RequiredBoatTitles = boatEN.RequiredBoatTitles.Select(x => new RequiredBoatTitleOutputDTO
            {
                TitleId = x.TitleId
            }).ToList();
            
        }
    }
}
