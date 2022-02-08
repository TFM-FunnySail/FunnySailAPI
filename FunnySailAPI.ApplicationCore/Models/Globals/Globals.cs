using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Globals
{
    public class Globals
    {
        public static Dictionary<UserRoleEnum, string> UserRoles { get; set; } = new Dictionary<UserRoleEnum, string> {
            {UserRoleEnum.Admin,"Admin" },
            {UserRoleEnum.BoatOwner,"BoatOwner" },
            {UserRoleEnum.Client,"Client" }
        };
    }
}
