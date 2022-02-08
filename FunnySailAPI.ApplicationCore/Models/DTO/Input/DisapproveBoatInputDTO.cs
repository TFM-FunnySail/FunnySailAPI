﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input
{
    public class DisapproveBoatInputDTO
    {
        public int BoatId { get; set; }
        public int AdminId { get; set; }

        [Required]
        public string Observation { get; set; }
    }
}
