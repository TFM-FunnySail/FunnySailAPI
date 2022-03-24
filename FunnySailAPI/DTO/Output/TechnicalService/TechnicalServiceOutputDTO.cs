using System;
using System.Collections.Generic;

namespace FunnySailAPI.DTO.Output.TechnicalService
{ 
    public class TechnicalServiceOutputDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public List<TechnicalServiceBoatOutputDTO> TechnicalServicesBoat { get; set; }
    }
}