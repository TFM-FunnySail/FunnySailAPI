using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input.Booking
{
    public class AddBookingInputDTO
    {
        public string ClientId { get; set; }
        [Required]
        public int TotalPeople { get; set; }
        public bool RequestCaptain { get; set; }
        public List<AddBoatBookingInputDTO> Boats { get; set; }
        public List<int> ServiceIds { get; set; }
        public List<int> ActivityIds { get; set; }
    }
}
