using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{   
    [Table("Refund")]
    public class RefundEN
    {
        [Key]
        public int Id { get; set; }
        
        [Required, MaxLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "money")]
        public decimal AmountToReturn { get; set; }
        public DateTime Date { get; set; }

        public int BookingId { get; set; }
        public int? ClientInvoiceId { get; set; }

        public BookingEN Booking { get; set; }

        public ClientInvoiceEN ClientInvoice { get; set; }
    }
}
