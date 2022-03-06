using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input
{
    public class DisapproveBoatInputDTO
    {
        public string AdminId { get; set; }

        [Required]
        public string Observation { get; set; }
    }
}
