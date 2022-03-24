using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class ActivityBookingFilters
    {
        public int ActivityId { get; set; }
        public (decimal,decimal) RangePrice { get; set; }
    }
}
