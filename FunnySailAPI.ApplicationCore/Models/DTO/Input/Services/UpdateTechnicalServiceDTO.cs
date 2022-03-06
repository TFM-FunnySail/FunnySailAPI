using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input.Services
{
    public class UpdateTechnicalServiceDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
    }
}
