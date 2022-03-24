using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class BookingFilters
    {
        public int? bookingId { get; set; }
        public string? ClientId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public int? TotalPeople { get; set; }
        public bool? Paid { get; set; }
        public bool? RequestCaptain { get; set; }
    }
}
