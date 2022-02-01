using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("BoatBooking")]
    public class BoatBookingEN
    {
        public int BookingId { get; set; }
        public int BoatId { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public BookingEN Booking { get; set; }
        public BoatEN Boat { get; set; }
    }
}
