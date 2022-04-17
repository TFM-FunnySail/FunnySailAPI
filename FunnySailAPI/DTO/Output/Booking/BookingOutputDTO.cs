using FunnySailAPI.DTO.Output.Boat;
using FunnySailAPI.DTO.Output.ClientInvoice;
using FunnySailAPI.DTO.Output.Refund;
using FunnySailAPI.DTO.Output.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.DTO.Output.Booking
{
    public class BookingOutputDTO
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public int TotalPeople { get; set; }
        public bool Paid { get; set; }
        public bool RequestCaptain { get; set; }
        public string Status { get; set; }
        public UserOutputDTO client { get; set; } 
        public IList<BoatBookingOutputDTO> BoatBookings { get; set; }
        public IList<ServiceBookingOutputDTO> ServiceBookings { get; set; }
        public IList<ActivityBookingOutputDTO> ActivyBookings { get; set; }
        public IList<RefundOutputDTO> Refunds { get; set; }
        public ClientInvoiceLinesOutputDTO ClientInvoiceLine { get; set; }
        public BookingOutputDTO() { }
    }
}
