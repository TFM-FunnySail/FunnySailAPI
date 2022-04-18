using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.DTO.Output.Booking;
using FunnySailAPI.DTO.Output.User;

namespace FunnySailAPI.DTO.Output.OwnerInvoice
{
    public class OwnerInvoiceLinesOutputDTO
    {
        public int BookingId { get; set; }
        public string OwnerId { get; set; }
        public int? OwnerInvoiceId { get; set; }
        public decimal Price { get; set; }
        public UserOutputDTO Owner { get; set; }
        public BookingOutputDTO Booking { get; set; }
    }
}