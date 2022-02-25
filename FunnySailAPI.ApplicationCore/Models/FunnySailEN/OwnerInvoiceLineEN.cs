using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("OwnerInvoiceLine")]
    public class OwnerInvoiceLineEN
    {
        public int BookingId { get; set; }
        public string OwnerId { get; set; }
        public int? OwnerInvoiceId { get; set; }
        
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public BookingEN Booking { get; set; }
        public OwnerInvoiceEN OwnerInvoice { get; set; }
    }
}
