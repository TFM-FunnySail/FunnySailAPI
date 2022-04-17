using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class OwnerInvoiceLineFilters
    {
        public List<int> BookingIds { get; set; }
        public string OwnerId { get; set; }
        public int OwnerInvoiceId { get; set; }
        public bool? Invoiced { get; set; }
    }
}
