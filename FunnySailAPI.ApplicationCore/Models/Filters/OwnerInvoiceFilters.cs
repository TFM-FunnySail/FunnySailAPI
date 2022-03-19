using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class OwnerInvoiceFilters
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public bool? ToCollet { get; set; }
        public PriceRangeFilter CreatedPricesRange { get; set; }
        public DaysRangeFilter CreatedDaysRange { get; set; }
        public bool? IsPaid { get; set; }
        public bool? IsCanceled { get; set; }

    }
}
