using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BoatPricesCEN : IBoatPricesCEN
    {
        private readonly IBoatPricesCAD _boatPricesCAD;

        public BoatPricesCEN(IBoatPricesCAD boatPricesCAD)
        {
            _boatPricesCAD = boatPricesCAD;
        }
    }
}
