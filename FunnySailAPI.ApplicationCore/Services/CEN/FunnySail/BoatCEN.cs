using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BoatCEN : IBoatCEN
    {
        private readonly IBoatCAD _boatCAD;

        public BoatCEN(IBoatCAD boatCAD)
        {
            _boatCAD = boatCAD;
        }


    }
}
