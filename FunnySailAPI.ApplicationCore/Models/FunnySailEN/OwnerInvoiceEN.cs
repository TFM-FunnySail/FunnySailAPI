using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("OwnerInvoice")]
    public class OwnerInvoiceEN
    {
        [Key]
        public int Id { get; set; }

        public string OwnerId { get; set; }

        public bool ToCollet { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public bool IsPaid { get; set; }

        public bool IsCanceled { get; set; }

        public DateTime Date { get; set; }

        public UsersEN Owner { get; set; }
        public List<OwnerInvoiceLineEN> OwnerInvoiceLines { get; set; }
        public List<TechnicalServiceBoatEN> TechnicalServiceBoats { get; set; }
    }
}
