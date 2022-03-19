using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input.ClientInvoice
{
    public class AddClientInvoiceInputDTO
    {

        public string ClientId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<int> InvoiceLinesIds { get; set; }
    }
}
