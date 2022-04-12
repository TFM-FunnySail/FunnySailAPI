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
    }
}
