using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("Bookings")]
    public class BookingEN
    {
        [Key]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public int TotalPeople { get; set; }
        public bool Paid { get; set; }
        public bool RequestCaptain { get; set; }
        public BookingStatusEnum Status { get; set; }

        public UsersEN Client { get; set; }
        public InvoiceLineEN InvoiceLine { get; set; }
        public List<BoatBookingEN> BoatBookings { get; set; }
    }
}
