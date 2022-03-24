using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class TechnicalServiceFilters
    {
        public int? Id { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }
    }
}
