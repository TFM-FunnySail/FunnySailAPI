using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Filters
{
    public class BoatFiltersDTO
    {
        public int BoatId { get; set; }
        public bool? Active { get; set; }
        public bool? PendingToReview { get; set; }
        public int BoatTypeId { get; set; }
        public DaysRangeFilterDTO CreatedDaysRange { get; set; }
    }
}
