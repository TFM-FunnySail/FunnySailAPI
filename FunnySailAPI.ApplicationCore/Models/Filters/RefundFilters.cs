using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class RefundFilters
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public AmountRangeFilter AmountToReturn { get; set; }
        public DaysRangeFilter Date { get; set; }

        public int BookingId { get; set; }
        public int ClientInvoiceId { get; set; }

    }
}
