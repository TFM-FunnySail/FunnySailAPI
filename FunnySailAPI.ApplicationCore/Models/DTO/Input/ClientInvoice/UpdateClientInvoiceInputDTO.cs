using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input.ClientInvoice
{
    public class UpdateClientInvoiceInputDTO
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public bool Paid { get; set; }
        public bool Canceled { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
