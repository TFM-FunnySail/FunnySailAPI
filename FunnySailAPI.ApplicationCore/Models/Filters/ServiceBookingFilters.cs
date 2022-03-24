using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class ServiceBookingFilters
    {
        public int ServiceId { get; set; }
        public (decimal, decimal) RangePrice { get; set; }
    }
}
