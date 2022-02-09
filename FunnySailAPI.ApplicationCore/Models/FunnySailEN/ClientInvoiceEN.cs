using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("ClientInvoice")]
    public class ClientInvoiceEN
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Paid { get; set; }
        public bool Canceled { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        public string ClientId { get; set; }

        public UsersEN Client { get; set; }
        public List<InvoiceLineEN> InvoiceLines { get; set; }
    }
}
