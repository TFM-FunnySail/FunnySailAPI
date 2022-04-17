using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class OwnerInvoiceFilters
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string OwnerEmail { get; set; }
        public bool? ToCollet { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public DaysRangeFilter CreatedDaysRange { get; set; }
        public bool? IsPaid { get; set; }
        public bool? IsCanceled { get; set; }

    }
}
