using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Services.CP.FunnySail
{
    public class BoatCP
    {
        private readonly IBoatCEN _boatCEN;

        public BoatCP(IBoatCEN boatCEN)
        {
            _boatCEN = boatCEN;
        }
    }
}
