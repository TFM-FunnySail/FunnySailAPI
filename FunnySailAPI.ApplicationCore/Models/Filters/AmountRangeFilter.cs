using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class AmountRangeFilter
    {
        public decimal? Max { get; set; }
        public decimal? Min { get; set; }
    }
}
