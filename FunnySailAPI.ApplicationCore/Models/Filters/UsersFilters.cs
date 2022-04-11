using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class UsersFilters
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? ReceivePromotion { get; set; }
        public bool? BoatOwner { get; set; }
        public DaysRangeFilter BirthDay { get; set; }
    }
}
