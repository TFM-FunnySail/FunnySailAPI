using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("ServiceBookings")]
    public class ServiceBookingEN
    {
        public int ServiceId { get; set; }
        public int BookingId { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public BookingEN booking { get; set; }
        public ServiceEN service { get; set; }
    }
}
