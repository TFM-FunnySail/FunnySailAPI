using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input.OwnerInvoice
{
    public class AddOwnerInvoiceInputDTO
    {
        public string OwnerId { get; set; }
        public OwnerInvoicesEnum Type { get; set; }
        public List<int> InvoiceLinesIds { get; set; }
    }
}
