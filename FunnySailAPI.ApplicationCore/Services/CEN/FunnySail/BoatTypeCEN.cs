﻿using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BoatTypeCEN : IBoatTypeCEN
    {
        private readonly IBoatTypeCAD _boatTypeCAD;

        public BoatTypeCEN(IBoatTypeCAD boatTypeCAD)
        {
            _boatTypeCAD = boatTypeCAD;
        }
    }
}
