using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input.User
{
    public class AddUserInputDTO
    {
        [Required,EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(200)]
        public string FirstName { get; set; }

        [StringLength(200)]
        public string LastName { get; set; }

        [Required, DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public bool? ReceivePromotion { get; set; }
        public DateTime? BirthDay { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }
        public string ReturnUrl { get; set; }

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
    }
}
