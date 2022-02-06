using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("ActivityBooking")]
    public class ActivityBookingEN
    {
        public int BookingId { get; set; }
        public int ActivityId { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public BookingEN Booking { get; set; }
        public ActivityEN Activity { get; set; }
    }
}
