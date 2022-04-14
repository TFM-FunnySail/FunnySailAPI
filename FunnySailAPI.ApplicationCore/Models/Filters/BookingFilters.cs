using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class BookingFilters
    {
        public int bookingId { get; set; }
        public string ClientId { get; set; }
        public DaysRangeFilter CreatedDateRange { get; set; }
        public DaysRangeFilter EntryDateRange { get; set; }
        public DaysRangeFilter DepartureDateRange { get; set; }
        public int TotalPeople { get; set; }
        public bool? Paid { get; set; }
        public bool? RequestCaptain { get; set; }
    }
}
