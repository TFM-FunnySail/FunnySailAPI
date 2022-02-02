using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input
{
    public class AddBoatInputDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Registration { get; set; }
        public string MooringPoint { get; set; }
        public decimal Length { get; set; }
        public decimal Sleeve { get; set; }
        public int Capacity { get; set; }
        public int MotorPower { get; set; }
        public int BoatTypeId { get; set; }
        public decimal DayBasePrice { get; set; }
        public decimal HourBasePrice { get; set; }
        public float Supplement { get; set; }
        public List<AddBoatResourcesInputDTO> BoatResources { get; set; }
        public List<AddRequiredBoatTitleInputDTO> RequiredBoatTitles { get; set; }
    }
}
