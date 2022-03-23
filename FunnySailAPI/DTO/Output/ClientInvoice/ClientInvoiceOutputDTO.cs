using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.User;
using System;
using System.Collections.Generic;

namespace FunnySailAPI.DTO.Output.ClientInvoice
{
    public class ClientInvoiceOutputDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Paid { get; set; }
        public bool Canceled { get; set; }
        public decimal TotalAmount { get; set; }
        public string ClientId { get; set; }
        public UserOutputDTO Client { get; set; }
        public List<ClientInvoiceLinesOutputDTO> InvoiceLines { get; set; }
        public List<RefundOutputDTO> Refunds { get; set; }

        public ClientInvoiceOutputDTO()
        {

        }

    }
}
