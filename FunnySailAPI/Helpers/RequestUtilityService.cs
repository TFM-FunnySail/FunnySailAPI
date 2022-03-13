using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Generic;

namespace FunnySailAPI.Helpers
{
    public class RequestUtilityService : IRequestUtilityService
    {
        public RequestUtilityService()
        {

        }

        public string ipAddress(HttpRequest request, HttpContext httpContext)
        {
            if (request.Headers.ContainsKey("X-Forwarded-For"))
                return request.Headers["X-Forwarded-For"];
            else
                return httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        public bool AnyRole(IList<string> userRoles, string[] allowedRoles)
        {
            foreach (var role in allowedRoles)
            {
                if(userRoles.Contains(role))
                    return true;
            }
            return false;
        }

        public bool AnyRole(IList<string> userRoles, string allowedRoles)
        {
            if (userRoles.Contains(allowedRoles))
                return true;
            return false;
        }
    }

    public interface IRequestUtilityService
    {
        string ipAddress(HttpRequest request, HttpContext httpContext);
        bool AnyRole(IList<string> userRoles, string[] allowedRoles);
        bool AnyRole(IList<string> userRoles, string allowedRoles);
    }
}
