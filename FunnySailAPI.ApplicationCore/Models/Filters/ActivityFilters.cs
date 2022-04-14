using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class ActivityFilters
    {
        public int ActivityId { get; set; }
        public bool? Active { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> ActivityIdList { get; set; }
        public List<int> ActivityNotIdList { get; set; }
    }
}
