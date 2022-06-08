using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class TechnicalServiceFilters
    {
        public int Id { get; set; }
        public int BoatId { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }
    }
}
