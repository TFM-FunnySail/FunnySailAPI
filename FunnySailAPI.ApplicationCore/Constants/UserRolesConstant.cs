using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Constants
{
    public class UserRolesConstant
    {
        public const string ADMIN = "Admin";
        public const string CLIENT = "Client";
        public const string BOAT_OWNER = "BoatOwner";

        public static bool ExistRole(string role)
        {
            return role == ADMIN || role == CLIENT || role == BOAT_OWNER;
        }
    }
}
