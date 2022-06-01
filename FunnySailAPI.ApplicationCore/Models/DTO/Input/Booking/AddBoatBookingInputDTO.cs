using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input.Booking
{
    public class AddBoatBookingInputDTO
    {
        public int BoatId { get; set; }
        public bool RequestCaptain { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
