using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input
{
    public class AddBoatInputDTO
    {
        [Required, StringLength(200)]
        public string Name { get; set; }

        [Required, StringLength(1000)]
        public string Description { get; set; }

        [Required, StringLength(50)]
        public string Registration { get; set; }

        [Required, StringLength(50)]
        public string MooringPoint { get; set; }

        [Required]
        public decimal Length { get; set; }

        [Required]
        public decimal Sleeve { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public int MotorPower { get; set; }

        [Required]
        public int BoatTypeId { get; set; }

        [Required]
        public decimal DayBasePrice { get; set; }

        [Required]
        public decimal HourBasePrice { get; set; }

        [Required]
        public float Supplement { get; set; }

        public int MooringId { get; set; }
        public string OwnerId { get; set; }

        public List<int> ResourcesIdList { get; set; }
        public List<int> RequiredBoatTitles { get; set; }
    }
}
