using FunnySailAPI.DTO.Output.TechnicalService;
using FunnySailAPI.DTO.Output.User;
using System;
using System.Collections.Generic;

namespace FunnySailAPI.DTO.Output.OwnerInvoice
{
    public class OwnerInvoiceOutputDTO
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public bool ToCollet { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime Date { get; set; }
        public UserOutputDTO Owner { get; set; }
        public List<OwnerInvoiceLinesOutputDTO> OwnerInvoiceLines { get; set; }
        public List<TechnicalServiceBoatOutputDTO> TechnicalServiceBoats { get; set; }

        public OwnerInvoiceOutputDTO()
        {

        }

    }
}
