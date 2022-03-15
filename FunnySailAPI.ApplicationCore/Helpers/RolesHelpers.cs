using FunnySailAPI.ApplicationCore.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Helpers
{
    public static class RolesHelpers
    {

        public static bool AnyRole(IList<string> userRoles, string[] allowedRoles)
        {
            foreach (var role in allowedRoles)
            {
                if (userRoles.Contains(role))
                    return true;
            }
            return false;
        }

        public static bool AnyRole(IList<string> userRoles, string allowedRoles)
        {
            if (userRoles.Contains(allowedRoles))
                return true;
            return false;
        }

        public static bool ExistRole(string role)
        {
            return role == UserRolesConstant.ADMIN || role == UserRolesConstant.CLIENT
                || role == UserRolesConstant.BOAT_OWNER;
        }
    }
}
