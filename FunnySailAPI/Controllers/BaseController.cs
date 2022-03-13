using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FunnySailAPI.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        public UsersEN User => (UsersEN)HttpContext.Items["User"];
        public IList<string> UserRoles { get {
                if (HttpContext.Items["Roles"] == null)
                    return new List<string>();
                return (IList<string>)HttpContext.Items["Roles"];
            } }
    }
}
