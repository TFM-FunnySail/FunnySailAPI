using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BoatResourceCEN : IBoatResourceCEN
    {
        private readonly IBoatResourceCAD _boatResourceCAD;

        public BoatResourceCEN(IBoatResourceCAD boatResourceCAD)
        {
            _boatResourceCAD = boatResourceCAD;
        }
    }
}
