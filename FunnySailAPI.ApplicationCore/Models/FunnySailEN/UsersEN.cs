using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    public class UsersEN
    {
        public string UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(200)]
        public string LastName { get; set; }

        public bool ReceivePromotion { get; set; }
        public bool BoatOwner { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDay { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(5)]
        public string ZipCode { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public List<BoatEN> Boats { get; set; }
        public List<ReviewEN> AdminReviews { get; set; }
        public List<BookingEN> Bookings { get; set; }
        public List<ClientInvoiceEN> Invoices { get; set; }
    }
}
