using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Filters
{
    public class ServiceFilters
    {
        public int ServiceId { get; set; }
        public bool? Active { get; set; }
        public List<int> ServiceIdList { get; set; }
    }
}
