using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Mooring;
using FunnySailAPI.DTO.Output.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunnySailAPI.DTO.Output.Boat
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
        public decimal Length { get; set; }
        public decimal Sleeve { get; set; }
        public int Capacity { get; set; }
        public int MotorPower { get; set; }
        public decimal? DayBasePrice { get; set; }
        public decimal? HourBasePrice { get; set; }
        public decimal? Price { get; set; }
        public float? Supplement { get; set; }
        public BoatTypeOutputDTO BoatType { get; set; }
        public MooringOutputDTO Mooring { get; set; }
        public List<BoatResourcesOutputDTO> BoatResources { get; set; }
        public List<RequiredBoatTitleOutputDTO> RequiredBoatTitles { get; set; }

        public BoatOutputDTO()
        {

        }

    }
}
