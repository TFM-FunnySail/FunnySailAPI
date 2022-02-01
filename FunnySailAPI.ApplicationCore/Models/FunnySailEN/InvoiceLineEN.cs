using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("InvoiceLines")]
    public class InvoiceLineEN
    {
        public int BookingId { get; set; }
        public CurrencyEnum Currency { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        public BookingEN Booking { get; set; }
    }
}
