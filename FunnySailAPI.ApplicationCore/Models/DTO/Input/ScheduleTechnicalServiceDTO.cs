using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input
{
    public class ScheduleTechnicalServiceDTO
    {
        [Required]
        public int BoatId { get; set; }

        [Required]
        public int TechnicalServiceId { get; set; }

        [Required]
        public DateTime ServiceDate { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
