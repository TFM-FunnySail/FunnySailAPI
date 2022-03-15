using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input
{
    public class DisapproveBoatInputDTO
    {
        [JsonIgnore]
        public string AdminId { get; set; }

        [Required]
        public string Observation { get; set; }
    }
}
