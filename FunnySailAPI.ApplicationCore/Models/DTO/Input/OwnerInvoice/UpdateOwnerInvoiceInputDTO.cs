using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input.OwnerInvoice
{
    public class UpdateOwnerInvoiceInputDTO
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public List<OwnerInvoicesEnum> Type { get; set; }
        public bool ToCollet { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCanceled { get; set; }

    }
}
