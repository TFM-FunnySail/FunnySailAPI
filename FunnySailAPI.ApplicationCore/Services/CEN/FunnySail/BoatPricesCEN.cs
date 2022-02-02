using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BoatPricesCEN
    {
        private readonly IBoatPricesCAD _boatPricesCAD;

        public BoatPricesCEN(IBoatPricesCAD boatPricesCAD)
        {
            _boatPricesCAD = boatPricesCAD;
        }
    }
}
