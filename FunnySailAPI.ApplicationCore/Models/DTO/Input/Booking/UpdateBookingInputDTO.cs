using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input.Booking
{
    public class UpdateBookingInputDTO
    {
        public int Id { get; set; }
        public string? ClientId { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public int? TotalPeople { get; set; }
        public bool? RequestCaptain { get; set; }
        public List<(int,int)>? BoatBookingIds { get; set; }
        public List<(int, int)>? ServiceBookingIds { get; set; }
        public List<(int, int)>? ActivityBookingIds { get; set; }
    }
}
