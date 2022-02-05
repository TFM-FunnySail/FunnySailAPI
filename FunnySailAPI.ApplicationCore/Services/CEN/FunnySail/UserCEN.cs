using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class UserCEN : IUserCEN
    {
        private readonly IUserCAD _userCAD;

        public UserCEN(IUserCAD userCAD)
        {
            _userCAD = userCAD;
        }
    }
}
