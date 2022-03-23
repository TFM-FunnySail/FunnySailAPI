using FunnySailAPI.DTO.Output.ClientInvoice;
using System;

namespace FunnySailAPI.DTO.Output.Refund
{
    public class RefundOutputDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal AmountToReturn { get; set; }
        public DateTime Date { get; set; }
        public int BookingId { get; set; }
        public int? ClientInvoiceId { get; set; }
        public ClientInvoiceOutputDTO ClientInvoice { get; set; }
    }
}
