using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class ReviewFilters
    {
        public int Id { get; set; }
        public int BoatId { get; set; }
        public string AdminId { get; set; }
        public string Description { get; set; }
        public bool? Closed { get; set; }
    }
}
