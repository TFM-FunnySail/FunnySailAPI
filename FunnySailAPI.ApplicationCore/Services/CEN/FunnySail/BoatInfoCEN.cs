using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BoatInfoCEN : IBoatInfoCEN
    {
        private readonly IBoatInfoCAD _boatInfoCAD;

        public BoatInfoCEN(IBoatInfoCAD boatInfoCAD)
        {
            _boatInfoCAD = boatInfoCAD;
        }
    }
}
