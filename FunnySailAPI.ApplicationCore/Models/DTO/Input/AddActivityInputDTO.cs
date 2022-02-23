﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input
{
    public class AddActivityInputDTO
    {
        [Required, StringLength(200)]
        public String Name { get; set; }

        [Required]
        public DateTime ActivityDate { get; set; }

        public bool Active { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required, StringLength(500)]
        public string Description { get; set; }

    }
}
