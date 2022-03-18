using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input.OwnerInvoice
{
    public class UpdateOwnerInvoiceInputDTO
    {
        public string OwnerId { get; set; }
        public List<OwnerInvoicesEnum> Type { get; set; }

    }
}
