using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    class BoatResourceCEN
    {
        private readonly IBoatResourceCEN _boatResourceCEN;

        public BoatResourceCEN(IBoatResourceCEN boatResourceCEN)
        {
            _boatResourceCEN = boatResourceCEN;
        }
    }
}
