using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    public class ApplicationUser : IdentityUser
    {
        public UsersEN Users { get; set; }
    }
}
