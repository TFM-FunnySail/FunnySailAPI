using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    class BoatTypeCEN
    {
        private readonly IBoatTypeCEN _boatTypeCEN;

        public BoatTypeCEN(IBoatTypeCEN boatTypeCEN)
        {
            _boatTypeCEN = boatTypeCEN;
        }
    }
}
