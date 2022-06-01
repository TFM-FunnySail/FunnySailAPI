using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class BoatFilters
    {
        public int BoatId { get; set; }
        public bool? Active { get; set; }
        public bool? PendingToReview { get; set; }
        public int BoatTypeId { get; set; }
        public DaysRangeFilter CreatedDaysRange { get; set; }
        public List<int> ExclusiveBoatId { get; set; }
        public List<int> BoatIdList { get; set; }
        public string OwnerId { get; set; }
    }
}
