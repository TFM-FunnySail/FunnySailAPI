using FunnySailAPI.DTO.Output.Booking;
using FunnySailAPI.DTO.Output.ClientInvoice;
using FunnySailAPI.DTO.Output.Refund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.DTO.Output.User
{
    public class UserOutputDTO
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool ReceivePromotion { get; set; }
        public bool BoatOwner { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public IList<string> Roles { get; set; }
        public IList<BookingOutputDTO> Bookings { get; set; }
        public IList<ClientInvoiceOutputDTO> Invoices { get; set; }

    }
}
