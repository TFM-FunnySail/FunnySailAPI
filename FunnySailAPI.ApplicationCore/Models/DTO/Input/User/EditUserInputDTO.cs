using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input.User
{
    public class EditUserInputDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? ReceivePromotion { get; set; }
        public DateTime? BirthDay { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
