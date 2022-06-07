using FunnySailAPI.DTO.Output.Boat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.DTO.Output.Booking
{
    public class BoatBookingOutputDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public bool RequestCaptain { get; set; }
        public IList<BoatResourcesOutputDTO> BoatResources { get; set; }
    }
}
