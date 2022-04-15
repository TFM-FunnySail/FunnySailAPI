using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.DTO.Output.User;
using System;

namespace FunnySailAPI.DTO.Output.ClientInvoice
{
    public class ClientInvoiceLinesOutputDTO
    {
        public int BookingId { get; set; }
        public string Currency { get; set; }
        public decimal TotalAmount { get; set; }
        public int? ClientInvoiceId { get; set; }

    }
}