using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input
{
    public class UpdateBoatInfoInputDTO
    {
        public int BoatId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Registration { get; set; }
        public decimal? Length { get; set; }
        public decimal? Sleeve { get; set; }
        public int? Capacity { get; set; }
        public int? MotorPower { get; set; }
    }
}
