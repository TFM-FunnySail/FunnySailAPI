﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Filters
{
    public class DaysRangeFilter
    {
        public DateTime? InitialDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}