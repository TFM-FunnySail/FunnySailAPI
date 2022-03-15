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
    }

    public interface IRequestUtilityService
    {
        string ipAddress(HttpRequest request, HttpContext httpContext);
    }
}
