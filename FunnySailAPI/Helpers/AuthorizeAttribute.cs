using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunnySailAPI.Helpers
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<string> _roles;

        public CustomAuthorizeAttribute(params string[] roles)
        {
            _roles = roles ?? new string[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (UsersEN)context.HttpContext.Items["User"];
            var userRoles = (IList<string>)context.HttpContext.Items["Roles"];
            if (user == null || (_roles.Any() && !AnyRole(userRoles)))
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }

        private bool AnyRole(IList<string> userRoles)
        {
            foreach(var role in _roles)
            {
                if (userRoles.Contains(role))
                    return true;
            }
            return false;
        }
    }
}
