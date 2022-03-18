using FunnySailAPI.ApplicationCore.Models.Globals;

namespace FunnySailAPI.DTO.Output.OwnerInvoice
{
    public class OwnerInvoiceLinesOutputDTO
    {
        public int BookingId { get; set; }
        public string OwnerId { get; set; }
        public int? OwnerInvoiceId { get; set; }
        public decimal Price { get; set; }
    }
}