using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class ClientInvoiceFilters
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ClientEmail { get; set; }
        public bool? ToCollet { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public DaysRangeFilter CreatedDaysRange { get; set; }
        public bool? IsPaid { get; set; }
        public bool? IsCanceled { get; set; }
    }
}
