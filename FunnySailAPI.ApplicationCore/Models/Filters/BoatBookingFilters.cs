using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class BoatBookingFilters
    {
        public int BoatId { get; set; }
        public (decimal, decimal) RangePrice { get; set; }
    }
}
