using FunnySailAPI.DTO.Output.Boat;
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
        public UserOutputDTO client { get; set; } 
        //public InvoiceDTO 
        public List<BoatBookingOutputDTO> BoatBookings { get; set; }
        public List<ServiceBookingOutputDTO> ServiceBookings { get; set; }
        public List<ActivyBookingOutputDTO> ActivyBookings { get; set; }
        public BookingOutputDTO() { }
    }
}
