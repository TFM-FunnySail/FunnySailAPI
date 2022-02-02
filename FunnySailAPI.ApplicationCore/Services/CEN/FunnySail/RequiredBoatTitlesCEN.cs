using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class RequiredBoatTitlesCEN : IRequiredBoatTitlesCEN
    {
        private readonly IRequiredBoatTitleCAD _requiredBoatTitleCAD;

        public RequiredBoatTitlesCEN(IRequiredBoatTitleCAD requiredBoatTitleCAD)
        {
            _requiredBoatTitleCAD = requiredBoatTitleCAD;
        }
    }
}
